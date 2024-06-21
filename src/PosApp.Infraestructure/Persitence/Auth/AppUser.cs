using Microsoft.AspNetCore.Identity;

namespace PosApp.Infraestructure.Persitence.Auth;

public class AppUser : IdentityUser
{
    public string? ProfileImageUrl {get; set;}
}