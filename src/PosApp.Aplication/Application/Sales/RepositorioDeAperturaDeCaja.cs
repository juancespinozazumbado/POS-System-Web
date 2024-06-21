using Inventario.BL.Funcionalidades.Ventas.Interfaces;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Ventas;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Inventario.BL.Funcionalidades.Ventas
{
    public class RepositorioDeAperturaDeCaja : IrepositorioDeAperturaDeCaja
    {
        private readonly InventarioDBContext context;

        public RepositorioDeAperturaDeCaja(InventarioDBContext context)
        {
            this.context = context;
        }
        public async Task<List<AperturaDeCaja>> AperturasDeCajaPorUsuario(string idUsuario)
        {
            List<AperturaDeCaja> AperturasDeCajaRegistradas = await ListarAperturasDeCaja();

            //var CajasPorUsuario = from caja in AperturasDeCajaRegistradas
            //                      where caja.UserId.Equals(idUsuario)
           
            List<AperturaDeCaja> Cajas = AperturasDeCajaRegistradas.Where( c => c.UserId == idUsuario ).ToList();  
            return Cajas;
        }

        public async Task<bool> CerrarUnaAperturaDeCaja(int id)
        {
            AperturaDeCaja caja = await ObtenerUnaAperturaDeCajaPorId( id);

            if (caja != null && caja.estado == EstadoCaja.Abierta)
            {
                var ventas = from venta in caja.Ventas
                             where venta.Estado == EstadoVenta.EnProceso
                             select venta;

                if (ventas.Count() == 0 || ventas == null)
                {
                    caja.estado = EstadoCaja.Cerrada;
                    caja.FechaDeCierre = DateTime.Now;
                    context.AperturasDeCaja.Update(caja);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async  Task<bool> CrearUnaAperturaDeCaja(AperturaDeCaja aperturaDeCaja)
        {
            var Cajas = await ListarAperturasDeCaja();

            var CajasAbiertas = from caja in Cajas
                                where caja.estado == EstadoCaja.Abierta
                                 && caja.UserId == aperturaDeCaja.UserId
                                select caja;

            if (CajasAbiertas.Count() == 0)
            {
                context.AperturasDeCaja.Add(aperturaDeCaja);
               await  context.SaveChangesAsync();
                return true;
            }return false;
        }

        public async Task<List<AperturaDeCaja>> ListarAperturasDeCaja()
        {
            return await context.AperturasDeCaja.Include(a => a.Ventas).ThenInclude(v=> v.VentaDetalles).ToListAsync();

        }


        public async Task<bool> LaCajaEstaCerrada(int id)
        {
            var caja = await ObtenerUnaAperturaDeCajaPorId(id);
            return caja.estado == EstadoCaja.Cerrada;
        }

        public async Task<AperturaDeCaja> ObtenerUnaAperturaDeCajaPorId(int id)
        {
            var aperturasExistentes = await ListarAperturasDeCaja();
            return aperturasExistentes.Where(a => a.Id == id).FirstOrDefault();
        }


        public async Task<Dictionary<string, List<Venta>>> OtenerTotalesPorCaja(int id)
        {
            AperturaDeCaja Caja = await ObtenerUnaAperturaDeCajaPorId(id);
            Dictionary<string, List<Venta>> DiccionarioDeventas = new Dictionary<string, List<Venta>>();

            
                DiccionarioDeventas = new Dictionary<string, List<Venta>>()
                {
                    {Enum.GetName(typeof(TipoDePago), 1), ObtenerElTotalDeVentasPorEfectivo(Caja)},
                    {Enum.GetName(typeof(TipoDePago), 2), ObtenerElTotalDeVentasPorTarjeta(Caja)},
                    {Enum.GetName(typeof(TipoDePago), 3), ObtenerElTotalDeVentasPorSinpeMovil(Caja)},
                 };

            return DiccionarioDeventas;
        }

        private  List<Venta>? ObtenerElTotalDeVentasPorEfectivo(AperturaDeCaja caja)
        {
            return  (List<Venta>?)caja.Ventas.Where(a => a.TipoDePago == TipoDePago.Efectivo).ToList();
        }

        private List<Venta>? ObtenerElTotalDeVentasPorTarjeta(AperturaDeCaja caja)
        {
            return (List<Venta>?)caja.Ventas.Where(a => a.TipoDePago == TipoDePago.Tarjeta).ToList();
        }

        private List<Venta>? ObtenerElTotalDeVentasPorSinpeMovil(AperturaDeCaja caja)
        {
            return (List<Venta>?)caja.Ventas.Where(a => a.TipoDePago == TipoDePago.SinpeMovil).ToList() ;
        }
    }
}
