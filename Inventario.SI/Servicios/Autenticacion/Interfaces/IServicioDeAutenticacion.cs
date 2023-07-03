using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos.Dtos.Usuarios;

namespace Inventario.SI.Servicios.Autenticacion.Interfaces
{
    public interface IServicioDeAutenticacion
    {
        public Task<RegistroResponsetDto> Registro(RegistroRequestDto registroRequest);

        Task<LoginResponsetDto> Login(LoginRequestDto loginRequest);

       
    }
}
