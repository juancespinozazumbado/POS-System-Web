using Inventario.WebApp.Models.ApiOpciones;
using Inventario.WebApp.Servicios.IServicio;

namespace Inventario.WebApp.Servicios
{
    public class ProveedorDeToken : IProveedorDeToken
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ProveedorDeToken(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;   
        }
        public void EscribirToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(ApiOPciones.CoqueToken, token);
        }

        public void LimpiarToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(ApiOPciones.CoqueToken);
        }

        public string? ObtenerToken()
        {
            string? token = null;
            bool? tieneToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ApiOPciones.CoqueToken, out token);
            return tieneToken is true ? token : null;
        }

    }
}
