using Inventario.Maui.ViewModels;

namespace Inventario.Maui.Views;

public partial class Menu : ContentPage
{
	public Menu()
	{
        BindingContext = App.Current.Servicios.GetRequiredService<MenuViewModel>();
        InitializeComponent();
	}
}