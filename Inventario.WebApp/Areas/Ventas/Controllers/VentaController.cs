using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.BL.Funcionalidades.Ventas;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
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
    public class VentaController : Controller
    {
        RepositorioDeVentas RepositorioDeVentas;
        ReporitorioDeInventarios ReporitorioDeInventarios;
        RepositorioDeUsuarios RepositorioDeUsuarios;
        RepositorioDeAperturaDeCaja RepositorioDeAperturaDeCaja;
        public VentaController(InventarioDBContext context)
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
            UsuarioParaCerar modelo = new() { Usuario = usaurioActual };

            AperturaDeCaja? caja = RepositorioDeAperturaDeCaja.AperturasDeCajaPorUsuario(id)
               .Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();
            if (caja != null)
                modelo.TieneUnaCajaAbierta = true;


            return View(modelo);

        }

        // GET: VentasController/Create
        public ActionResult CrearVenta()
        {

            string IdUsuario = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = RepositorioDeUsuarios.ObtengaUnUsuarioPorId(IdUsuario);
            AperturaDeCaja? caja = RepositorioDeAperturaDeCaja.AperturasDeCajaPorUsuario(IdUsuario)
              .Where(c => c.estado == EstadoCaja.Abierta).FirstOrDefault();

            Venta? ventaAbierta = RepositorioDeVentas.ListeLasVentasPorUsuario(IdUsuario)
               .Where(v => v.Estado == EstadoVenta.EnProceso).FirstOrDefault();
            List<Inventarios> inventarios = (List<Inventarios>)ReporitorioDeInventarios.listeElInventarios();
            VentaParaCrear modelo = new() { Inventarios = inventarios, Detalles = new() };

            if (ventaAbierta == null)
            {
                Venta venta = new()
                {
                    AperturaDeCaja = caja,
                    IdAperturaDeCaja = caja.Id,
                    UserId = IdUsuario

                };
                modelo.venta = venta;

            }else
            modelo.venta = ventaAbierta;

            return View(modelo);


        }

        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Venta venta)
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
                LaVenta = RepositorioDeVentas.ListeLasVentasPorUsuario(IdUsuario)
                    .Where(v => v.Estado != EstadoVenta.Terminada).FirstOrDefault();

                return RedirectToAction(nameof(CrearVenta), LaVenta);
            }
            catch (Exception e)
            {
                e = null;
                return View(nameof(CrearVenta));
            }
        }

        // POST: VentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarItem(VentaParaCrear modelo)
        {
            try
            {

                VentaDetalle item = modelo.Detalles;
                int id_venta = modelo.Detalles.Id_venta;
                int id_Inventario = modelo.Detalles.Id_inventario;

                Venta venta = RepositorioDeVentas.ObtengaUnaVentaPorId(id_venta);
                Inventarios inventario = ReporitorioDeInventarios.ObetenerInevtarioPorId(id_Inventario);
                item.Cantidad = modelo.Detalles.Cantidad;
                item.Id_venta = id_venta;
                item.Id_inventario = id_Inventario;
                item.Inventarios = inventario;
                item.Venta = venta;
                item.Precio = inventario.Precio;
                item.Monto = item.Precio * item.Cantidad;






                List<Inventarios> inventarios = (List<Inventarios>)ReporitorioDeInventarios.listeElInventarios();
                RepositorioDeVentas.AñadaUnDetalleAlaVenta(id_venta, item);

                venta = RepositorioDeVentas.ObtengaUnaVentaPorId(id_venta);
                VentaParaCrear VentaParaCrear = new()
                {
                    Inventarios = inventarios,
                    Detalles = new(),
                    venta = venta
                };

                return RedirectToAction(nameof(CrearVenta), VentaParaCrear);
            }
            catch (Exception e)
            {
                e = null;
                return View();
            }
        }


        // POST: VentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarItem(VentaParaCrear modelo)
        {
            try
            {

                int id_venta = modelo.Detalles.Id_venta;
                int id = modelo.Detalles.Id;

                Venta venta = RepositorioDeVentas.ObtengaUnaVentaPorId(id_venta);
                VentaDetalle item = venta.VentaDetalles.Find(d => d.Id == id);

                RepositorioDeVentas.ElimineUnDetalleDeLaVenta(id_venta, item);

                List<Inventarios> inventarios = (List<Inventarios>)ReporitorioDeInventarios.listeElInventarios();


                venta = RepositorioDeVentas.ObtengaUnaVentaPorId(id_venta);
                VentaParaCrear VentaParaCrear = new()
                {
                    Inventarios = inventarios,
                    Detalles = new(),
                    venta = venta
                };

                return RedirectToAction(nameof(CrearVenta), VentaParaCrear);
            }
            catch (Exception e)
            {
                e = null;
                return View();
            }
        }
    





    // POST: VentasController/Edit/5
 
    public ActionResult TerminarLaVenta(int id)
    {
        try
        {


                //Venta venta = RepositorioDeVentas.ObtengaUnaVentaPorId(id_venta);
                //VentaDetalle item = venta.VentaDetalles.Find(d => d.Id == id);

                //RepositorioDeVentas.ElimineUnDetalleDeLaVenta(id_venta, item);

                //List<Inventarios> inventarios = (List<Inventarios>)ReporitorioDeInventarios.listeElInventarios();


                //venta = RepositorioDeVentas.ObtengaUnaVentaPorId(id_venta);
                //VentaParaCrear VentaParaCrear = new()
                //{
                //    Inventarios = inventarios,
                //    Detalles = new(),
                //    venta = venta
                //};

                RepositorioDeVentas.TermineLaVenta(id);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            e = null;
            return View();
        }
    }
 }
}


