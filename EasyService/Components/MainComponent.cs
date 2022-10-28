using EasyService.Models;
using EasyService.StartupServices.ApiServices;
using Microsoft.AspNetCore.Components;

namespace EasyService.Components;

public class MainComponent:ComponentBase
{
    [Inject] private NavigationManager _navigationManager { get; set; }
    [Inject] private IApiService _apiService { get; set; }

    public async Task UseMainServiceAsync(MainServiceStaus staus)
    {
        
    }
}