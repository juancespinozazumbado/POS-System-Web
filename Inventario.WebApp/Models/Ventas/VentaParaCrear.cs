using Inventario.Models.Dominio.Productos;
using Inventario.Models.Dominio.Ventas;

namespace Inventario.WebApp.Areas.Ventas.Models
{
    public class VentaParaCrear
    {
        public VentaDetalle Detalles { get; set; }
        public Venta venta { get; set; }
        public List<ProductosAVender> productosAVender { get; set; }

        public List<Inventarios> Inventarios { get; set; }
    }
}
