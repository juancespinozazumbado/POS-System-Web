using Inventario.Maui.Servicios;
using Inventario.Maui.Servicios.Iservicios;
using Inventario.Maui.ViewModels;
using Inventario.Maui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Inventario.Maui
{
    public partial class App : Application
    {
        public static  new App Current => (App)Application.Current;
        public IServiceProvider Servicios { get; }
        public App()
        {

            ServiceCollection servicios = new ServiceCollection();
            Servicios = ConfigurarServicios(servicios);

            InitializeComponent();

            MainPage = new AppShell();
        }

        public static IServiceProvider ConfigurarServicios(ServiceCollection servicios)
        {
            //servicios base 
            servicios.AddSingleton<IServicioBase, ServicioBase>();
            servicios.AddSingleton<IProveedorDeToken, ProveedorDeToken>();

          
            servicios.AddHttpClient();
            servicios.AddHttpClient<IServicioDeAutenticacion, ServicioDeAutenticacion>();


            //servicios
            servicios.AddSingleton<IServicioDeAutenticacion, ServicioDeAutenticacion>();
            servicios.AddSingleton<IservicioDeVentas, ServicioDeVentas>();  
            servicios.AddSingleton<IServicioDeInventario, ServicioDeInventario>();

            //ViewModels
            servicios.AddTransient<LoginViewModel>();
            servicios.AddScoped<InventariosViewModel>();
            servicios.AddScoped<VentasViewModel>();
            servicios.AddScoped<MenuViewModel>();

            //Viwes
            servicios.AddSingleton<LoginPage>();
            servicios.AddSingleton<InventarioPage>();
            servicios.AddSingleton<ResumenDeVentas>();
            servicios.AddSingleton<Menu>();



            return servicios.BuildServiceProvider();
        }
    }
}