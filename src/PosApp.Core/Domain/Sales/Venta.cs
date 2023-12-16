using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models.Dominio.Ventas
{
   
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        [Column("NombreCliente")]
        
        public string NombreCliente { get; set; } = null!;

        [Column("Fecha")]
        public DateTime Fecha { get; set; }

        [Column("TipoDePago")]
        public TipoDePago TipoDePago { get; set; }

        [Column("Total")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Total { get; set; }

        [Column("SubTotal")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal SubTotal { get; set; }

        [Column("PorcentajeDesCuento")]
        public int PorcentajeDesCuento { get; set; }

        [Column("MontoDescuento")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal MontoDescuento { get; set; }

        [Column("UserId")]
        public string UserId { get; set; } = null!;

        [Column("Estado")]
        public EstadoVenta Estado { get; set; }

        //[Column("IdAperturaDeCaja")]
        public int IdAperturaDeCaja { get; set; }

        public AperturaDeCaja AperturaDeCaja { get; set; }

        public virtual List<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
    }
}
