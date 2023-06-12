using Inventario.Models.Dominio.Ventas;

namespace Inventario.BL.Funcionalidades.Ventas.Interfaces
{
    public interface IRepositorioDeVentas
    {
        public void CreeUnaVenta(Venta venta);

        public void AñadaUnDetalleAlaVenta(VentaDetalle item);

        public void ElimineUnDetalleDeLaVenta(VentaDetalle item);

        public void ApliqueUnDescuento();

        public void TermineLaVenta();

        public IEnumerable<Venta> ListeLasVentas();
        public IEnumerable<Venta> ListeLasVentasPorId(int id);
        public IEnumerable<Venta> ListeLasVentasPorUsuario();


    }


}
