using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EasyApiService.StartupServices.AuthenticationServices;

public static class AuthenticationService
{
    public static void AddJwtAuthenticationService(this IServiceCollection service,IConfiguration configuration)
    {
          
        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtBearerOptions =>
        {
            jwtBearerOptions.RequireHttpsMetadata = false;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtConfiguration:SecretKey"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}