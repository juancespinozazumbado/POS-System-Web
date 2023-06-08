using Inventario.Models.Dominio.Ventas;

namespace Inventario.BL.Funcionalidades.Ventas.Interfaces
{
    public interface IRepositorioDeVentas
    {
        public void CrearUnaVenta(Venta venta);

        public void AñadirDetalleDeVenta(VentaDetalle item);

        public void EliminarUnDetalleDeVenta(VentaDetalle item);

        public void AplicarUnDescuento();

        public void TerminarUnaVenta();

        public IEnumerable<Venta> ListarVentas();
        public IEnumerable<Venta> ListarVentasPorId(int id);
        public IEnumerable<Venta> ListarVentasPorusUario();


    }


}
