using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Areas.Ventas.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace Inventario.WebApp.Areas.Ventas.Controllers
{
    [Area("Ventas")]
    [Authorize]
    public class VentasController : Controller
    {
        RepositorioDeVentas RepositorioDeVentas;
        ReporitorioDeInventarios ReporitorioDeInventarios;
        RepositorioDeUsuarios RepositorioDeUsuarios;
        RepositorioDeAperturaDeCaja RepositorioDeAperturaDeCaja;
        
        public VentasController(InventarioDBContext context)
        {
            RepositorioDeVentas = new(context);
            ReporitorioDeInventarios = new(context);
            RepositorioDeUsuarios = new(context);
            RepositorioDeAperturaDeCaja = new(context);
            
        }
        // GET: VentasController
        public ActionResult Index()
        {

            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(id);
            AperturaDeCajaViewModel modelo = new() { Usuario = usaurioActual };

            AperturaDeCaja? caja = RepositorioDeAperturaDeCaja.AperturasDeCajaPorUsuario(id)
               .Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();
            if (caja != null)
                modelo.TieneUnaCajaAbierta = true;


            return View(modelo);
        }

        // GET: VentasController/Create
        public ActionResult VentaEnProceso()
        {

            string IdUsuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(IdUsuario);
            AperturaDeCaja? caja = RepositorioDeAperturaDeCaja.AperturasDeCajaPorUsuario(IdUsuario)
              .Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

            Venta? ventaAbierta = RepositorioDeVentas.ListeLasVentasPorUsuario(IdUsuario)
               .Where(v => v.Estado == EstadoVenta.EnProceso).FirstOrDefault();

            List<Inventarios> inventarios = (List<Inventarios>)ReporitorioDeInventarios.listeElInventarios();


            VentaEnProcesoViewModel modelo = new() { Inventarios = inventarios, Detalles = new() };

            if (ventaAbierta == null)
            {
                Venta venta = new()
                {
                    AperturaDeCaja = caja,
                    IdAperturaDeCaja = caja.Id,
                    UserId = IdUsuario

                };


                modelo.venta = venta;

            }
            else
                modelo.venta = ventaAbierta;

            return View(modelo);

        }

        [HttpPost]
        public ActionResult AgregarItem(VentaEnProcesoViewModel modelo)
        {

            // Obtener el item seleccionado del inventario
            var itemDelInventario = ReporitorioDeInventarios.ObetenerInevtarioPorId(modelo.Detalles.Id_inventario);

            // Verificar si hay suficiente cantidad disponible en el inventario
            if (itemDelInventario.Cantidad >= modelo.Detalles.Cantidad && modelo.Detalles.Cantidad > 0)
            {

                VentaDetalle ventaDetalle = modelo.Detalles;

                ventaDetalle.Monto = ventaDetalle.Cantidad * ventaDetalle.Precio;

                // Actualizar la cantidad en el inventario
                itemDelInventario.Cantidad -= ventaDetalle.Cantidad;
                ReporitorioDeInventarios.EditarInventario(itemDelInventario);
                RepositorioDeVentas.AñadaUnDetalleAlaVenta(ventaDetalle.Id_venta, ventaDetalle);

                List<Inventarios> inventarios = (List<Inventarios>)ReporitorioDeInventarios.listeElInventarios();
                Venta venta = RepositorioDeVentas.ObtengaUnaVentaPorId(ventaDetalle.Id_venta);
                VentaEnProcesoViewModel VentaEnProcesoViewModel = new()
                {
                    Inventarios = inventarios,
                    Detalles = new(),
                    venta = venta
                };

                return RedirectToAction(nameof(VentaEnProceso), VentaEnProcesoViewModel);
            }
            else
            {
                ModelState.AddModelError("", "La cantidad solicitada excede la cantidad disponible en el inventario.");
            }

            // En caso de error, volver a la vista Index
            return RedirectToAction(nameof(VentaEnProceso));
        }

        // POST: VentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarItem(VentaEnProcesoViewModel modelo)
        {
            try
            {
                Inventarios inventario = ReporitorioDeInventarios.ObetenerInevtarioPorId(modelo.Detalles.Id_inventario);
                inventario.Cantidad += modelo.Detalles.Cantidad;
                ReporitorioDeInventarios.EditarInventario(inventario);

                Venta venta = RepositorioDeVentas.ObtengaUnaVentaPorId(modelo.Detalles.Id_venta);
                VentaDetalle item = venta.VentaDetalles.Find(d => d.Id == modelo.Detalles.Id);

                RepositorioDeVentas.ElimineUnDetalleDeLaVenta(venta.Id, item);
                List<Inventarios> inventarios = (List<Inventarios>)ReporitorioDeInventarios.listeElInventarios();

                VentaEnProcesoViewModel VentaEnProcesoViewModel = new()
                {
                    Inventarios = inventarios,
                    Detalles = new(),
                    venta = venta
                };

                return RedirectToAction(nameof(VentaEnProceso), VentaEnProcesoViewModel);
            }
            catch (Exception e)

            {
                e = null;
                return View();
            }
        }


        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearVenta(Venta venta)
        {
            try
            {

                string IdUsuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(IdUsuario);
                AperturaDeCaja? caja = RepositorioDeAperturaDeCaja.AperturasDeCajaPorUsuario(IdUsuario)
                  .Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

                Venta LaVenta = new()
                {
                    AperturaDeCaja = caja,
                    IdAperturaDeCaja = caja.Id,
                    UserId = IdUsuario,
                    NombreCliente = venta.NombreCliente,
                    Fecha = DateTime.Now,
                    Estado = EstadoVenta.EnProceso,
                    VentaDetalles = new()
                };
                RepositorioDeVentas.CreeUnaVenta(LaVenta);
                LaVenta = RepositorioDeVentas.ListeLasVentasPorUsuario(IdUsuario).Where(v => v.Estado != EstadoVenta.Terminada).FirstOrDefault();

                return RedirectToAction(nameof(VentaEnProceso), LaVenta);
            }
            catch (Exception e)
            {
                e = null;
                return View();
            }
        }
        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TerminarVenta(VentaEnProcesoViewModel modelo)
        {
            try
            {
                Venta venta = RepositorioDeVentas.ObtengaUnaVentaPorId(modelo.venta.Id);
                venta.TipoDePago = modelo.venta.TipoDePago;
                RepositorioDeVentas.EstablescaElTipoDePago(venta.Id, modelo.venta.TipoDePago);
                RepositorioDeVentas.TermineLaVenta(venta.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AplicarUnDescuento(VentaEnProcesoViewModel modelo)
        {
            try
            {
                Venta venta = RepositorioDeVentas.ObtengaUnaVentaPorId(modelo.venta.Id);
                RepositorioDeVentas.ApliqueUnDescuento(venta.Id, modelo.venta.PorcentajeDesCuento);

                modelo.venta = venta;

                return RedirectToAction(nameof(VentaEnProceso));
            }
            catch
            {
                return View();
            }
        }


        // GET: VentasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

  
    }
}
