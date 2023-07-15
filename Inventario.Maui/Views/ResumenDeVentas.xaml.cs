using Inventario.Maui.ViewModels;

namespace Inventario.Maui.Views;

public partial class ResumenDeVentas : ContentPage
{
	public ResumenDeVentas()
	{
        BindingContext = App.Current.Servicios.GetRequiredService<VentasViewModel>();
        InitializeComponent();
	}
}