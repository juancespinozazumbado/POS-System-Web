using Inventario.Maui.ViewModels;

namespace Inventario.Maui
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            BindingContext = App.Current.Servicios.GetRequiredService<InventarioViewModel>();
            InitializeComponent();
        }

    }
}