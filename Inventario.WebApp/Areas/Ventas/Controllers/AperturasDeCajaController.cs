using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Areas.Autenticacion.Servicio;
using Inventario.WebApp.Areas.Ventas.Modelos;
using Inventario.WebApp.Areas.Ventas.Modelos.Dtos;
using Inventario.WebApp.Areas.Ventas.Servicio.IServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using TipoDePago = Inventario.Models.Dominio.Ventas.TipoDePago;

namespace Inventario.WebApp.Areas.Ventas.Controllers
{
    [Area("Ventas")]
    [Authorize]
    public class AperturasDeCajaController : Controller
    {
        

        private readonly IservicioDeAperturaDeCaja _servicioDeAperturaDeCaja;
        
       
        public AperturasDeCajaController(IservicioDeAperturaDeCaja servicioDeCaja)
        { 
            _servicioDeAperturaDeCaja = servicioDeCaja; 
        }
        // GET: VentasController
        public async Task<ActionResult> Index()
        {

            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string username = User.Identity.Name;
            string user = User.FindFirst(ClaimTypes.Name).Value;
            string email = User.FindFirst(ClaimTypes.Email).Value;

           
            var cajas = await _servicioDeAperturaDeCaja.AperturasDeCajaPorUsuario(id);
            var cajasCerradas = cajas.Where(c => c.estado == EstadoCaja.Cerrada).ToList();
            AperturaDeCaja  cajaActual = cajas
                .Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

          
                AperturaDeCajaViewModel modelo = new()
                {
                    Usuario = username,
                    TieneUnaCajaAbierta = cajaActual != null,
                    Cajas = cajasCerradas,
                    Caja = cajaActual,
                    Totales = cajaActual != null ?  ObtenerTotalesPorCaja(cajaActual) : null

                };

            return View(modelo);

        }

        // GET: VentasController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string id_usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var caja = await _servicioDeAperturaDeCaja.ObtenerUnaAperturaDeCajaPorId(id, id_usuario);

            return View(caja);
        }

        // GET: VentasController/Create
        public ActionResult AbrirCaja()
        {
            string id_usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
           

            AperturaDeCaja aperturaCaja = new()
            {
                UserId = id_usuario,
                FechaDeInicio = DateTime.Now,
                FechaDeCierre = null,
                Observaciones = null,
                estado = EstadoCaja.Abierta,
                Ventas = new()
            };

            return View(aperturaCaja);
        }

        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AperturaDeCaja caja)
        {
            try
            {
                
                   await _servicioDeAperturaDeCaja.CrearUnaAperturaDeCaja(new AperturadeCajaDto()
                   {
                   Id_Usuario = caja.UserId,
                   Observaciones= caja.Observaciones
                   } );

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VentasController/CerrarLaCaja/5
        public async Task<ActionResult> CerrarLaCaja(int id)
        {
            string id_usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string username = User.Identity.Name;
            var r = await _servicioDeAperturaDeCaja.CerrarUnaAperturaDeCaja(id, id_usuario);


            var cajasDelUsuario = await _servicioDeAperturaDeCaja.AperturasDeCajaPorUsuario(id_usuario);
                AperturaDeCaja cajaActual = cajasDelUsuario.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

            AperturaDeCajaViewModel modelo = new()
            {
                Usuario = username,
                TieneUnaCajaAbierta = cajaActual != null,
                Cajas = cajasDelUsuario,
                Caja = cajaActual

            };
            return View(nameof(Index), modelo);
        }

       

        // GET: VentasController/Delete/5
        public async Task<ActionResult> DetallesCajaCerrada(int id)
        {

            string usuarioId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string username = User.Identity.Name;

            AperturaDeCaja caja = await _servicioDeAperturaDeCaja.ObtenerUnaAperturaDeCajaPorId(id, usuarioId);

            var Totales = ObtenerTotalesPorCaja(caja);

            AperturaDeCajaViewModel modelo = new()
            {
                Usuario = username,
                Caja = caja,
                Totales = Totales,

            };


            return View(modelo);
        }


        public async Task<ActionResult> VentasPorCaja(int id)
        {

            string usuarioId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string username = User.Identity.Name;

            AperturaDeCaja caja = await _servicioDeAperturaDeCaja.ObtenerUnaAperturaDeCajaPorId(id, usuarioId);

            AperturaDeCajaViewModel modelo = new()
            {
                Usuario = username,
                Caja = caja,
                
            };


            return View(modelo);
        }


        private  Dictionary<string,List<Venta>> ObtenerTotalesPorCaja(AperturaDeCaja caja)
        {
            

            return new Dictionary<string, List<Venta>>()
            {
                {Enum.GetName(typeof(TipoDePago), 1), caja.Ventas.Where(v=> v.Estado == EstadoVenta.Terminada && v.TipoDePago == TipoDePago.Efectivo).ToList()},
                {Enum.GetName(typeof(TipoDePago), 2), caja.Ventas.Where(v=> v.Estado == EstadoVenta.Terminada && v.TipoDePago == TipoDePago.Tarjeta).ToList()},
                {Enum.GetName(typeof(TipoDePago), 3), caja.Ventas.Where(v=> v.Estado == EstadoVenta.Terminada && v.TipoDePago == TipoDePago.SinpeMovil).ToList()},
            };
        }



    }
}
