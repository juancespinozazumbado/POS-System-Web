namespace Inventario.Models.Dominio.Ventas
{
    public class ProductosAVender
    {
        public int idDelProducto { get; set; }

        public string NombreDelProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public decimal MontoDeDescuento { get; set; }
        public decimal Total { get; set; }
        public int IdDeVenta { get; set; }

    }
}
