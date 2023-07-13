using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Areas.Ventas.Modelos;
using Inventario.WebApp.Areas.Ventas.Modelos.Dtos;
using Inventario.WebApp.Areas.Ventas.Servicio.IServicio;
using Inventario.WebApp.Models.ApiOpciones;
using Inventario.WebApp.Models.Dto;
using Inventario.WebApp.Servicios.IServicio;
using Newtonsoft.Json;
using static Inventario.WebApp.Models.ApiOpciones.ApiOPciones;

namespace Inventario.WebApp.Areas.Ventas.Servicio
{
    public class ServicioDeVentas : IServicioDeVentas
    {

        private readonly IServicioBase _servicioBase;

        public ServicioDeVentas(IServicioBase servicioBase)
        {
            _servicioBase = servicioBase;   
        }

        public async Task<Venta> CreeUnaVenta(CrearVentaRequest venta)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.POST,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ApiOPciones.API_URL + $"/Ventas/nueva",
                Cuerpo = venta

            });
            if(resultado != null && resultado.esSucces)
            {
                var ventaEnProceso = JsonConvert.DeserializeObject<Venta>(Convert.ToString(resultado?.Respuesta));
                return ventaEnProceso;
            }
            return null;
        }

        public async Task<Venta> AñadaUnDetalleAlaVenta(int id, AgregarItemDeVenatRequest item)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.PUT,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ApiOPciones.API_URL + $"/Ventas/{id}/agregarItem",
                Cuerpo = item

            });
            if (resultado != null && resultado.esSucces)
            {
                var ventaEnProceso = JsonConvert.DeserializeObject<Venta>(Convert.ToString(resultado?.Respuesta));
                return ventaEnProceso;
            }
            return null;
        }

    
        public async Task<Venta> ElimineUnDetalleDeLaVenta(int id, QuitarItemDeVentaRequest item)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.PUT,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ApiOPciones.API_URL + $"/Ventas/{id}/QuitarItem",
                Cuerpo = item

            });
            if (resultado != null && resultado.esSucces)
            {
                var ventaEnProceso = JsonConvert.DeserializeObject<Venta>(Convert.ToString(resultado?.Respuesta));
                return ventaEnProceso;
            }
            return null;
        }



        public async Task<Venta> ApliqueUnDescuento(int id, AplicarDescuentoRequest cuerpo)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.PUT,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ApiOPciones.API_URL + $"/Ventas/{id}/Descuento",
                Cuerpo = cuerpo

            });
            if (resultado != null && resultado.esSucces)
            {
                var ventaEnProceso = JsonConvert.DeserializeObject<Venta>(Convert.ToString(resultado?.Respuesta));
                return ventaEnProceso;
            }
            return null;
        }


        public async Task<Venta> TermineLaVenta(int id, TerminarVentaRequest cuerpo)
        {
            var resultado = await _servicioBase.SendAsync(new ConsultaRestDto()
            {
                MetodoRest = MetodoREST.PUT,
                TipoDeContenido = TipoDeContenido.Json,
                URL = ApiOPciones.API_URL + $"/Ventas/{id}/Terminar", 
                Cuerpo = cuerpo
                

            });
            if (resultado != null && resultado.esSucces)
            {
                var ventaEnProceso = JsonConvert.DeserializeObject<Venta>(Convert.ToString(resultado?.Respuesta));
                return ventaEnProceso;
            }
            return null;
        }
    }
}
