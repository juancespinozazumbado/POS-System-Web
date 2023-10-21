using Inventario.Models.Dominio.Productos;
using Inventario.WebApp.Areas.Productos.Models;
using Inventario.WebApp.Models.Dto;

namespace Inventario.WebApp.Areas.Productos.Servicio.Iservicio
{
    public interface IservicioDeAjustesDeInventario
    {
    
        public Task<bool> AgegarAjusteDeInventario(int id, AjusteDto ajusteDeInventario);

        public Task<AjusteDeInventario> ObtenerAjustePorId(int id, int id_detalle);
    }
}
