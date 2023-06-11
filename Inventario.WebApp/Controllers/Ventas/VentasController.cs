using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Ventas;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inventario.WebApp.Controllers.Ventas
{
    public class VentasController : Controller
    {
        RepositorioDeVentas RepositorioDeVentas;
        ReporitorioDeInventarios ReporitorioDeInventarios;
        public VentasController(InventarioDBContext context)
        {
            RepositorioDeVentas = new(context);
            ReporitorioDeInventarios = new(context);
        }
        // GET: VentasController
        public ActionResult Index()
        {
            string IdDelUsuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            /* int IdDelUsuario = !string.IsNullOrEmpty(ValorDelIdDelUsuario) ? int.Parse(ValorDelIdDelUsuario) : 0;*/

            var venta = new Venta
            {
                NombreCliente = "NombreCliente1",
                Fecha = DateTime.Now,
                TipoDePago = 1,
                Total = 100,
                SubTotal = 90,
                PorcentajeDesCuento = 10,
                MontoDescuento = 10,
                UserId = IdDelUsuario,
                Estado = EstadoVenta.EnProceso,
                IdAperturaDeCaja = 10,


            };
            RepositorioDeVentas.RegistreElInicioDeLaVenta(venta);
            var inventario = ReporitorioDeInventarios.listeElInventarios();
            var ventas = RepositorioDeVentas.ListeLasVentas();

            var viewModel = new MostrarVentasInventarios
            {
                Inventario = inventario,
                Ventas = ventas
            };

            return View(viewModel);
        }

        // GET: VentasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VentasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Venta venta)
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
