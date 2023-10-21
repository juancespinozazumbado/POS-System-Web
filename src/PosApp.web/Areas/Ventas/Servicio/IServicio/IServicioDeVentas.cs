using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Areas.Ventas.Modelos;
using Inventario.WebApp.Areas.Ventas.Modelos.Dtos;

namespace Inventario.WebApp.Areas.Ventas.Servicio.IServicio
{
    public interface IServicioDeVentas
    {
        public Task<Venta> CreeUnaVenta(CrearVentaRequest venta);

        public Task<Venta> AñadaUnDetalleAlaVenta(int id, AgregarItemDeVenatRequest item);
        public Task<Venta> ElimineUnDetalleDeLaVenta(int id, QuitarItemDeVentaRequest item);

        public Task<Venta> ApliqueUnDescuento(int id, AplicarDescuentoRequest cuerpo);

        public Task<Venta> TermineLaVenta(int id, TerminarVentaRequest cuerpo);
    }
}
