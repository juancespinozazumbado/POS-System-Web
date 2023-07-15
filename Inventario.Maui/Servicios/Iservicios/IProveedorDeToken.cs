
namespace Inventario.Maui.Servicios.Iservicios
{
    public interface IProveedorDeToken
    {
        public void EscribirToken(string token);
        Task<string> ObtenerToken();
        public void LimpiarToken();

    }
}
