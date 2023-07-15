

using Inventario.Maui.Modelos;
using Inventario.Maui.Modelos.Dtos;

namespace Inventario.Maui.Servicios.Iservicios
{
    public interface IServicioDeAutenticacion
    {
        Task<LoginRespuestaDto?> LoginAsync(LoginRequestDto request);
        

    }
}
