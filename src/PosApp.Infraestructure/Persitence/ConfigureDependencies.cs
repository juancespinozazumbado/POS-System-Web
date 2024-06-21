using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PosApp.Dommain.Interfaces;
using PosApp.Infraestructure.Persitence.ApplicationContex;
using PosApp.Infraestructure.Persitence.Auth;
using PosApp.Infraestructure.Persitence.Repository;


namespace PosApp.Infraestructure.Persitence;

public static class ConfigureDependencies
{
    public static IServiceCollection ConfigurePersistenceDependencies(this IServiceCollection services,IConfiguration configuration)
    {

        var databaseOption = configuration.GetRequiredSection("DatabaseOptions")["local"] ?? "";
        if (databaseOption.Equals("true") || databaseOption == string.Empty)
        {
            services.AddDbContext<ApplicationDbContex>(options => options.UseInMemoryDatabase("MemoryDb"));
            services.AddDbContext<AuthDbContext>(options => options.UseInMemoryDatabase("MemoryDb"));

        }else
        {
            var appDbConnectionString = configuration.GetConnectionString("Aplication");
            var AuthDbConnectionString = configuration.GetConnectionString("Auth");
            services.AddDbContext<ApplicationDbContex>(options => options.UseSqlServer(appDbConnectionString));
            services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(AuthDbConnectionString));

        }

        /// Add repository layer
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));    

        return services;
    }
}
