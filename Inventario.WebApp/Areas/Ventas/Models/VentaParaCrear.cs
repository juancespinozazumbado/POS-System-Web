using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;
using Inventario.Models.Dominio.Productos;

namespace Inventario.WebApp.Areas.Ventas.Models
{
    public class VentaParaCrear
    {
        public VentaDetalle Detalles { get; set; }
        public Venta venta { get; set; }

        public List<Inventarios> Inventarios { get; set; }
    }
}
