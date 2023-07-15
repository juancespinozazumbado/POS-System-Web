using Inventario.Maui.Views;

namespace Inventario.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(InventarioPage), typeof(InventarioPage));
            Routing.RegisterRoute(nameof(ResumenDeVentas), typeof(ResumenDeVentas));
            Routing.RegisterRoute(nameof(Menu), typeof(Menu));





        }
    }
}