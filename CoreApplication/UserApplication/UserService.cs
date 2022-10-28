using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CoreApplication.UserApplication;

public class UserService:IUserService
{
    private DbSet<User> _users;
    public UserService(IUnitOfWork work)
    {
        _users = work.Set<User>();
    }

    public async Task SignupUserAsync(User user) => await _users.AddAsync(user);

    public async Task<User?> GetUserAsync(string? phoneNumber) =>
        await _users.FirstOrDefaultAsync(x => x.PhoneNumber!.Equals(phoneNumber));

    public async Task<User?> GetUserAsync(int userId) => await _users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
    public async Task<bool> IsUserExistsAsync(string? phoneNumber) => await _users.AnyAsync(x =>x.PhoneNumber.Equals(phoneNumber));

}