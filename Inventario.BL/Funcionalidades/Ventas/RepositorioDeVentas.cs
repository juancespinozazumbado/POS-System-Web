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
        public async Task<bool> AñadaUnDetalleAlaVenta(int idVenta, VentaDetalle item)
        {

            Venta venta = await  ObtengaUnaVentaPorId(idVenta);
            if (!LaVentaEstaTerminada(venta))
            {
                venta.VentaDetalles.Add(item);
                _dbContext.Ventas.Update(venta);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<bool> CreeUnaVenta(Venta venta)
        {
            await _dbContext.Ventas.AddAsync(venta);
            await _dbContext.SaveChangesAsync();
            return true;


        }
        public async Task<bool> ElimineUnDetalleDeLaVenta(int idVenta, VentaDetalle item)
        {
            Venta venta = await ObtengaUnaVentaPorId(idVenta);
            if (!LaVentaEstaTerminada(venta))
            {
                venta.VentaDetalles.Remove(item);

                _dbContext.Ventas.Update(venta);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            else return false;

        }

        public async Task<List<Venta>> ListeLasVentas()
        {
            return await _dbContext.Ventas.Include(v => v.VentaDetalles).ToListAsync();
        }

        public async Task<Venta> ObtengaUnaVentaPorId(int id)
        {
            var ventas = await _dbContext.Ventas.Include(v => v.VentaDetalles).ThenInclude(d => d.Inventarios).ToListAsync();
            return ventas.Find(v => v.Id == id);
        }

        public async Task<List<Venta>> ListeLasVentasPorUsuario(string userId)
        {
            var ventas = await _dbContext.Ventas.Include(v => v.VentaDetalles).ThenInclude(d => d.Inventarios)
                             .ToListAsync();
            return ventas.Where(v => v.UserId == userId).ToList();
        }

        public async Task<bool> TermineLaVenta(int id)
        {
            Venta venta = await ObtengaUnaVentaPorId(id);
            if (!LaVentaEstaTerminada(venta))
            {
                if (venta.VentaDetalles.Count == 0) throw new NotImplementedException();

                foreach (VentaDetalle item in venta.VentaDetalles)
                {
                    venta.SubTotal += item.Monto;
                    //Inventarios inventario = item.Inventarios;
                     //item.MontoDescuento = item.Monto* venta.PorcentajeDesCuento / 100;
                    await _dbContext.SaveChangesAsync();
                }
                venta.MontoDescuento = venta.SubTotal * venta.PorcentajeDesCuento / 100;
                venta.Total = venta.SubTotal - venta.MontoDescuento;
                venta.Estado = EstadoVenta.Terminada;
                _dbContext.Update(venta);
                await _dbContext.SaveChangesAsync();
                return true;

            }else return false;
        }


        public async  Task<List<Venta>> ListeLasVentasPorFecha(DateTime fecha_inicial, DateTime fecha_final)
        {
            var ventas = from venta in await ListeLasVentas()
                         where venta.Fecha >= fecha_inicial
                         || venta.Fecha <= fecha_final
                         select venta;
            return (List<Venta>) ventas;
        }

        public async Task<bool> ApliqueUnDescuento(int id, int decuento)
        {
            Venta venta = await ObtengaUnaVentaPorId(id);
            if (!LaVentaEstaTerminada(venta))
            {

                venta.PorcentajeDesCuento = decuento;
                decimal porcejtaje = venta.PorcentajeDesCuento;
                decimal porcentajeDescuento = porcejtaje / 100;
                foreach (var item in venta.VentaDetalles)
                {
                    item.Monto = item.Precio * item.Cantidad;
                    item.MontoDescuento = 0;
                    item.MontoDescuento = item.Monto * porcentajeDescuento;
                    

                }
                _dbContext.Update(venta);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else return false;

        }

        public async Task<bool> EstablescaElTipoDePago(int id, TipoDePago tipoDePago)
        {
            Venta venta = await ObtengaUnaVentaPorId(id);
            if (!LaVentaEstaTerminada(venta))
            {
                venta.TipoDePago = tipoDePago;
                _dbContext.Update(venta);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }


        private bool LaVentaEstaTerminada(Venta venta)
        {
            return venta.Estado == EstadoVenta.Terminada;
        }

       
    }
}
