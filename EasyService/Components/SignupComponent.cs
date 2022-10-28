using EasyService.Models;
using EasyService.StartupServices.ApiServices;
using Microsoft.AspNetCore.Components;

namespace EasyService.Components;

public class SignupComponent:ComponentBase
{
    [Inject]public IApiService ApiService { get; set; }
    
    [Inject]public NavigationManager NavigationManager { get; set; }
    public string?Message { get; set; }
    public async Task SignupAsync(SignupModel signupModel)
    {
        var isCompleted = true;
        var response = await ApiService.SignupAsync(signupModel);
        if (response.IsUserExists)
        {
            Message = "This user already exists";
            isCompleted = false;
        }
        if (isCompleted)
        {
            if (response.IsCreated && response.IsSuccessful)
            {
                NavigationManager.NavigateTo("/Login");
            }
        }
    }
}