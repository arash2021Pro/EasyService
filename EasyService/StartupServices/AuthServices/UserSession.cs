namespace EasyService.StartupServices.AuthServices;

public class UserSession
{
     public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public int ExpiresIn { get; set; }
    public DateTime ExpiryTimeStamp { get; set; }
}