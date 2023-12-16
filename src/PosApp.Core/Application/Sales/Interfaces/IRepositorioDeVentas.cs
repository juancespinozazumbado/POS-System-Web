using Inventario.Models.Dominio.Ventas;

namespace Inventario.BL.Funcionalidades.Ventas.Interfaces
{
    public interface IRepositorioDeVentas
    {
        public Task<bool> CreeUnaVenta(Venta venta);

        public Task<bool> AñadaUnDetalleAlaVenta(int id, VentaDetalle item);
        public Task<bool> ElimineUnDetalleDeLaVenta(int id, VentaDetalle item);

        public Task<bool> ApliqueUnDescuento(int id, int decuento);

        public Task<bool> TermineLaVenta(int id);

        public Task<List<Venta>> ListeLasVentas();
        public Task<Venta> ObtengaUnaVentaPorId(int id);

        public  Task<List<Venta>> ListeLasVentasPorFecha(DateTime fecha_inicial, DateTime fecha_final);
        public Task<List<Venta>> ListeLasVentasPorUsuario(string userId);

        public  Task<bool> EstablescaElTipoDePago(int id, TipoDePago tipoDePago);


    }


}
