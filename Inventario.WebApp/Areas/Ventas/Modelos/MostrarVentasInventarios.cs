using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Ventas;

namespace Inventario.WebApp.Areas.Ventas.Modelos
{
    public class MostrarVentasInventarios
    {

        public IEnumerable<Inventarios> Inventario { get; set; }
        public IEnumerable<ProductosAVender> ProductosAVender { get; set; }

    }
}
