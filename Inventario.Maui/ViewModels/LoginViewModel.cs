
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inventario.Maui.Modelos.Dtos;
using Inventario.Maui.Servicios.Iservicios;
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

        public LoginViewModel()
        {
            _proveedorDeToken = App.Current.Servicios.GetRequiredService<IProveedorDeToken>();
            _servicioDeAutenticacion = App.Current.Servicios.GetRequiredService<IServicioDeAutenticacion>();
        }

        [RelayCommand]
        public async void Login()
        {

            ValidateAllProperties();
            GetErrors(nameof(Correo)).ToList().ForEach(e => Errores.Add($"Correo: {e}"));
            GetErrors(nameof(Contraseña)).ToList().ForEach(e => Errores.Add($"Contraseña: {e}"));
            
            if (correo != "" && contraseña != "")
            {
                var request = new LoginRequestDto()
                {
                    Correo = correo,
                    Contraseña = contraseña

                };

                var respuesta = await _servicioDeAutenticacion.LoginAsync(request);
                if(respuesta != null)
                {
                    _proveedorDeToken.EscribirToken(respuesta.Token);


                }
                
            }

            
        }
        
    }
}
