using CoreBussiness.BussinessEntity.AppServices;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.ManagerService;
using CoreBussiness.UnitOfWork;
using EasyApiService.Models.MainServices;
using EasyApiService.Models.Users;
using EasyApiService.Responses.Users;
using EasyApiService.StartupServices.AuthenticationServices;
using EasyApiService.StartupServices.MainServices;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace EasyApiService.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class UserController:ControllerBase
{
    private IUnitOfWork _work;
    private IManagerService _managerService;
    private IJwtAuthManagerService _jwtAuthManagerService;
    private IMainService _mainService;
    public UserController(IUnitOfWork work, IManagerService managerService, IJwtAuthManagerService jwtAuthManagerService, IMainService mainService)
    {
        _work = work;
        _managerService = managerService;
        _jwtAuthManagerService = jwtAuthManagerService;
        _mainService = mainService;
    }

    [HttpPost]
    public async Task<ActionResult<SignupResponse>> Signup([FromBody] UserSignupModel? signupModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _managerService.UserService.IsUserExistsAsync(signupModel!.PhoneNumber);
            if (result)
            {
                var response = GenerateResponse.GenerateSignupResponse(true, 200, "already exists", true,false);
                return response;
            }
            var user = new User
            {
                Password = signupModel.Password,
                PhoneNumber = signupModel.PhoneNumber,
                IsAdmin = "user",
                IsGender = signupModel.IsGender
            };
            await _managerService.UserService.SignupUserAsync(user);
            var saveChanges = await _work.SaveChangesAsync();
            if (saveChanges > 0)
            {
                var response = GenerateResponse.GenerateSignupResponse(true, 200, "Successful", false, true);
                return response;
            }
            var responseError = GenerateResponse.GenerateSignupResponse(false, 500, "somehting went wrong", false, false);
            return responseError;
        }
        var responseValidation = GenerateResponse.GenerateSignupResponse(false, 500, "somehting went wrong", false, false);
        return responseValidation;
    }
    
    [HttpPost]
    public async Task<ActionResult<UserSession>> Login([FromBody] LoginModel loginModel)
    {
        var userSession = await _jwtAuthManagerService.GenerateJwtAsync(loginModel.PhoneNumber, loginModel.Password);
        if (userSession is null )
            return NotFound();
        return userSession;
    }

    [HttpPost]
    public async Task<ActionResult<MainServiceResponse>> GetMainService([FromBody] MainServiceStatus status )
    {
        switch (status)
        {
           case MainServiceStatus.phone:
               return new MainServiceResponse() {PhoneNumber = _mainService.GeneratePhoneNumber(7),Code = 0};
           case MainServiceStatus.gmail:
               return new MainServiceResponse() {Gmail = _mainService.GenerateGmail(10),Code = 1};
           case MainServiceStatus.password:
               return new MainServiceResponse() {Password = _mainService.GeneratePassword(10),Code = 2};
        }
        return NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> StoreService([FromBody]StoreModel storeModel)
    {
        var appService = new AppService() {Gmail = null, Password = null, PhoneNumber = null};
        if (storeModel.Code == 0)
        {
            appService.PhoneNumber = storeModel.Data;
            appService.UserId = storeModel.UserId;
        }
        else if (storeModel.Code == 1)
        {
            appService.Gmail = storeModel.Data;
            appService.UserId = storeModel.UserId;
        }
        else if(storeModel.Code == 2)
        {
            appService.Password = storeModel.Data;
            appService.UserId = storeModel.UserId;
        }
        await _managerService.AppService.AddAppService(appService);
        var saveChanges = await _work.SaveChangesAsync();
        if (saveChanges > 0)
        {
            return Ok();
        }
        return BadRequest();
    }

}