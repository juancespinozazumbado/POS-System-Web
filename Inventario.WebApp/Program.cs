using Inventario.DA.Database;
using Inventario.Models.Dominio.Usuarios;
using Inventario.WebApp.Areas.Autenticacion.Servicio;
using Inventario.WebApp.Areas.Productos.Servicio;
using Inventario.WebApp.Areas.Productos.Servicio.Iservicio;
using Inventario.WebApp.Areas.Ventas.Servicio;
using Inventario.WebApp.Areas.Ventas.Servicio.IServicio;
using Inventario.WebApp.Models.ApiOpciones;
using Inventario.WebApp.Servicios;
using Inventario.WebApp.Servicios.IServicio;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string opcion = builder.Environment.IsDevelopment() ? "Local" : "Produccion";
var CadenaDeConeccion = builder.Configuration.GetConnectionString(opcion);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IServicioDeAutenticacion, ServicioDeAutenticaicion>();
builder.Services.AddHttpClient<IServicioDeInventario, ServicioDeInventario>();
builder.Services.AddHttpClient< IservicioDeAjustesDeInventario, ServicioDeAjustesDeInventario >();
builder.Services.AddHttpClient<IServicioDeVentas, ServicioDeVentas>();
builder.Services.AddHttpClient<IservicioDeAperturaDeCaja, ServicioDeAperturaDeCaja>();
builder.Services.AddHttpClient<IServicioBase, ServicioBase>();


builder.Services.AddScoped<IServicioDeInventario, ServicioDeInventario>();
builder.Services.AddScoped<IservicioDeAjustesDeInventario, ServicioDeAjustesDeInventario>();
builder.Services.AddScoped<IServicioDeVentas, ServicioDeVentas>();
builder.Services.AddScoped<IservicioDeAperturaDeCaja, ServicioDeAperturaDeCaja>();  
builder.Services.AddScoped<IServicioDeAutenticacion, ServicioDeAutenticaicion>();
builder.Services.AddScoped<IProveedorDeToken, ProveedorDeToken>();
builder.Services.AddScoped<IServicioBase, ServicioBase>();  

ApiOPciones.API_URL = builder.Configuration[$"UrlServicio:{opcion}"];

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = new PathString("/Autenticacion/Cuenta/Login");
        //options.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.Configure<IdentityOptions>(options => options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);


builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
app.MapControllerRoute(

    name: "default",
    pattern: "{controller=Home}/{action=Index}");
//app.MapRazorPages();

app.Run();
