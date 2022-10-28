using System.Security.Claims;
using Blazored.SessionStorage;
using EasyService.StartupServices.ApiServices;
using EasyService.StartupServices.BlazoredStorageService;
using Microsoft.AspNetCore.Components.Authorization;

namespace EasyService.StartupServices.AuthServices;

public class CustomAuthProvider:AuthenticationStateProvider
{
    private IApiService _apiService;
    private ISessionStorageService _sessionStorageService;
    private ClaimsPrincipal _claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
    public CustomAuthProvider(IApiService apiService, ISessionStorageService sessionStorageService)
    {
        _apiService = apiService;
        _sessionStorageService = sessionStorageService;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var UserSession = await _sessionStorageService.ReadEncryptedItemAsync<UserSession>("UserSession");
            if (UserSession == null)
                return await Task.FromResult(new AuthenticationState(_claimsPrincipal));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("JwtAuth");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, UserSession.PhoneNumber));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserSession.Id.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, UserSession.Role));
            
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            return await Task.FromResult(new AuthenticationState(claimPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_claimsPrincipal));
        }
    }

    public async Task UpdateAuthenticationStateAsync(UserSession? userSession)
    {
        ClaimsPrincipal claimsPrincipal;
        if (userSession != null)
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
               new Claim(ClaimTypes.Name,userSession.PhoneNumber),
               new Claim(ClaimTypes.NameIdentifier,userSession.Id.ToString()),
               new Claim(ClaimTypes.Role,userSession.Role),
         
            }));
            userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpiresIn);
            await _sessionStorageService.SaveItemEncryptedAsync("UserSession", userSession);
        }
        else
        {
            claimsPrincipal = _claimsPrincipal;
            await _sessionStorageService.RemoveItemAsync("UserSession");
        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }


    public async Task<string> GetTokenJwtAsync()
    {
        var result = String.Empty;
        try
        {
            var UserSession = await _sessionStorageService.ReadEncryptedItemAsync<UserSession>("UserSession");
            if (UserSession != null && DateTime.Now < UserSession.ExpiryTimeStamp)
                result = UserSession.Token;
        }
        catch { }

        return result;
    }
}