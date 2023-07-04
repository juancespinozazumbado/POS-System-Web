using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos.Dtos.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Servicios.Autenticacion.Interfaces
{
    public interface IServicioDeAutenticacion
    {
        public Task<ActionResult<Object>> Registro(RegistroRequestDto registroRequest);

        Task<LoginResponsetDto> Login(LoginRequestDto loginRequest);

       
    }
}
