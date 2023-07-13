using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos;
using Inventario.SI.Modelos.Dtos.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Servicios.Autenticacion.Interfaces
{
    public interface IServicioDeAutenticacion
    {
        Task<RespuestaDto> Registro(RegistroRequestDto registroRequest);

        Task<RespuestaDto> Login(LoginRequestDto loginRequest);

        Task<RespuestaDto> CambiarContraseña(CambioDeContraseñaRequestDto request);

    }
}
