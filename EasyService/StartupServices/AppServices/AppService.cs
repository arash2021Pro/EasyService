using Blazored.SessionStorage;
using EasyService.StartupServices.ApiServices;
using EasyService.StartupServices.AuthServices;
using Microsoft.AspNetCore.Components.Authorization;

namespace EasyService.StartupServices.AppServices;

public static class AppService
{
    public static void RunAppServices(this IServiceCollection service)
    {
        service.AddTransient(sp => new HttpClient  {BaseAddress = new Uri("https://localhost:7065/")});
        service.AddScoped<IApiService, ApiService>();
        service.AddBlazoredSessionStorage();
        service.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
    }
}