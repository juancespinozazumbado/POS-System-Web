using Inventario.Maui.ViewModels;

namespace Inventario.Maui.Views;

public partial class InventarioPage : ContentPage
{
	public InventarioPage()
	{

        BindingContext = App.Current.Servicios.GetRequiredService<InventariosViewModel>();
        InitializeComponent();
	}
}