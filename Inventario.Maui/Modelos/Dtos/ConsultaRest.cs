
using static Inventario.Maui.Modelos.ConfiguracionApi;

namespace Inventario.Maui.Modelos.Dtos
{
    public class ConsultaRest
    {
        public string? URL { get; set; }
        public Object Cuerpo { get; set; } = null;
        public MetodoREST MetodoRest { get; set; } = MetodoREST.GET;
        public TipoDeContenido TipoDeContenido { get; set; } = TipoDeContenido.Json;

    }
}
