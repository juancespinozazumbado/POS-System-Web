using Newtonsoft.Json.Linq;

namespace Inventario.WebApp.Models.ApiOpciones
{
    public class ApiOPciones
    {

        public static string? API_URL { get; set; }

        public const string CoqueToken = "JWTToken";

        public enum MetodoREST
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public enum TipoDeContenido
        {
            Json,
            Null
        }



    }
}
