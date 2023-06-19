using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Ventas;

namespace Inventario.WebApp.Models
{
    public class MostrarVentasInventarios
    {

        public IEnumerable<Inventarios> Inventario { get; set; }
        public Venta VentaActual { get; set; }

    }
}
