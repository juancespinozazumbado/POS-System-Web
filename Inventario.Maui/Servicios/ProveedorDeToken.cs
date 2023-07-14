

using Inventario.Maui.Modelos;
using Inventario.Maui.Servicios.Iservicios;

namespace Inventario.Maui.Servicios
{
    public class ProveedorDeToken : IProveedorDeToken
    {
        public async void EscribirToken(string token)
        {
           await SecureStorage.SetAsync(ConfiguracionApi.CoqueToken, token);
        }

        public  void LimpiarToken()
        {
             SecureStorage.Remove(ConfiguracionApi.CoqueToken);
            
        }

        public async Task<string> ObtenerToken()
        {
            var token = await SecureStorage.GetAsync(ConfiguracionApi.CoqueToken);
            return token;
        }
    }
}
