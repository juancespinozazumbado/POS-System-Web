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
        private readonly ReporitorioDeInventarios _Repo;
        public InventariosController(InventarioDBContext contexto)
        {
            _Repo = new ReporitorioDeInventarios(contexto);
        }


        // GET: InventariosController
        public ActionResult Index()
        {
            List<Inventarios> lista = (List<Inventarios>)_Repo.listeElInventarios();
            return View(lista);
        }

        // GET: InventariosController/Details/5
        public ActionResult Details(int id)
        {
            Inventarios inventario = _Repo.ObetenerInevtarioPorId(id);
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

                _Repo.AgregarInventario(inventario);
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
            Inventarios inventario = _Repo.ObetenerInevtarioPorId(id);
            return View(inventario);
        }

        // POST: InventariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inventarios inventario)
        {
            try
            {
                _Repo.EditarInventario(inventario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InventariosController/Delete/5
        public ActionResult Delete(int id)
        {
            Inventarios inventario = _Repo.ObetenerInevtarioPorId(id);
            return View(inventario);
        }

        // POST: InventariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Inventarios inventario)
        {
            try
            {
                _Repo.EliminarInventario(inventario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
