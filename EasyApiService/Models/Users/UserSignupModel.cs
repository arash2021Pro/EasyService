using System.ComponentModel.DataAnnotations;

namespace EasyApiService.Models.Users;

public class UserSignupModel
{
    [Required(ErrorMessage = "phoneNumber is required")]
    public string? PhoneNumber{ get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
    public string?IsAdmin { get; set; }
    public bool IsGender { get; set; }
}