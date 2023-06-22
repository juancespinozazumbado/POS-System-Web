using Inventario.Models.Dominio.Productos;

namespace Inventario.WebApp.Areas.Productos.Models
{
    public class CrearAjuste
    {

        public Inventarios Inventario { get; set; }
        public AjusteDeInventario Ajuste { get; set; }
    }
}
