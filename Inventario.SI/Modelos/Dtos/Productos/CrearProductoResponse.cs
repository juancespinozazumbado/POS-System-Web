using Inventario.Models.Dominio.Productos;

namespace Inventario.SI.Modelos.Dtos.Productos
{
    public class CrearProductoResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public Categoria Categoria { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

    }
}
