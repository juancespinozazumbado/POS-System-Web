using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Models.Ventas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace Inventario.WebApp.Controllers
{


    [Authorize]
    public class VentasController : Controller
    {
        RepositorioDeVentas RepositorioDeVentas;
        ReporitorioDeInventarios ReporitorioDeInventarios;
        IEnumerable<ProductosAVender> ListaDeProductosAVenders;
        public VentasController(InventarioDBContext context, IMemoryCache elCache)
        {
            RepositorioDeVentas = new(context, elCache);
            ReporitorioDeInventarios = new(context);
            ListaDeProductosAVenders = new List<ProductosAVender>();
        }
        // GET: VentasController
        public ActionResult Index()
        {

            var inventario = ReporitorioDeInventarios.listeElInventarios();
            var productosAVender = RepositorioDeVentas.ObtengaLaListaDeItems();

            var viewModel = new Models.Ventas.MostrarVentasInventarios
            {
                Inventario = inventario,
                ProductosAVender = productosAVender
            };


            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AgregarItem(int id, int cantidad)
        {
            MostrarVentasInventarios estaVenta;

            // Obtener el item seleccionado del inventario
            var itemDelInventario = ReporitorioDeInventarios.ObetenerInevtarioPorId(id);

            // Verificar si hay suficiente cantidad disponible en el inventario
            if (itemDelInventario.Cantidad >= cantidad)
            {
                // Agregar el item al carrito de compras
                ProductosAVender productos = new ProductosAVender
                {
                    NombreDelProducto = itemDelInventario.Nombre,
                    Cantidad = cantidad,
                    Precio = itemDelInventario.Precio,
                    Total = itemDelInventario.Precio * cantidad
                };
                RepositorioDeVentas.AgregueUnItemAlCarrito(productos);

                // Actualizar la cantidad en el inventario
                itemDelInventario.Cantidad -= cantidad;
                ReporitorioDeInventarios.EditarInventario(itemDelInventario);

                // Después de actualizar la cantidad en el inventario
                var ListaDeProductosAVender = RepositorioDeVentas.ObtengaLaListaDeItems();
                return PartialView("ProductosParcial", ListaDeProductosAVender);

            }
            else
            {
                ModelState.AddModelError("", "La cantidad solicitada excede la cantidad disponible en el inventario.");
            }

            // En caso de error, volver a la vista Index
            return RedirectToAction("Index");
        }

        // Post: VentasController/NuevaVenta
        public ActionResult NuevaVenta()
        {
            string IdDelUsuario = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            var venta = new Venta
            {
                NombreCliente = "NombreCliente2",
                Fecha = DateTime.Now,
                TipoDePago = 0,
                Total = 0,
                SubTotal = 0,
                PorcentajeDesCuento = 0,
                MontoDescuento = 0,
                UserId = IdDelUsuario,
                Estado = EstadoVenta.EnProceso,
                IdAperturaDeCaja = 11


            };
            RepositorioDeVentas.CreeUnaVenta(venta);
            return RedirectToAction("Index");
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
