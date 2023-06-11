using Inventario.BL.Funcionalidades.Ventas.Interfaces;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Ventas;

namespace Inventario.BL.Funcionalidades.Ventas
{

    public class RepositorioDeVentas : IRepositorioDeVentas
    {
        private readonly InventarioDBContext _dbContext;
        public RepositorioDeVentas(InventarioDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void ApliqueUnDescuento()
        {
            throw new NotImplementedException();
        }

        public void AñadaUnDetalleAlaVenta(VentaDetalle item)
        {
            throw new NotImplementedException();
        }

        public void RegistreElInicioDeLaVenta(Venta venta)
        {
            _dbContext.Ventas.Add(venta);
            _dbContext.SaveChanges();

        }
        public void CreeUnaVenta(Venta venta)
        {



        }

        public void ElimineUnDetalleDeLaVenta(VentaDetalle item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Venta> ListeLasVentas()
        {
            return _dbContext.Ventas.ToList();
        }

        public IEnumerable<Venta> ListeLasVentasPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Venta> ListeLasVentasPorUsuario()
        {
            throw new NotImplementedException();
        }

        public void TermineLaVenta()
        {
            throw new NotImplementedException();
        }
    }
}
