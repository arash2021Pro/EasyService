using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoreBussiness.ManagerService;
using EasyApiService.Models.Users;
using Microsoft.IdentityModel.Tokens;

namespace EasyApiService.StartupServices.AuthenticationServices;

public class JwtAuthManagerService:IJwtAuthManagerService
{
    private string Jwt_Security_Key = " my-32-character-ultra-secure-and-ultra-long-secret";
    private const int Valid_Token_Time = 10;
    private IManagerService _managerService;
    public JwtAuthManagerService(IManagerService managerService)
    {
        _managerService = managerService;
    }

    public async Task<UserSession?> GenerateJwtAsync(string Phonenumber, string Password)
    {
        if (String.IsNullOrEmpty(Phonenumber) || String.IsNullOrEmpty(Password)) return null;
        var user = await _managerService.UserService.GetUserAsync(Phonenumber);
        if (user == null) return null;
        var TokenExpiryTimeStamp = DateTime.Now.AddMinutes(Valid_Token_Time);
        var TokenKey = Encoding.ASCII.GetBytes(Jwt_Security_Key);
        ClaimsIdentity claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.PhoneNumber));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if (user.IsAdmin == "admin")
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Anonymous,1.ToString()));
        }
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "user"));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Anonymous,2.ToString()));
        var SigningCredetials =
            new SigningCredentials(new SymmetricSecurityKey(TokenKey), SecurityAlgorithms.HmacSha256Signature);
        var SecurityTokenDescriptor = new SecurityTokenDescriptor()
            {Subject = claimsIdentity, Expires = TokenExpiryTimeStamp, SigningCredentials = SigningCredetials};
        var JwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var SecurityToken = JwtSecurityTokenHandler.CreateToken(SecurityTokenDescriptor);
        var Token = JwtSecurityTokenHandler.WriteToken(SecurityToken);
        var UserSession = new UserSession()
        {
            Id = user.Id
            ,Role = user.IsAdmin
            , Token = Token, 
            PhoneNumber = user.PhoneNumber,
            ExpiresIn = (int) TokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
            IsAuthenticated = true
        };
        return UserSession;
    }
}