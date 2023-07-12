using Inventario.Models.Dominio.Usuarios;
using Inventario.SI.Modelos;
using Inventario.SI.Modelos.Dtos.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.SI.Servicios.Autenticacion.Interfaces
{
    public interface IServicioDeAutenticacion
    {
        Task<RespuestaDeAutenticacion<RegistroResponseDto>> Registro(RegistroRequestDto registroRequest);

        Task<RespuestaDeAutenticacion<LoginResponsetDto>> Login(LoginRequestDto loginRequest);

        Task<RespuestaDeAutenticacion<string>> CambiarContraseña(CambioDeContraseñaRequestDto request);

    }
}
