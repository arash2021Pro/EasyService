using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.UnitOfWork;
using CoreStrorage.AppContext;
using Microsoft.EntityFrameworkCore;

namespace EasyApiService.StartupServices.SeedService;

public class DatabaseInitialService:IDatabaseInitializer
{
    private ApplicationContext _applicationContext;

    public DatabaseInitialService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public void SeedData()
    {
        if (!_applicationContext.Users.Any())
        {
            var user = new User() {PhoneNumber = "09130242717", Password = "1380", IsAdmin = "admin", IsGender = true};
            _applicationContext.Users.AddAsync(user);
            _applicationContext.SaveChanges();
        }
    }
}