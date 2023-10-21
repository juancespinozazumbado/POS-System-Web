using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos.Dtos.Autenticacion;

namespace Inventario.SI.Modelos.Dtos.Usuarios
{
    public class LoginResponsetDto
    {
        public UsuarioDto Usuario { get; set; }
        public string Token { get; set; }

    }
}
