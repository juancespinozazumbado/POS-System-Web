namespace Inventario.WebApp.Servicios.IServicio
{
    public interface IProveedorDeToken
    {
        public void EscribirToken(string token);
        public string? ObtenerToken();
        public void LimpiarToken();
    }
}
