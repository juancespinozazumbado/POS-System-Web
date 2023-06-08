
using Inventario.Models.Dominio.Productos;

namespace Inventario.Models.Dominio.Ventas
{
    public class VentaDetalle
    {
        public int Id { get; set; }
        public int VentaId { get; set; } 
        
        public Venta Venta { get; set; }

        public int InventariosId { get; set; } 
        
        public Inventarios Inventarios { get; set; } 

        public int Cantidad { get; set; }   

        public decimal Precio { get; set; }

        public decimal Monto { get; set; }

        public decimal MontoDescuento { get; set; }

    }
}
