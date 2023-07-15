
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventario.Maui.Modelos;
using Inventario.Maui.Modelos.Dtos;
using Inventario.Maui.Servicios.Iservicios;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Inventario.Maui.ViewModels
{
    public partial class LoginViewModel : ObservableValidator
    {


        private string correo;
        [Required]
        [MaxLength(25)]
        public string Correo
        {
            get { return correo; }
            set { SetProperty(ref correo, value); }
        }

        private string contraseña;
        [Required]
        [MaxLength(25)]
        public string Contraseña
        {
            get { return contraseña; }
            set { SetProperty(ref contraseña, value); }
        }



        public ObservableCollection<string> Errores { get; set; } = new();

        private readonly IServicioDeAutenticacion _servicioDeAutenticacion;
        private readonly IProveedorDeToken _proveedorDeToken;
        private readonly IservicioDeVentas _ventas;
        private readonly IServicioDeInventario _Inventarios;

        public LoginViewModel()
        {
            _proveedorDeToken = App.Current.Servicios.GetRequiredService<IProveedorDeToken>();
            _servicioDeAutenticacion = App.Current.Servicios.GetRequiredService<IServicioDeAutenticacion>();
           _ventas = App.Current.Servicios.GetRequiredService<IservicioDeVentas>();
            _Inventarios = App.Current.Servicios.GetRequiredService<IServicioDeInventario>();



        }

        [RelayCommand]
        public async void Login()
        {

            ValidateAllProperties();
            GetErrors(nameof(Correo)).ToList().ForEach(e => Errores.Add($"Correo: {e}"));
            GetErrors(nameof(Contraseña)).ToList().ForEach(e => Errores.Add($"Contraseña: {e}"));
            
            
            
                var request = new LoginRequestDto()
                {
                    Correo = correo,
                    Contraseña = contraseña

                };

                var respuesta = await _servicioDeAutenticacion.LoginAsync(request);
            if (respuesta != null)
            {
                await SecureStorage.SetAsync("Id_Usuario", respuesta.Usuario.Id);
                await SecureStorage.SetAsync("Nombre_Usuario", respuesta.Usuario.UserName);

                var usuario = await  SecureStorage.Default.GetAsync("Id_Usuario");
              

                _proveedorDeToken.EscribirToken(respuesta.Token);
                //var token = await SecureStorage.GetAsync(ConfiguracionApi.CoqueToken);

                await Task.Delay(1000);
                //await Shell.Current.Navigation.PushAsync(new Views.Menu());
                await Shell.Current.GoToAsync($"{nameof(Views.Menu)}");


            }
            else
            {
                correo = "";
                contraseña="";
            }
            

                      
        }

      
        
    }
}
