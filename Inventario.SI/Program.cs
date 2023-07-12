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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string cadena = builder.Environment.IsDevelopment() ? "Local" : "Produccion";


var conectionString = builder.Configuration.GetConnectionString(cadena);


// Agrega los servicios

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
builder.Services.AddScoped<IServicioDeJWT, ServicioDeJWT>();
builder.Services.AddScoped<IServicioDeAutenticacion, ServicioDeAutenticacion>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // validate the token based on the key we have provided inside appsettings.development.json JWT:Key
            ValidateIssuerSigningKey = true,
            // the issuer singning key based on JWT:Key
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ApiSettings:JwtOptions:secret"])),
            // the issuer which in here is the api project url we are using
            ValidIssuer = builder.Configuration["ApiSettings:JwtOptions:Issuer"],
            // validate the issuer (who ever is issuing the JWT)
            ValidateIssuer = true,
            // don't validate audience (angular side)
            ValidateAudience = false
        };
    });

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
