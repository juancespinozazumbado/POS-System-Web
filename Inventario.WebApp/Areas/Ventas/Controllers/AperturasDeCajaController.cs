using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Areas.Ventas.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inventario.WebApp.Areas.Ventas.Controllers
{
    [Area("Ventas")]
    [Authorize]
    public class AperturasDeCajaController : Controller
    {
        RepositorioDeVentas RepositorioDeVentas;
        ReporitorioDeInventarios ReporitorioDeInventarios;
        RepositorioDeUsuarios RepositorioDeUsuarios;
        RepositorioDeAperturaDeCaja RepositorioDeAperturaDeCAja;
        public AperturasDeCajaController(InventarioDBContext context)
        {
            RepositorioDeVentas = new(context);
            ReporitorioDeInventarios = new(context);
            RepositorioDeUsuarios = new(context);
            RepositorioDeAperturaDeCAja = new(context);
        }
        // GET: VentasController
        public ActionResult Index()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(id);
            AperturaDeCaja? cajaActual = RepositorioDeAperturaDeCAja.AperturasDeCajaPorUsuario(id)
                .Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();
            List<AperturaDeCaja> cajas = RepositorioDeAperturaDeCAja.AperturasDeCajaPorUsuario(id)
                .Where(c => c.estado == EstadoCaja.Cerrada).ToList();
    
          
                AperturaDeCajaViewModel modelo = new()
                {
                    Usuario = usaurioActual,
                    TieneUnaCajaAbierta = cajaActual != null,
                    Cajas = cajas,
                    Caja = cajaActual,
                    Totales = cajaActual != null ? RepositorioDeAperturaDeCAja.OtenerTotalesPorCaja(cajaActual.Id) : null

                };
            
           
            
           


            return View(modelo);

        }

        // GET: VentasController/Details/5
        public ActionResult Details(int id)
        {
            AperturaDeCaja caja = RepositorioDeAperturaDeCAja.ObtenerUnaAperturaDeCajaPorId(id);

            return View(caja);
        }

        // GET: VentasController/Create
        public ActionResult AbrirCaja()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AperturaDeCaja aperturaCaja = new()
            {
                UserId = id,
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
        public ActionResult Create(AperturaDeCaja caja)
        {
            try
            {
                RepositorioDeAperturaDeCAja.CrearUnaAperturaDeCaja(caja);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VentasController/CerrarLaCaja/5
        public ActionResult CerrarLaCaja(int id)
        {
            RepositorioDeAperturaDeCAja.CerrarUnaAperturaDeCaja(id);

            string usuarioId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(usuarioId);
            AperturaDeCaja? cajaActual = RepositorioDeAperturaDeCAja.AperturasDeCajaPorUsuario(usuarioId)
                .Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();
            List<AperturaDeCaja> cajas = RepositorioDeAperturaDeCAja.AperturasDeCajaPorUsuario(usuarioId)
                .Where(c => c.estado == EstadoCaja.Cerrada).ToList();
            AperturaDeCajaViewModel modelo = new()
            {
                Usuario = usaurioActual,
                TieneUnaCajaAbierta = cajaActual != null,
                Cajas = cajas,
                Caja = cajaActual

            };
            return View(nameof(Index), modelo);
        }

       

        // GET: VentasController/Delete/5
        public ActionResult DetallesCajaCerrada(int id)
        {

            string usuarioId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(usuarioId);

            AperturaDeCaja caja = RepositorioDeAperturaDeCAja.ObtenerUnaAperturaDeCajaPorId(id);

            var Totales = RepositorioDeAperturaDeCAja.OtenerTotalesPorCaja(id);

            AperturaDeCajaViewModel modelo = new()
            {
                Usuario = usaurioActual,
                Caja = caja,
                Totales = Totales,

            };


            return View(modelo);
        }


        public ActionResult VentasPorCaja(int id)
        {

            string usuarioId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(usuarioId);

            AperturaDeCaja caja = RepositorioDeAperturaDeCAja.ObtenerUnaAperturaDeCajaPorId(id);



            AperturaDeCajaViewModel modelo = new()
            {
                Usuario = usaurioActual,
                Caja = caja,
                
            };


            return View(modelo);
        }



    }
}
