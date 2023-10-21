using static Inventario.WebApp.Models.ApiOpciones.ApiOPciones;

namespace Inventario.WebApp.Models.Dto
{
    public class ConsultaRestDto
    {
        public string? URL { get; set; }
        public Object Cuerpo { get; set; } = null;
        public MetodoREST MetodoRest { get; set; } = MetodoREST.GET;
        public TipoDeContenido TipoDeContenido { get; set; } = TipoDeContenido.Json;




    }
}
