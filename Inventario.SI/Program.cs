using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Usuarios.Interfaces;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.BL.Funcionalidades.Ventas.Interfaces;
using Inventario.BL.ServicioEmail;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos;
using Inventario.SI.Servicios.Autenticacion;
using Inventario.SI.Servicios.Autenticacion.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<InventarioDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<AplicationUser>()
    .AddEntityFrameworkStores<InventarioDBContext>()
    .AddSignInManager<SignInManager<AplicationUser>>()
    .AddUserManager<UserManager<AplicationUser>>()
    .AddDefaultTokenProviders(); 
    


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();



builder.Services.AddTransient<IRepositorioDeUsuarios, RepositorioDeUsuarios>();
builder.Services.AddTransient<IRepositorioDeInventarios, ReporitorioDeInventarios>();
builder.Services.AddTransient<IRepositorioDeAjusteDeInventarios, RepositorioDeAjusteDeInventario>();
builder.Services.AddTransient<IRepositorioDeVentas, RepositorioDeVentas>();
builder.Services.AddTransient<IrepositorioDeAperturaDeCaja, RepositorioDeAperturaDeCaja>();

builder.Services.AddTransient<IServicioDeEmail, ServicioDeEmail>();


builder.Services.AddScoped<IServicioDeAutenticacion, ServicioDeAutenticacion>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
