using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Maui.Modelos.Ventas
{
   
    public class Venta
    {
        
       public int Id { get; set; }


        
        public string NombreCliente { get; set; } = null!;

        public DateTime Fecha { get; set; }


        public TipoDePago TipoDePago { get; set; }


        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Total { get; set; }


        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal SubTotal { get; set; }

   
        public int PorcentajeDesCuento { get; set; }

   
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal MontoDescuento { get; set; }


        public string UserId { get; set; } = null!;

 
        public EstadoVenta Estado { get; set; }


        public int IdAperturaDeCaja { get; set; }

        public AperturaDeCaja AperturaDeCaja { get; set; }

        public virtual List<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
    }
}
