using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventario.WebApp.Areas.Productos.Models
{
    public enum Categoria
    {
        [Display(Name = "A: artículos caros y de alta gama con controles estrictos e inventarios reducidos")]
        A = 1,
        [Display(Name = "B: artículos de precio medio, de prioridad media, con un volumen de ventas y unas existencias medias")]
        B = 2,
        [Display(Name = "B: : artículos de bajo valor y bajo coste con grandes ventas y enormes inventarios")]
        C = 3
    }
}
