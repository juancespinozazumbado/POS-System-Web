namespace Inventario.Models.Dominio.Ventas
{
    public class MostrarVentasInventarios
    {

        public IEnumerable<Productos.Inventarios> Inventario { get; set; }
        public Venta VentaActual { get; set; }

    }
}
