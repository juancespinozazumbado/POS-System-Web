using Inventario.BL.Funcionalidades.Inventario;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Inventario.WebApp.Areas.Productos.Models;
using Inventario.WebApp.Areas.Productos.Servicio.Iservicio;
using Inventario.WebApp.Servicios.IServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.WebApp.Areas.Administracion.Controllers
{

    [Area("Productos")]
    [Authorize]
    public class InventariosController : Controller
    {
        private readonly IServicioDeInventario _servicioDeInventario;
        
        public InventariosController(IServicioDeInventario servicioDeInventario)
        {
            _servicioDeInventario = servicioDeInventario;
        }


        // GET: InventariosController
        public async Task<ActionResult> Index( string nombre)
        {
            

            List<Inventarios> ListaDeItems;

            if (nombre == null)
            {
                ListaDeItems = await _servicioDeInventario.ListarInventarios();

            }else
            {
                ListaDeItems = await _servicioDeInventario.InventariosPorNombre(nombre);    
            }

            if (ListaDeItems == null) ListaDeItems = new();
            
            return View(ListaDeItems);
        }

        // GET: InventariosController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Inventarios inventario = await _servicioDeInventario.InvenatrioPorId(id);
            return View(inventario);
        }

        // GET: InventariosController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: InventariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InventarioDto inventario)
        {
            try
            { 
                var resultado = await  _servicioDeInventario.AgregarInvenatrio(inventario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InventariosController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Inventarios inventario = await _servicioDeInventario.InvenatrioPorId(id);
            return View(inventario);
        }

        // POST: InventariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(Inventarios inventario)
        {
            try
            {
                int id = inventario.Id;

                await  _servicioDeInventario.EditarInvenatrio( id, new InventarioDto()
                {
                    Nombre = inventario.Nombre,
                    Precio = inventario.Precio,
                    Categoria = (Productos.Models.Categoria)inventario.Categoria
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InventariosController/Delete/5
        
    }
}
