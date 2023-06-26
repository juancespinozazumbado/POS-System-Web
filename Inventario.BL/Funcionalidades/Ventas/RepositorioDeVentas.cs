using Inventario.BL.Funcionalidades.Ventas.Interfaces;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Ventas;
using Microsoft.EntityFrameworkCore;

namespace Inventario.BL.Funcionalidades.Ventas
{

    public class RepositorioDeVentas : IRepositorioDeVentas
    {
   
      
        private readonly InventarioDBContext _dbContext;

        public RepositorioDeVentas(InventarioDBContext dbContext)
        {
            _dbContext = dbContext;
           

        }
        public void AñadaUnDetalleAlaVenta(int idVenta, VentaDetalle item)
        {

            Venta venta = ObtengaUnaVentaPorId(idVenta);
            if (!LaVentaEstaTerminada(venta))
            {
                venta.VentaDetalles.Add(item);
                _dbContext.Ventas.Update(venta);
                _dbContext.SaveChanges();
            }
        }

        public void CreeUnaVenta(Venta venta)
        {
            _dbContext.Ventas.Add(venta);
            _dbContext.SaveChanges();


        }
        public void ElimineUnDetalleDeLaVenta(int idVenta, VentaDetalle item)
        {
            Venta venta = ObtengaUnaVentaPorId(idVenta);
            if (!LaVentaEstaTerminada(venta))
            {
                venta.VentaDetalles.Remove(item);

                _dbContext.Ventas.Update(venta);
                _dbContext.SaveChanges();

            }

        }

        public IEnumerable<Venta> ListeLasVentas()
        {
            return _dbContext.Ventas.Include(v => v.VentaDetalles).ToList();
        }

        public Venta? ObtengaUnaVentaPorId(int id)
        {
            return _dbContext.Ventas.Include(v => v.VentaDetalles).ThenInclude(d => d.Inventarios)
                             .ToList().Find(v => v.Id == id);
        }

        public IEnumerable<Venta> ListeLasVentasPorUsuario(string userId)
        {
            return _dbContext.Ventas.Include(v => v.VentaDetalles).ThenInclude(d => d.Inventarios)
                             .ToList().Where(v => v.UserId == userId);
        }

        public void TermineLaVenta(int id)
        {
            Venta venta = ObtengaUnaVentaPorId(id);
            if (!LaVentaEstaTerminada(venta))
            {
                if (venta.VentaDetalles.Count == 0) throw new NotImplementedException();

                foreach (VentaDetalle item in venta.VentaDetalles)
                {
                    venta.SubTotal += item.Monto;
                    //Inventarios inventario = item.Inventarios;
                     //item.MontoDescuento = item.Monto* venta.PorcentajeDesCuento / 100;
                    _dbContext.SaveChanges();
                }
                venta.MontoDescuento = venta.SubTotal * venta.PorcentajeDesCuento / 100;
                venta.Total = venta.SubTotal - venta.MontoDescuento;
                venta.Estado = EstadoVenta.Terminada;
                _dbContext.Update(venta);
                _dbContext.SaveChanges();

            }

        }


        public IEnumerable<Venta> ListeLasVentasPorFecha(DateTime fecha_inicial, DateTime fecha_final)
        {
            var ventas = from venta in ListeLasVentas()
                         where venta.Fecha >= fecha_inicial
                         || venta.Fecha <= fecha_final
                         select venta;
            return ventas;
        }

        public void ApliqueUnDescuento(int id, int decuento)
        {
            Venta venta = ObtengaUnaVentaPorId(id);
            if (!LaVentaEstaTerminada(venta))
            {

                venta.PorcentajeDesCuento = decuento;
                decimal porcejtaje = venta.PorcentajeDesCuento;
                decimal porcentajeDescuento = porcejtaje  / 100;
                foreach (var item in venta.VentaDetalles)
                {
                    item.Monto = item.Precio * item.Cantidad;
                    item.MontoDescuento = 0;
                    item.MontoDescuento = item.Monto * porcentajeDescuento;
                    item.Monto -= item.MontoDescuento;

                }
                _dbContext.Update(venta);
                _dbContext.SaveChanges();
            }


        }

        public void EstablescaElTipoDePago(int id, TipoDePago tipoDePago)
        {
            Venta venta = ObtengaUnaVentaPorId(id);
            if (!LaVentaEstaTerminada(venta))
            {
                venta.TipoDePago = tipoDePago;
                _dbContext.Update(venta);
                _dbContext.SaveChanges();
            }

        }


        private bool LaVentaEstaTerminada(Venta venta)
        {
            return venta.Estado == EstadoVenta.Terminada;
        }

       
    }
}
