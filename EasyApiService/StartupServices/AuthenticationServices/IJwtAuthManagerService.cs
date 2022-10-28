using EasyApiService.Models.Users;

namespace EasyApiService.StartupServices.AuthenticationServices;

public interface IJwtAuthManagerService
{
    Task<UserSession?> GenerateJwtAsync(string Phonenumber, string Password);
}