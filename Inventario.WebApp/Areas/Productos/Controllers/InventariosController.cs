using Inventario.BL.Funcionalidades.Inventario;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.WebApp.Areas.Administracion.Controllers
{

    [Area("Productos")]
    [Authorize]
    public class InventariosController : Controller
    {
        private readonly ReporitorioDeInventarios _RepositorioDeInventarios;
        public InventariosController(InventarioDBContext contexto)
        {
            _RepositorioDeInventarios = new ReporitorioDeInventarios(contexto);
        }


        // GET: InventariosController
        public ActionResult Index( string nombre)
        {
            List<Inventarios> ListaDeItems;

            if (nombre == null)
            {
                ListaDeItems = (List<Inventarios>)_RepositorioDeInventarios.listeElInventarios();
            }else
            {
                ListaDeItems = (List<Inventarios>)_RepositorioDeInventarios.ListarInventariosPorNombre(nombre);    
            }
            
            return View(ListaDeItems);
        }

        // GET: InventariosController/Details/5
        public ActionResult Details(int id)
        {
            Inventarios inventario = _RepositorioDeInventarios.ObetenerInevtarioPorId(id);
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
        public ActionResult Create(Inventarios inventario)
        {
            try
            {

                _RepositorioDeInventarios.AgregarInventario(inventario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InventariosController/Edit/5
        public ActionResult Edit(int id)
        {
            Inventarios inventario = _RepositorioDeInventarios.ObetenerInevtarioPorId(id);
            return View(inventario);
        }

        // POST: InventariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inventarios inventario)
        {
            try
            {
                _RepositorioDeInventarios.EditarInventario(inventario);
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
