using Inventario.Maui.ViewModels;

namespace Inventario.Maui.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
        BindingContext = App.Current.Servicios.GetRequiredService<LoginViewModel>();
        InitializeComponent();
	}
}