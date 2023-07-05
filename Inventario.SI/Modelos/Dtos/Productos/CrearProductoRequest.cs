using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Modelos.Dtos.Productos
{
    public class CrearProductoRequest
    {
        public string Nombre { get; set; }
        public int Categoria { get; set; }
        public decimal Precio { get; set; }

    }
}
