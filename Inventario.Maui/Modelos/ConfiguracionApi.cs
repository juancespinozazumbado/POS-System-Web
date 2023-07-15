

using static System.Net.WebRequestMethods;

namespace Inventario.Maui.Modelos
{
    public class ConfiguracionApi
    {
        public static string? API_URL { get;  } = "https://apiinventario.azurewebsites.net/api";

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
