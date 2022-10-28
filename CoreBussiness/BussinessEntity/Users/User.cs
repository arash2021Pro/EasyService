using CoreBussiness.BussinessEntity.AppServices;
using CoreBussiness.BussinessEntity.Comments;
using CoreBussiness.CoreEntity;

namespace CoreBussiness.BussinessEntity.Users;

public class User:Core
{
    public string? PhoneNumber{ get; set; }
    public string? Password { get; set; }
    public string?IsAdmin { get; set; }
    public bool IsGender { get; set; }
    public ICollection<AppService>AppServices { get; set; }
    public ICollection<Comment>Comments { get; set; }
}