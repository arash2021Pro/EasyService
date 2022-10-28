namespace EasyApiService.StartupServices.SeedService;

public static class ScopeInitialService
{
    public static void RunInitialScopeService(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using var scope=scopeFactory.CreateScope();
        var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
        databaseInitializer.SeedData();
    }
}