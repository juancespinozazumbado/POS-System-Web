using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Usuarios;
using Inventario.WebApp.Areas.Productos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inventario.WebApp.Areas.Administracion.Controllers
{
    [Area("Productos")]
    [Authorize]
    public class AjustesDeInventarioController : Controller
    {
        private readonly RepositorioDeAjusteDeInventario _RepositorioDeAjustes;
        private readonly ReporitorioDeInventarios _RepositorioDeInventarios;
        private readonly RepositorioDeUsuarios _RpepositorioDeUsuarios;

        public AjustesDeInventarioController(InventarioDBContext context)
        {
            _RepositorioDeAjustes = new(context);
            _RepositorioDeInventarios = new(context);
            _RpepositorioDeUsuarios = new(context);
        }
        public ActionResult Index(string nombre )
        {
            List<Inventarios> ListaDeItems;

            if (nombre == null)
            {
                ListaDeItems = (List<Inventarios>)_RepositorioDeInventarios.listeElInventarios();
            }
            else
            {
                ListaDeItems = (List<Inventarios>)_RepositorioDeInventarios.ListarInventariosPorNombre(nombre);
            }

            return View(ListaDeItems);
        }

        // GET: AjustestDeInventarioController/Details/5

        public ActionResult ListaDeAjustes(int id)
        {
            Inventarios invenntario = _RepositorioDeInventarios.ObetenerInevtarioPorId(id);
            return View(invenntario);
        }

        public ActionResult DetalleAjuste(int InventarioId, int Id)
        {

            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AplicationUser usaurioActual = _RpepositorioDeUsuarios.ObtengaUnUsuarioPorId(id);
            Inventarios invenntario = _RepositorioDeInventarios.ObetenerInevtarioPorId(InventarioId);
            AjusteDeInventario ajuste = invenntario.Ajustes.Where(a => a.Id == Id).FirstOrDefault();
            AjusteViweModel modelo = new AjusteViweModel {Inventario = invenntario, Ajuste = ajuste , usuario = usaurioActual };
            return View(modelo);
        }

        // GET: AjustestDeInventarioController/Create

        public ActionResult CrearAjuste(int id)
        {
            Inventarios inventario = _RepositorioDeInventarios.ObetenerInevtarioPorId(id);

            AjusteViweModel ajuste = new();
            ajuste.Inventario = inventario;


            return View(ajuste);
        }

        // POST: AjustestDeInventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(AjusteViweModel ajustes)
        {
            try
            {
                int id = ajustes.Inventario.Id;
                ajustes.Ajuste.Fecha = DateTime.Now;
                ajustes.Ajuste.Id_Inventario = id;
                ajustes.Ajuste.UserId = "";
                _RepositorioDeAjustes.AgegarAjusteDeInventario(id, ajustes.Ajuste);
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

    }
}
