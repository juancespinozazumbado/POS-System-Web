using System.ComponentModel.DataAnnotations;

namespace Inventario.WebApp.Models.Products
{
    public enum TipoAjuste
    {
        [Display(Name = "Auemnto")]
        aumento = 1,
        [Display(Name = "Disminucion")]
        disminucion = 2
    }
}
