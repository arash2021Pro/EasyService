using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.CoreEntity;

namespace CoreBussiness.BussinessEntity.AppServices;

public class AppService:Core
{
    public string ?Gmail { get; set; }
    public string ?Password { get; set; }
    public string ?PhoneNumber { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
}