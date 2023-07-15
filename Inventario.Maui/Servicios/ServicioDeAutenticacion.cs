
using Inventario.Maui.Modelos;
using Inventario.Maui.Modelos.Dtos;
using Inventario.Maui.Servicios.Iservicios;
using Newtonsoft.Json;
using System;
using static Inventario.Maui.Modelos.ConfiguracionApi;

namespace Inventario.Maui.Servicios
{
    public class ServicioDeAutenticacion : IServicioDeAutenticacion
    {
        private readonly IServicioBase _servicioBase;
        private readonly IProveedorDeToken _proveedorDeToken;

        public ServicioDeAutenticacion()
        {
            _servicioBase =  App.Current.Servicios.GetRequiredService<IServicioBase>();
            _proveedorDeToken = App.Current.Servicios.GetRequiredService<IProveedorDeToken>();  
        }
        public async Task<LoginRespuestaDto> LoginAsync(LoginRequestDto request)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRest()
            {

                MetodoRest = MetodoREST.POST,
                Cuerpo = request,
                URL = ConfiguracionApi.API_URL + $"/Autenticacion/Login",
                TipoDeContenido = TipoDeContenido.Json
            }, conBearer: false);

            if (resultado.esSucces)
            {
                LoginRespuestaDto? respuestaLogin =
                JsonConvert.DeserializeObject<LoginRespuestaDto>(Convert.ToString(resultado.Respuesta));

                
                return respuestaLogin;
            }
            return null;

            
        }
    }
}
