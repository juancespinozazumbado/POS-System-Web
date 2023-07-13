using Inventario.BL.Funcionalidades.Inventario;
using Inventario.BL.Funcionalidades.Usuarios;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Usuarios;
using Inventario.WebApp.Areas.Productos.Models;
using Inventario.WebApp.Areas.Productos.Servicio.Iservicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Inventario.WebApp.Areas.Administracion.Controllers
{
    [Area("Productos")]
    [Authorize]
    public class AjustesDeInventarioController : Controller
    {
       
        private readonly IservicioDeAjustesDeInventario _servicioDeAjustes;
        private readonly IServicioDeInventario _serviciodeInventario;
        

        public AjustesDeInventarioController( 
            IServicioDeInventario serviciodeInventario, IservicioDeAjustesDeInventario servicioDeAjustes)
        {
           
            _serviciodeInventario = serviciodeInventario;
            _servicioDeAjustes = servicioDeAjustes;
         
        }
        public async Task<ActionResult> Index(string Nombre)
        {
            List<Inventarios> inventarios = new();

            if (Nombre == null)
            {
              inventarios = await _serviciodeInventario.ListarInventarios();

            }else
                 inventarios = await _serviciodeInventario.InventariosPorNombre(Nombre);
            return View(inventarios);
        }

        // GET: AjustestDeInventarioController/Details/5

        public async Task<ActionResult> ListaDeAjustes(int id)
        {
            Inventarios invenntario = await _serviciodeInventario.InvenatrioPorId(id);     
            return View(invenntario);
        }

        public async Task<ActionResult> DetalleAjuste(int InventarioId, int Id)
        {

           
            Inventarios invenntario = await _serviciodeInventario.InvenatrioPorId(InventarioId);
            AjusteDeInventario ajuste = invenntario.Ajustes.Where(a => a.Id == Id).FirstOrDefault();
            AjusteViweModel modelo = new AjusteViweModel {Inventario = invenntario, Ajuste = ajuste , usuario = ajuste.UserId };
            return View(modelo);
        }

        // GET: AjustestDeInventarioController/Create

        public async Task<ActionResult> CrearAjuste(int id)
        {
            Inventarios inventario = await _serviciodeInventario.InvenatrioPorId(id);

            AjusteViweModel ajuste = new();
            ajuste.Inventario = inventario;


            return View(ajuste);
        }

        // POST: AjustestDeInventarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(AjusteViweModel ajustes)
        {
            try
            {
               
                 var st = User.Identities.FirstOrDefault().Claims.Count();

                var idt = HttpContext.User.Identity.Name;
                int id = ajustes.Inventario.Id;
                ajustes.Ajuste.Fecha = DateTime.Now;
                ajustes.Ajuste.Id_Inventario = id;
                ajustes.Ajuste.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //_RepositorioDeAjustes.AgegarAjusteDeInventario(id, ajustes.Ajuste);

                AjusteDto ajusteDto = new() {Ajuste =  ajustes.Ajuste.Ajuste , 
                    Id_Usuario = ajustes.Ajuste.UserId, TipoAjuste = (Productos.Models.TipoAjuste)ajustes.Ajuste.Tipo,
                    Observaciones = ajustes.Ajuste.Observaciones  };
                var resultado = await _servicioDeAjustes.AgegarAjusteDeInventario(id, ajusteDto);
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
