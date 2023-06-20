using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Areas.Ventas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inventario.WebApp.Controllers
{


    [Area("Ventas")]
    [Authorize]
    public class AperturasDeCajaController : Controller
    {
        RepositorioDeVentas RepositorioDeVentas;
        ReporitorioDeInventarios ReporitorioDeInventarios;
        RepositorioDeUsuarios RepositorioDeUsuarios;
        RepositorioDeAperturaDeCaja RepositorioDeAperturaDeCAja;
        public AperturasDeCajaController (InventarioDBContext context)
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
           AplicationUser usaurioActual  = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(id);
            AperturaDeCaja? cajaActual = RepositorioDeAperturaDeCAja.AperturasDeCajaPorUsuario(id)
                .Where(c=> c.estado== EstadoCaja.Abierta).FirstOrDefault();  
            UsuarioParaCerar modelo = new() { 
                Usuario = usaurioActual,
                TieneUnaCajaAbierta = cajaActual!= null
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

        // GET: VentasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VentasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VentasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
