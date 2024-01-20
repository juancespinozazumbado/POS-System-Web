
using Inventario.Models.Dominio.Productos;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models.Dominio.Ventas
{
    public class VentaDetalle
    {
        public int Id { get; set; }
        public int Id_venta { get; set; }

        public Venta Venta { get; set; }

        public int Id_inventario { get; set; }

        
        public Inventarios Inventarios { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public decimal Monto { get; set; }

        public decimal MontoDescuento { get; set; }

    }
}
