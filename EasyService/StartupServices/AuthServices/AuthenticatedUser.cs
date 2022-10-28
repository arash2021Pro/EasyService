using System.Security.Claims;

namespace EasyService.StartupServices.AuthServices;

public static class AuthenticatedUser
{
    public static int GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userId = 0;
        try
        {
            userId = int.Parse(claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
        catch (Exception e)
        {
            
        }
        return userId;
    }
}