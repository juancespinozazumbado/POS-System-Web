using Inventario.Models.Dominio.Productos;

namespace Inventario.SI.Modelos.Dtos.Productos
{
    public class CrearAjusteDeInventarioRequest
    {
        public int Ajuste { get; set; }
        public int TipoAjuste { get; set; }

        public string Id_Usuario { get; set; }

        public string? Observaciones { get; set; }

    }
}
