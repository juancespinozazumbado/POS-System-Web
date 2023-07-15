
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventario.Maui.Servicios.Iservicios;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Maui.ViewModels
{
    partial class MenuViewModel : ObservableObject
    {
        private readonly IProveedorDeToken _proveedorDeToken;

        public MenuViewModel() => _proveedorDeToken = App.Current.Servicios.GetRequiredService<IProveedorDeToken>();



        [RelayCommand]
        public async  void VerVentas()
        {
            await Task.Delay(1000);
            //await Shell.Current.Navigation.PushAsync(new Views.ResumenDeVentas());
            await Shell.Current.GoToAsync($"{nameof(Views.ResumenDeVentas)}");
        }

        [RelayCommand]
        public async void VerInventarios()
        {
            await Task.Delay(1000);
            //await Shell.Current.Navigation.PushAsync(new Views.InventarioPage());
            await Shell.Current.GoToAsync($"{nameof(Views.InventarioPage)}");

        }

        [RelayCommand]
        public async  void LogOut()
        {
            _proveedorDeToken.LimpiarToken();

            SecureStorage.Default.Remove("Id_Usuario");
            SecureStorage.Default.Remove("Nombre_Usuario");

            await Task.Delay(1000);

            //await Shell.Current.Navigation.PushAsync(new Views.LoginPage());
            await Shell.Current.GoToAsync($"{nameof(Views.LoginPage)}");

        }



    }
}
