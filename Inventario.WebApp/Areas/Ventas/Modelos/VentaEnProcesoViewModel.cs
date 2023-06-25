using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Ventas;

namespace Inventario.WebApp.Areas.Ventas.Modelos
{
    public class VentaEnProcesoViewModel
    {
        public VentaDetalle Detalles { get; set; }
        public Venta venta { get; set; }
      
        public List<Inventarios> Inventarios { get; set; }
    }
}
