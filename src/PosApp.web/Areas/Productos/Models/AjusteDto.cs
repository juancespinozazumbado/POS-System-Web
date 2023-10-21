using Inventario.Models.Dominio.Productos;
using Inventario.WebApp.Areas.Ventas.Modelos;

namespace Inventario.WebApp.Areas.Productos.Models
{
    public class AjusteDto
    {
        public int Ajuste { get; set; }
        public TipoAjuste TipoAjuste { get; set; }

        public string Id_Usuario { get; set; }

        public string? Observaciones { get; set; }

        
    }
}
