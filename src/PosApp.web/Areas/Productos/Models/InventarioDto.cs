using Inventario.Models.Dominio.Productos;

namespace Inventario.WebApp.Areas.Productos.Models
{
    public class InventarioDto
    {
        public string Nombre { get; set; }
        public Categoria Categoria { get; set; }

        public decimal Precio { get; set; }
    }
}
