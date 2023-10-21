using Inventario.Models.Dominio.Productos;
using Inventario.WebApp.Areas.Autenticacion.Models;
using Inventario.WebApp.Areas.Productos.Models;
using Inventario.WebApp.Areas.Productos.Servicio.Iservicio;
using Inventario.WebApp.Models.ApiOpciones;
using Inventario.WebApp.Models.Dto;
using Inventario.WebApp.Servicios.IServicio;
using Newtonsoft.Json;
using static Inventario.WebApp.Models.ApiOpciones.ApiOPciones;

namespace Inventario.WebApp.Areas.Productos.Servicio
{
    public class ServicioDeInventario : IServicioDeInventario
    {
        private readonly IServicioBase _servicioBase;

        public ServicioDeInventario(IServicioBase servicioBase)
        {
            _servicioBase = servicioBase;
        }

        public async Task<bool> AgregarInvenatrio(InventarioDto request)
        {
            var respuesta = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.POST,
                URL = ApiOPciones.API_URL + $"/Inventarios",
                Cuerpo = request,
                TipoDeContenido  = TipoDeContenido.Json

            });

            return respuesta.esSucces;
        }

        public async Task<bool> EditarInvenatrio(int id, InventarioDto request)
        {
            var respuesta = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.PUT,
                URL = ApiOPciones.API_URL + $"/Inventarios/{id}",
                Cuerpo = request,
                TipoDeContenido = TipoDeContenido.Json

            });
            return respuesta.esSucces;
        }

        public async Task<bool> EliminarInventario(int id)
        {
            var respuesta = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.PUT,
                URL = ApiOPciones.API_URL + $"/Inventarios/{id}",
                TipoDeContenido = TipoDeContenido.Json

            });
            return respuesta.esSucces;
            
        }

        public async Task<Inventarios?> InvenatrioPorId(int id)
        {
            var respuesta = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.GET,
                URL = ApiOPciones.API_URL + $"/Inventarios/{id}",
                TipoDeContenido = TipoDeContenido.Json
            });

           var inventario = JsonConvert.DeserializeObject<Inventarios>(Convert.ToString(respuesta.Respuesta));
            return inventario == null ? null : (Inventarios)inventario;
        }

        public async Task<List<Inventarios>> InventariosPorNombre(string nombre)
        {
             var respuesta = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.GET,
                URL = ApiOPciones.API_URL + $"/Inventarios/Nombre?Nombre={nombre}",
                 TipoDeContenido = TipoDeContenido.Json

             });

            var inventario = JsonConvert.DeserializeObject<List<Inventarios>>(Convert.ToString(respuesta.Respuesta));
            return inventario == null ? null : (List<Inventarios>)inventario;
        }

        public async Task<List<Inventarios>> ListarInventarios()
        {
            var respuesta = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.GET,
                URL = ApiOPciones.API_URL + $"/Inventarios",
                TipoDeContenido = TipoDeContenido.Json

            });

            var inventario = JsonConvert.DeserializeObject<List<Inventarios>>(Convert.ToString(respuesta.Respuesta));
            return inventario == null ? null : (List<Inventarios>)inventario;
        }
    }


}
