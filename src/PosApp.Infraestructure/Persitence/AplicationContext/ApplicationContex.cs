using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace PosApp.Infraestructure.Persitence.ApplicationContex;

public class ApplicationDbContex : DbContext
{

    public ApplicationDbContex(DbContextOptions<ApplicationDbContex> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}