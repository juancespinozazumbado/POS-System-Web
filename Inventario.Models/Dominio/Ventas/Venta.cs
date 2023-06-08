

namespace Inventario.Models.Dominio.Ventas
{
    public class Venta {

        public int Id { get; set; } 
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; } 
        public TipoDePago TipoDePago { get; set; }

        public decimal Total { get; set; } 
        public decimal Subtotal { get; set; }    
        public int PorcentajeDeDescuento { get; set; }  

        public decimal MontoDescuento { set; get; }

        public EstadoVenta Estado { set; get; }

       public int UserId { get; set; }  
      
       public int IdAperturaDeCaja { get; set; } 
       public AperturaDeCaja AperturaDeCaja { get; set; }

       public List<VentaDetalle> VentaDetalles { get; set; }      

    }
}
