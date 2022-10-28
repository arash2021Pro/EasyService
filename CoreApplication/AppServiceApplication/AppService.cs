using CoreBussiness.BussinessEntity.AppServices;
using CoreBussiness.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CoreApplication.AppServiceApplication;

public class AppService:IAppService
{
    private DbSet<CoreBussiness.BussinessEntity.AppServices.AppService> _appServices;
    public AppService(IUnitOfWork work)
    {
        _appServices = work.Set<CoreBussiness.BussinessEntity.AppServices.AppService>();
    }

    public async Task AddAppService(CoreBussiness.BussinessEntity.AppServices.AppService appService) =>
        await _appServices.AddAsync(appService);

}