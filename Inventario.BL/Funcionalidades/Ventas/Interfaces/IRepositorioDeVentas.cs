using Inventario.Models.Dominio.Ventas;

namespace Inventario.BL.Funcionalidades.Ventas.Interfaces
{
    public interface IRepositorioDeVentas
    {
        public void CreeUnaVenta(Venta venta);

        public void AñadaUnDetalleAlaVenta(int id, VentaDetalle item);
        public void ElimineUnDetalleDeLaVenta(int id, VentaDetalle item);

        public void ApliqueUnDescuento(int id, int decuento);

        public void TermineLaVenta(int id);

        public IEnumerable<Venta> ListeLasVentas();
        public Venta ObtengaUnaVentaPorId(int id);

        public IEnumerable<Venta> ListeLasVentasPorFecha(DateTime fecha_inicial, DateTime fecha_final);
        public IEnumerable<Venta> ListeLasVentasPorUsuario(string userId);


    }


}
