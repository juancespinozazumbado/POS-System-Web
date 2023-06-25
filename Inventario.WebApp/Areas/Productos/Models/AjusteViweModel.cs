using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Usuarios;

namespace Inventario.WebApp.Areas.Productos.Models
{
    public class AjusteViweModel
    {

        public Inventarios Inventario { get; set; }
        public AjusteDeInventario Ajuste { get; set; }

        public AplicationUser usuario { get; set; } 
    }
}
