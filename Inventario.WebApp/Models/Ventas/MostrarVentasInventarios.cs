

using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Ventas;

namespace Inventario.WebApp.Models.Ventas
{
    public class MostrarVentasInventarios
    {

        public IEnumerable<Inventarios> Inventario { get; set; }
        public IEnumerable<ProductosAVender> ProductosAVender { get; set; }

    }
}
