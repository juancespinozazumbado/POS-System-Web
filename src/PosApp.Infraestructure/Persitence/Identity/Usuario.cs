using Microsoft.AspNetCore.Identity;
namespace Identity;


public class AppUser : IdentityUser
{
    public string? ProfileImageUrl {get; set;}
}