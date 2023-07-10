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
        public async Task<ActionResult> Index()
        {
            string id =  User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual =  RepositorioDeUsuarios.ObtengaUnUsuarioPorId(id);
            var cajas = await RepositorioDeAperturaDeCAja.AperturasDeCajaPorUsuario(id);
            var cajasCerradas = cajas.Where(c => c.estado == EstadoCaja.Cerrada).ToList();
            AperturaDeCaja  cajaActual = cajas
                .Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();
            List<AperturaDeCaja> cajasDelUsuario = await RepositorioDeAperturaDeCAja.AperturasDeCajaPorUsuario(id);
            cajasDelUsuario = cajasDelUsuario.Where(c => c.estado == EstadoCaja.Cerrada).ToList();
    
          
                AperturaDeCajaViewModel modelo = new()
                {
                    Usuario = usaurioActual,
                    TieneUnaCajaAbierta = cajaActual != null,
                    Cajas = cajasCerradas,
                    Caja = cajaActual,
                    Totales = cajaActual != null ?  await RepositorioDeAperturaDeCAja.OtenerTotalesPorCaja(cajaActual.Id) : null

                };
            
           
            
           


            return View(modelo);

        }

        // GET: VentasController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            AperturaDeCaja caja = await RepositorioDeAperturaDeCAja.ObtenerUnaAperturaDeCajaPorId(id);

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
        public async Task<ActionResult> Create(AperturaDeCaja caja)
        {
            try
            {
                await RepositorioDeAperturaDeCAja.CrearUnaAperturaDeCaja(caja);

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
            await RepositorioDeAperturaDeCAja.CerrarUnaAperturaDeCaja(id);

            string usuarioId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(usuarioId);

            var cajasDelUsuario = await RepositorioDeAperturaDeCAja.AperturasDeCajaPorUsuario(usuarioId);
                AperturaDeCaja cajaActual = cajasDelUsuario.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

            List<AperturaDeCaja> cajas = await RepositorioDeAperturaDeCAja.AperturasDeCajaPorUsuario(usuarioId);
                cajas = cajas.Where(c => c.estado == EstadoCaja.Cerrada).ToList();
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
        public async Task<ActionResult> DetallesCajaCerrada(int id)
        {

            string usuarioId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(usuarioId);

            AperturaDeCaja caja = await RepositorioDeAperturaDeCAja.ObtenerUnaAperturaDeCajaPorId(id);

            var Totales = await RepositorioDeAperturaDeCAja.OtenerTotalesPorCaja(id);

            AperturaDeCajaViewModel modelo = new()
            {
                Usuario = usaurioActual,
                Caja = caja,
                Totales = Totales,

            };


            return View(modelo);
        }


        public async Task<ActionResult> VentasPorCaja(int id)
        {

            string usuarioId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(usuarioId);

            AperturaDeCaja caja = await RepositorioDeAperturaDeCAja.ObtenerUnaAperturaDeCajaPorId(id);



            AperturaDeCajaViewModel modelo = new()
            {
                Usuario = usaurioActual,
                Caja = caja,
                
            };


            return View(modelo);
        }



    }
}
