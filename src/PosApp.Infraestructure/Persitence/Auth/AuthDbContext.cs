using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PosApp.Infraestructure.Persitence.Auth;


/// <summary>
/// DbContext for store users information
/// </summary>
internal class AuthDbContext : IdentityDbContext<AppUser>
{
    //Requiered for EF core
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
