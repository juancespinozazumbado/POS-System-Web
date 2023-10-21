
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Areas.Productos.Servicio.Iservicio;
using Inventario.WebApp.Areas.Ventas.Modelos;
using Inventario.WebApp.Areas.Ventas.Modelos.Dtos;
using Inventario.WebApp.Areas.Ventas.Servicio.IServicio;
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
    

        private readonly IServicioDeVentas _servicioDeVentas;
        private readonly IservicioDeAperturaDeCaja _servicioDeCajas;
        private readonly IServicioDeInventario _servicioDeInventarios;
        
        public VentasController(
            IServicioDeInventario servicioDeInevnatrio, 
            IservicioDeAperturaDeCaja servicioDeDecajas, 
            IServicioDeVentas serviciodeVentas )
        {
            
            _servicioDeCajas = servicioDeDecajas;
            _servicioDeInventarios = servicioDeInevnatrio;
            _servicioDeVentas = serviciodeVentas;
            
        }
        // GET: VentasController
        public async Task<ActionResult> Index()
        {

            string Id_Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string username = User.Identity.Name;
            
            AperturaDeCajaViewModel modelo = new() { Usuario = username };

            var cajas = await _servicioDeCajas.AperturasDeCajaPorUsuario(Id_Usuario);
              AperturaDeCaja caja = cajas.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();
            if (caja != null)
                modelo.TieneUnaCajaAbierta = true;


            return View(modelo);
        }

        // GET: VentasController/Create
        public async Task<ActionResult> VentaEnProceso()
        {

            string IdUsuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
           
            var cajas = await _servicioDeCajas.AperturasDeCajaPorUsuario(IdUsuario);

              AperturaDeCaja caja = cajas.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

            var ventasDelusario = caja.Ventas;
            Venta ventaAbierta = ventasDelusario.Where(v => v.Estado == EstadoVenta.EnProceso).FirstOrDefault();

            List<Inventarios> inventarios = await _servicioDeInventarios.ListarInventarios();


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
        public async Task<ActionResult> AgregarItem(VentaEnProcesoViewModel modelo)
        {
            var Id_Usuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            
            var cajas = await _servicioDeCajas.AperturasDeCajaPorUsuario(Id_Usuario);

            AperturaDeCaja caja = cajas.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();


            AgregarItemDeVenatRequest itemDeVenta = new()
            { 
                id_Usuario = Id_Usuario ,
                Id_venta = modelo.Detalles.Id_venta,
                cantidad = modelo.Detalles.Cantidad,
                Id_Inventario = modelo.Detalles.Id_inventario,
                Id_caja = caja.Id
            };
            var inventarios = await _servicioDeInventarios.ListarInventarios();

            var itemDelInventario = inventarios.Find(i => i.Id == modelo.Detalles.Id_inventario);

            // Verificar si hay suficiente cantidad disponible en el inventario
            if (itemDelInventario.Cantidad >= modelo.Detalles.Cantidad && modelo.Detalles.Cantidad > 0)
            {
                itemDeVenta.cantidad = modelo.Detalles.Cantidad;
                var venta = await _servicioDeVentas.AñadaUnDetalleAlaVenta(modelo.Detalles.Id_venta, itemDeVenta);
                 inventarios = await _servicioDeInventarios.ListarInventarios();
                // reconstruye el modelo de la vista principal
                VentaEnProcesoViewModel VentaEnProcesoViewModel = new()
                {
                    Inventarios = inventarios,
                    Detalles = new(),
                    venta = venta
                };

                return RedirectToAction(nameof(VentaEnProceso), VentaEnProcesoViewModel);
            } else { 
                ModelState.AddModelError("", "La cantidad solicitada excede la cantidad disponible en el inventario.");
            }

            // En caso de error, volver a la vista Index
            return RedirectToAction(nameof(VentaEnProceso));
        }

        // POST: VentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult >EliminarItem(VentaEnProcesoViewModel modelo)
        {
            try
            {
                var Id_Usuario =  User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var inventarios = await _servicioDeInventarios.ListarInventarios();
                var cajas = await _servicioDeCajas.AperturasDeCajaPorUsuario(Id_Usuario);

                AperturaDeCaja caja = cajas.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

                var inventario = inventarios.Find(i => i.Id == modelo.Detalles.Id_inventario);

                var venta = await _servicioDeVentas.ElimineUnDetalleDeLaVenta(modelo.Detalles.Id_venta,
                    new QuitarItemDeVentaRequest()
                    {
                        id_Item = modelo.Detalles.Id,
                        id_Usuario = Id_Usuario,
                        Id_venta = modelo.Detalles.Id_venta,
                        Id_caja = caja.Id
                    });

                //Venta venta = await RepositorioDeVentas.ObtengaUnaVentaPorId(modelo.Detalles.Id_venta);
                //VentaDetalle item = venta.VentaDetalles.Find(d => d.Id == modelo.Detalles.Id);
                inventarios = await _servicioDeInventarios.ListarInventarios();

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
        public async Task<ActionResult> CrearVenta(Venta venta)
        {
            try
            {

                string IdUsuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
               
                var cajas = await _servicioDeCajas.AperturasDeCajaPorUsuario(IdUsuario);
                AperturaDeCaja caja = cajas.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

                CrearVentaRequest LaVenta = new()
                {
                    Id_caja  = caja.Id,
                    Id_Usuario = IdUsuario,
                    Cliente = venta.NombreCliente,
                    
                };
                var ventaActual = await _servicioDeVentas.CreeUnaVenta(LaVenta);

                 //var ventaActual  = caja.Ventas.Where(v => v.Estado != EstadoVenta.Terminada).FirstOrDefault();

                return RedirectToAction(nameof(VentaEnProceso), ventaActual);
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
        public async Task<ActionResult>TerminarVenta(VentaEnProcesoViewModel modelo)
        {
            try
            {

                var venta = await _servicioDeVentas.TermineLaVenta(modelo.venta.Id, 
                    new TerminarVentaRequest() { Id_venta = modelo.venta.Id, TipoDePago = (Modelos.TipoDePago)modelo.venta.TipoDePago});

                
               // venta.TipoDePago = modelo.venta.TipoDePago;
               //await  RepositorioDeVentas.EstablescaElTipoDePago(venta.Id, modelo.venta.TipoDePago);
               //await  RepositorioDeVentas.TermineLaVenta(venta.Id);

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
        public async Task<ActionResult> AplicarUnDescuento(VentaEnProcesoViewModel modelo)
        {
            try
            {
                var venta = await _servicioDeVentas.ApliqueUnDescuento(modelo.venta.Id,
                    new AplicarDescuentoRequest()
                    {
                        Id_venta = modelo.venta.Id,
                        descuento = modelo.venta.PorcentajeDesCuento,

                    });

                
                //modelo.venta = venta;

                return RedirectToAction(nameof(VentaEnProceso));
            }
            catch
            {
                return View();
            }
        }


  
    }
}
