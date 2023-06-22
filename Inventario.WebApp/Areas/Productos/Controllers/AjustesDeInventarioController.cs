using Inventario.BL.Funcionalidades.Inventario;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Inventario.WebApp.Areas.Productos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.WebApp.Areas.Administracion.Controllers
{
    [Area("Productos")]
    [Authorize]
    public class AjustesDeInventarioController : Controller
    {
        private readonly RepositorioDeAjusteDeInventario _Repo;
        private readonly ReporitorioDeInventarios _RepoInventarios;

        public AjustesDeInventarioController(InventarioDBContext context)
        {
            _Repo = new(context);
            _RepoInventarios = new(context);
        }
        public ActionResult Index()
        {
            List<Inventarios> inventarios = (List<Inventarios>)_RepoInventarios.listeElInventarios();
            return View(inventarios);
        }

        // GET: AjustestDeInventarioController/Details/5

        public ActionResult Details(int id)
        {
            Inventarios invenntario = _RepoInventarios.ObetenerInevtarioPorId(id);
            return View(invenntario);
        }

        // GET: AjustestDeInventarioController/Create

        public ActionResult Create(int id)
        {
            Inventarios inventario = _RepoInventarios.ObetenerInevtarioPorId(id);

            CrearAjuste ajuste = new();
            ajuste.Inventario = inventario;


            return View(ajuste);
        }

        // POST: AjustestDeInventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CrearAjuste ajustes)
        {
            try
            {
                int id = ajustes.Inventario.Id;
                ajustes.Ajuste.Fecha = DateTime.Now;
                ajustes.Ajuste.Id_Inventario = id;
                ajustes.Ajuste.UserId = "";
                _Repo.AgegarAjusteDeInventario(id, ajustes.Ajuste);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AjustestDeInventarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AjustestDeInventarioController/Edit/5
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

        // GET: AjustestDeInventarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AjustestDeInventarioController/Delete/5
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
