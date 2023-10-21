using System.ComponentModel.DataAnnotations;

namespace Inventario.WebApp.Areas.Productos.Models
{
    public enum TipoAjuste
    {
        [Display(Name = "Aumento")]
        aumento = 1,
        [Display(Name = "Disminución")]
        disminucion = 2
    }
}
