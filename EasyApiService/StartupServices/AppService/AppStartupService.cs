using CoreApplication.CommentApplication;
using CoreApplication.UserApplication;
using CoreBussiness.BussinessEntity.AppServices;
using CoreBussiness.BussinessEntity.Comments;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.ManagerService;
using CoreBussiness.UnitOfWork;
using CoreStrorage.AppContext;
using EasyApiService.StartupServices.AuthenticationServices;
using EasyApiService.StartupServices.MainServices;
using EasyApiService.StartupServices.SeedService;
using MapsterMapper;

namespace EasyApiService.StartupServices.AppService;

public static class AppStartupService
{
    public static void RunAppServices(this IServiceCollection service)
    {
        service.AddScoped<IUserService, UserService>();
        service.AddScoped<ICommentService, CommentService>();
        service.AddScoped<IAppService, CoreApplication.AppServiceApplication.AppService>();
        service.AddScoped<IUnitOfWork, ApplicationContext>();
        service.AddScoped<IManagerService, ManagerService>();
        service.AddScoped<IMapper,Mapper>();
        service.AddScoped<IDatabaseInitializer,DatabaseInitialService>();
        service.AddScoped<IJwtAuthManagerService, JwtAuthManagerService>();
        service.AddScoped<IMainService, MainService>();
    }
}