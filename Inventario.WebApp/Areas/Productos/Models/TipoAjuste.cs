using System.ComponentModel.DataAnnotations;

namespace Inventario.WebApp.Areas.Productos.Models
{
    public enum TipoAjuste
    {
        [Display(Name = "Auemnto")]
        aumento = 1,
        [Display(Name = "Disminucion")]
        disminucion = 2
    }
}
