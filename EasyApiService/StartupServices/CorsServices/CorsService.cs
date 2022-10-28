namespace EasyApiService.StartupServices.CorsServices;

public static class CorsService
{
    public static void RunCorsService(this IServiceCollection service)
    {
        service.AddCors(policy =>
        {
            policy.AddPolicy("CorsPolicy", opt => opt
                .AllowAnyHeader()
                .SetIsOriginAllowed(option=>true)
                .AllowAnyMethod()
                .AllowCredentials()
            );
        });
    }
}