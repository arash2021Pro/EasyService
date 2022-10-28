using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json.Serialization;
using EasyService.Models;
using EasyService.Responses;
using EasyService.StartupServices.AuthServices;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace EasyService.StartupServices.ApiServices;

public class ApiService:IApiService
{
    private HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<SignupResponse?> SignupAsync(SignupModel signupModel)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync($"Api/User/Signup", signupModel);
        var content = httpResponse.Content.ReadAsStringAsync().Result;
        var response = JsonConvert.DeserializeObject<SignupResponse>(content);
        return response;
    }
    
    public async Task<UserSession?> LoginAsync(LoginModel loginModel)
    {
        var response = await _httpClient.PostAsJsonAsync($"Api/User/Login", loginModel);
        if (response.IsSuccessStatusCode)
        {
            var userSession = await response.Content.ReadFromJsonAsync<UserSession>();
            return userSession;
        }
        return null;
    }

    public async Task<MainServiceResponse> GetMainServiceAsync(MainServiceStaus staus)
    {
        var httpResponse = await _httpClient.PostAsJsonAsync($"Api/User/GetMainService", staus);
        var content = httpResponse.Content.ReadAsStringAsync().Result;
        var response = JsonConvert.DeserializeObject<MainServiceResponse>(content);
        return response!;
    }

    public async Task<string> StoreServiceAsync(StoreModel storeModel)
    {
        var response = await _httpClient.PostAsJsonAsync($"Api/User/StoreService", storeModel);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return "Successfully Saved";
        }
        return "Something went wrong";
    }
}