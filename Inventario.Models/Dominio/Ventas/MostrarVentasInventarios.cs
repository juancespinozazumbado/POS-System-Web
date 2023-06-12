namespace Inventario.Models.Dominio.Ventas
{
    public class MostrarVentasInventarios
    {

        public IEnumerable<Productos.Inventarios> Inventario { get; set; }
        public IEnumerable<Venta> Ventas { get; set; }

    }
}
