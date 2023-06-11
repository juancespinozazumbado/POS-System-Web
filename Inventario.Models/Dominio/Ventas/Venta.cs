using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models.Dominio.Ventas
{
    [Table("Ventas")]
    public class Venta
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("NombreCliente")]
        public string NombreCliente { get; set; } = null!;

        [Column("Fecha")]
        public DateTime Fecha { get; set; }

        [Column("TipoDePago")]
        public int TipoDePago { get; set; }

        [Column("Total")]
        public decimal Total { get; set; }

        [Column("SubTotal")]
        public decimal SubTotal { get; set; }

        [Column("PorcentajeDesCuento")]
        public int PorcentajeDesCuento { get; set; }

        [Column("MontoDescuento")]
        public decimal MontoDescuento { get; set; }

        [Column("UserId")]
        public string UserId { get; set; } = null!;

        [Column("Estado")]
        public EstadoVenta Estado { get; set; }

        [Column("IdAperturaDeCaja")]
        public int IdAperturaDeCaja { get; set; }

        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
    }
}
