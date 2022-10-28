namespace CoreBussiness.BussinessEntity.Users;

public interface IUserService
{
    Task SignupUserAsync(User user);
    Task<User?> GetUserAsync(string? phoneNumber);
    Task<User?> GetUserAsync(int userId);
    Task<bool> IsUserExistsAsync(string? phoneNumber);
    
}