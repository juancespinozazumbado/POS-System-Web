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
        public IEnumerable<AperturaDeCaja> AperturasDeCajaPorUsuario(string idUsuario)
        {
            List<AperturaDeCaja> AperturasDeCajaRegistradas = (List<AperturaDeCaja>)ListarAperturasDeCaja();

            var CajasPorUsuario = from caja in AperturasDeCajaRegistradas
                                  where caja.UserId.Equals(idUsuario)
                                  select caja;

            return CajasPorUsuario;
        }

        public void CerrarUnaAperturaDeCaja(int id)
        {
            AperturaDeCaja caja = ObtenerUnaAperturaDeCajaPorId( id);

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
                    context.SaveChanges();
                }
            }
        }

        public void CrearUnaAperturaDeCaja(AperturaDeCaja aperturaDeCaja)
        {
            var Cajas = ListarAperturasDeCaja();

            var CajasAbiertas = from caja in Cajas
                                where caja.estado == EstadoCaja.Abierta
                                 && caja.UserId == aperturaDeCaja.UserId
                                select caja;

            if (CajasAbiertas.Count() == 0)
            {
                context.AperturasDeCaja.Add(aperturaDeCaja);
                context.SaveChanges();

            }
        }

        public IEnumerable<AperturaDeCaja> ListarAperturasDeCaja()
        {
            return context.AperturasDeCaja.Include(a => a.Ventas).ToList();

        }


        public bool LaCajaEstaCerrada(int id)
        {
            return ObtenerUnaAperturaDeCajaPorId(id).estado == EstadoCaja.Cerrada;
        }

        public AperturaDeCaja ObtenerUnaAperturaDeCajaPorId(int id)
        {
            return ListarAperturasDeCaja().Where(a => a.Id == id).FirstOrDefault();
        }


        public Dictionary<string, List<Venta>> OtenerTotalesPorCaja(int id)
        {
            AperturaDeCaja Caja = ObtenerUnaAperturaDeCajaPorId(id);
            Dictionary<string, List<Venta>> DiccionarioDeventas = new Dictionary<string, List<Venta>>();

            
                DiccionarioDeventas = new Dictionary<string, List<Venta>>()
                {
                    {Enum.GetName(typeof(TipoDePago), 1), ObtenerElTotalDeVentasPorEfectivo(Caja)},
                    {Enum.GetName(typeof(TipoDePago), 2), ObtenerElTotalDeVentasPorTarjeta(Caja)},
                    {Enum.GetName(typeof(TipoDePago), 3), ObtenerElTotalDeVentasPorSinpeMovil(Caja)},
                 };
               
            

            return DiccionarioDeventas;
        }

        private List<Venta>? ObtenerElTotalDeVentasPorEfectivo(AperturaDeCaja caja)
        {
            return (List<Venta>?)caja.Ventas.Where(a => a.TipoDePago == TipoDePago.Efectivo).ToList();
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
