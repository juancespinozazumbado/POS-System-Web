using Azure.Core;
using Inventario.WebApp.Areas.Autenticacion.Models;
using Inventario.WebApp.Models.ApiOpciones;
using Inventario.WebApp.Models.Dto;
using Inventario.WebApp.Servicios.IServicio;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Policy;
using static Inventario.WebApp.Models.ApiOpciones.ApiOPciones;

namespace Inventario.WebApp.Areas.Autenticacion.Servicio
{
    public class ServicioDeAutenticaicion : IServicioDeAutenticacion
    {
        private readonly IServicioBase _servicioBase;

        public ServicioDeAutenticaicion(IServicioBase servicioBase)
        {
            _servicioBase = servicioBase;
        }
        public async Task<RespuestaRestDto?> LoginAsync(LoginDto request)
        {
            return await _servicioBase.SendAsync(
                new ConsultaRestDto()
                {
                    MetodoRest = MetodoREST.POST,
                    Cuerpo = request,
                    URL = ApiOPciones.API_URL + $"/Autenticacion/Login",
                    TipoDeContenido = TipoDeContenido.Json
                }, conBearer : false);
        }

        public async Task<RespuestaRestDto?> Registro(RegistroDto request)
        {
            var resultado = await _servicioBase.SendAsync(
                 new ConsultaRestDto()
                 {
                     MetodoRest = MetodoREST.POST,
                     Cuerpo = request,
                     URL = ApiOPciones.API_URL + $"/Autenticacion/Registro",
                     TipoDeContenido = TipoDeContenido.Json
                 }, conBearer: false);


            return new RespuestaRestDto() { Respuesta = resultado };
        }

        public async Task<RespuestaRestDto?> CambiarClave(CambioDeClaveDto request)
        {
            var resultado = await _servicioBase.SendAsync(
                 new ConsultaRestDto()
                 {
                     MetodoRest = MetodoREST.POST,
                     Cuerpo = request,
                     URL = ApiOPciones.API_URL + $"/Cuenta/CambioDeClave",
                     TipoDeContenido = TipoDeContenido.Json
                 });

            return resultado;
        }




        public async Task<RespuestaRestDto> Obtenerusuario(string nombre)
        {
            var respuesta = await _servicioBase.SendAsync(
                 new ConsultaRestDto()
                 {
                     MetodoRest = MetodoREST.GET,
                     URL = ApiOPciones.API_URL + $"/Cuenta/Usuario?correo={nombre}",
                     TipoDeContenido = TipoDeContenido.Json
                 });
            return respuesta;
        }
    }
}
