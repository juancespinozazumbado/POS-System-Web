//using Inventario.Models.Dominio.Productos;
//using Inventario.WebApp.Areas.Productos.Models;
//using Inventario.WebApp.Areas.Productos.Servicio.Iservicio;
//using Inventario.WebApp.Models.ApiOpciones;
//using Inventario.WebApp.Models.Dto;
//using Inventario.WebApp.Servicios;
//using Inventario.WebApp.Servicios.IServicio;
//using Newtonsoft.Json;
//using static Inventario.WebApp.Models.ApiOpciones.ApiOPciones;

//namespace Inventario.WebApp.Areas.Productos.Servicio
//{
//    public class ServicioDeAjustesDeInventario : IservicioDeAjustesDeInventario
//    {
//        private readonly IServicioBase _servicioBase;
//        public ServicioDeAjustesDeInventario(IServicioBase servicioBase)
//        {
//            _servicioBase = servicioBase;
//        }
//        public async Task<bool> AgegarAjusteDeInventario(int id, AjusteDto ajusteDeInventario)
//        {
//            var respuesta = await _servicioBase.SendAsync(new ConsultaRestDto()
//            {
//                MetodoRest = MetodoREST.POST,
//                TipoDeContenido = TipoDeContenido.Json,
//                Cuerpo = ajusteDeInventario,
//                URL = ApiOPciones.API_URL + $"/AjustesDeInventarios/{id}"

//            });

//            return respuesta.esSucces;
            
//        }

//        public async Task<AjusteDeInventario> ObtenerAjustePorId(int id, int id_detalle)
//        {
//            var respuesta = await _servicioBase.SendAsync(new ConsultaRestDto()
//            {
//                MetodoRest = MetodoREST.GET,
//                TipoDeContenido = TipoDeContenido.Json,
//                URL = ApiOPciones.API_URL + $"/AjustesDeInventarios/{id}/Detalle/?id_detalle={id_detalle}"

//            });

//           var inventario = JsonConvert.DeserializeObject<AjusteDeInventario>(Convert.ToString(respuesta.Respuesta));
//            return inventario == null ? null : (AjusteDeInventario)inventario;
//        }

        
//    }
//}
