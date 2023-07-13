using Inventario.Models.Dominio.Usuarios;

namespace Inventario.SI.Modelos.Dtos.Usuarios
{
    public class LoginResponsetDto
    {
        public AplicationUser Usuario { get; set; }
        public string Token { get; set; }

    }
}
