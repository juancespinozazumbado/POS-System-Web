using Inventario.BL.Funcionalidades.Ventas.Interfaces;
using Inventario.Models.Dominio.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.BL.Funcionalidades.Ventas
{
    public class RepositorioDeVentas : IRepositorioDeVentas
    {
        public void AplicarUnDescuento()
        {
            throw new NotImplementedException();
        }

        public void AñadirDetalleDeVenta(VentaDetalle item)
        {
            throw new NotImplementedException();
        }

        public void CrearUnaVenta(Venta venta)
        {
            throw new NotImplementedException();
        }

        public void EliminarUnDetalleDeVenta(VentaDetalle item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Venta> ListarVentas()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Venta> ListarVentasPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Venta> ListarVentasPorusUario()
        {
            throw new NotImplementedException();
        }

        public void TerminarUnaVenta()
        {
            throw new NotImplementedException();
        }
    }
}
