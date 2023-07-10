﻿using Inventario.BL.Funcionalidades.Inventario;
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
        public async Task<ActionResult> Index()
        {

            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(id);
            AperturaDeCajaViewModel modelo = new() { Usuario = usaurioActual };

            var cajas = await RepositorioDeAperturaDeCaja.AperturasDeCajaPorUsuario(id);
              AperturaDeCaja caja = cajas.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();
            if (caja != null)
                modelo.TieneUnaCajaAbierta = true;


            return View(modelo);
        }

        // GET: VentasController/Create
        public async Task<ActionResult> VentaEnProceso()
        {

            string IdUsuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(IdUsuario);
            var cajas = await RepositorioDeAperturaDeCaja.AperturasDeCajaPorUsuario(IdUsuario);

              AperturaDeCaja caja = cajas.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

            var ventasDelusario = await  RepositorioDeVentas.ListeLasVentasPorUsuario(IdUsuario);
               Venta ventaAbierta = ventasDelusario.Where(v => v.Estado == EstadoVenta.EnProceso).FirstOrDefault();

            List<Inventarios> inventarios = await ReporitorioDeInventarios.listeElInventarios();


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

            // Obtener el item seleccionado del inventario
            var itemDelInventario = await ReporitorioDeInventarios.ObetenerInevtarioPorId(modelo.Detalles.Id_inventario);

            // Verificar si hay suficiente cantidad disponible en el inventario
            if (itemDelInventario.Cantidad >= modelo.Detalles.Cantidad && modelo.Detalles.Cantidad > 0)
            {

                VentaDetalle ventaDetalle = modelo.Detalles;

                ventaDetalle.Monto = ventaDetalle.Cantidad * ventaDetalle.Precio;

                // Actualizar la cantidad en el inventario
                itemDelInventario.Cantidad -= ventaDetalle.Cantidad;
                await ReporitorioDeInventarios.EditarInventario(itemDelInventario);
                await RepositorioDeVentas.AñadaUnDetalleAlaVenta(ventaDetalle.Id_venta, ventaDetalle);

                List<Inventarios> inventarios = await ReporitorioDeInventarios.listeElInventarios();
                Venta venta = await RepositorioDeVentas.ObtengaUnaVentaPorId(ventaDetalle.Id_venta);
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
        public async Task<ActionResult >EliminarItem(VentaEnProcesoViewModel modelo)
        {
            try
            {
                Inventarios inventario = await ReporitorioDeInventarios.ObetenerInevtarioPorId(modelo.Detalles.Id_inventario);
                inventario.Cantidad += modelo.Detalles.Cantidad;
                await ReporitorioDeInventarios.EditarInventario(inventario);

                Venta venta = await RepositorioDeVentas.ObtengaUnaVentaPorId(modelo.Detalles.Id_venta);
                VentaDetalle item = venta.VentaDetalles.Find(d => d.Id == modelo.Detalles.Id);

                await RepositorioDeVentas.ElimineUnDetalleDeLaVenta(venta.Id, item);
                List<Inventarios> inventarios = await ReporitorioDeInventarios.listeElInventarios();

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
                AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(IdUsuario);
                var cajas = await RepositorioDeAperturaDeCaja.AperturasDeCajaPorUsuario(IdUsuario);
                  AperturaDeCaja caja = cajas.Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

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
                await RepositorioDeVentas.CreeUnaVenta(LaVenta);
                 var ventas = await RepositorioDeVentas.ListeLasVentasPorUsuario(IdUsuario);
                 LaVenta  = ventas.Where(v => v.Estado != EstadoVenta.Terminada).FirstOrDefault();

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
        public async Task<ActionResult>TerminarVenta(VentaEnProcesoViewModel modelo)
        {
            try
            {
                Venta venta = await  RepositorioDeVentas.ObtengaUnaVentaPorId(modelo.venta.Id);
                venta.TipoDePago = modelo.venta.TipoDePago;
               await  RepositorioDeVentas.EstablescaElTipoDePago(venta.Id, modelo.venta.TipoDePago);
               await  RepositorioDeVentas.TermineLaVenta(venta.Id);

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
                Venta venta = await RepositorioDeVentas.ObtengaUnaVentaPorId(modelo.venta.Id);
                await RepositorioDeVentas.ApliqueUnDescuento(venta.Id, modelo.venta.PorcentajeDesCuento);

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
