namespace EasyApiService.StartupServices.MainServices;

public interface IMainService
{
    string GenerateGmail(int charcount);
    string GeneratePassword(int len);
    string GeneratePhoneNumber(int len);
}