using Inventario.Models.Dominio.Productos;

namespace Inventario.WebApp.Areas.Productos.Models
{
    public class CrearInventario
    {


        public string Nombre { get; set; }

        public Categoria Categoria { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public List<AjusteDeInventario>? Ajustes { get; set; }
    }
}
