using EasyService.Models;
using EasyService.Responses;
using EasyService.StartupServices.AuthServices;

namespace EasyService.StartupServices.ApiServices;

public interface IApiService
{
    Task<SignupResponse?> SignupAsync(SignupModel signupModel);
    Task<UserSession?> LoginAsync(LoginModel loginModel);
    Task<MainServiceResponse> GetMainServiceAsync(MainServiceStaus staus);
    Task<string> StoreServiceAsync(StoreModel storeModel);
}