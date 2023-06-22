using System.ComponentModel.DataAnnotations;

namespace Inventario.WebApp.Models.Ventas
{

    public enum TipoDePago
    {
        [Display(Name = "Efectivo")]

        Efectivo = 1,
        [Display(Name = "Tarjeta")]
        Tarjeta = 2,
        [Display(Name = "SinpeMovil")]

        SinpeMovil = 3
    }

}
