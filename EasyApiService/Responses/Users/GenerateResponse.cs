namespace EasyApiService.Responses.Users;

public static class GenerateResponse
{
    public static SignupResponse GenerateSignupResponse(bool IsSucessful, int StatusCode, string? ResponseMsg,
        bool IsExists,bool IsCreated)
    {
        var response = new SignupResponse()
        {
            IsSuccessful = IsSucessful, StatusCode = StatusCode, ResponseMessage = ResponseMsg, IsUserExists = IsExists,
            IsCreated = IsCreated
        };
        return response;
    }
}