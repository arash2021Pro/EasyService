namespace EasyApiService.Responses.Users;

public class SignupResponse
{
    public int StatusCode { get; set; }
    public string?ResponseMessage { get; set; }
    public bool IsSuccessful { get; set; }
    public bool IsUserExists { get; set; }
    public bool IsCreated { get; set; }
}