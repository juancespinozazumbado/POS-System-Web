
using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Areas.Ventas.Modelos.Dtos;
using Inventario.WebApp.Areas.Ventas.Servicio.IServicio;
using Inventario.WebApp.Models.ApiOpciones;
using Inventario.WebApp.Models.Dto;
using Inventario.WebApp.Servicios.IServicio;
using Newtonsoft.Json;
using static Inventario.WebApp.Models.ApiOpciones.ApiOPciones;

namespace Inventario.WebApp.Areas.Ventas.Servicio
{
    public class ServicioDeAperturaDeCaja : IservicioDeAperturaDeCaja
    {
        private readonly IServicioBase _servicioBase;   
        public ServicioDeAperturaDeCaja(IServicioBase servicioBase)
        {
            _servicioBase = servicioBase;       
        }
        public async Task<List<AperturaDeCaja>> AperturasDeCajaPorUsuario(string idUsuario)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.GET,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ApiOPciones.API_URL +$"/AperturasDeCaja/usuario/cajas?Id_Usuario={idUsuario}",

            });
            if (resultado != null && resultado.esSucces)
            {
                var cajas = JsonConvert.DeserializeObject<List<AperturaDeCaja>>(Convert.ToString(resultado?.Respuesta));
                return cajas == null ? null : (List<AperturaDeCaja>)cajas;
            }

            throw new NotImplementedException();
        }

        public async Task<bool> CerrarUnaAperturaDeCaja(int id, string id_usuario)
        {

            var resultado = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.PUT,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ApiOPciones.API_URL + $"/AperturasDeCaja/usuario/cajas/{id}?Id_Usuario={id_usuario}",

            });
             return resultado.esSucces;
                      
        }

        public async Task<bool> CrearUnaAperturaDeCaja(AperturadeCajaDto cuerpo)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.POST,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ApiOPciones.API_URL + $"/AperturasDeCaja/usuario/cajas",
                Cuerpo = cuerpo

            });
            return resultado.esSucces;
        }

     

        public async Task<AperturaDeCaja> ObtenerUnaAperturaDeCajaPorId(int id, string id_usuario)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.GET,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ApiOPciones.API_URL + $"/AperturasDeCaja/usuario/cajas/{id}?Id_Usuario={id_usuario}",

            });
            
                var cajas = JsonConvert.DeserializeObject<AperturaDeCaja>(Convert.ToString(resultado?.Respuesta));
                return cajas == null ? null : (AperturaDeCaja)cajas;
            
        }
    }
}
