using Inventario.Models.Dominio.Usuarios;

namespace Inventario.SI.Servicios.Autenticacion.Interfaces
{
    public interface IServicioDeJWT
    {
        string GenerarToken(AplicationUser usuario);
    }
}
