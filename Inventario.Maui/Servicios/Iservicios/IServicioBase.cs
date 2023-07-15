
using Inventario.Maui.Modelos;
using Inventario.Maui.Modelos.Dtos;

namespace Inventario.Maui.Servicios.Iservicios
{
    public interface IServicioBase
    {
        Task<RespuestaRest?> SendAsync(ConsultaRest requestDto, bool conBearer = true);
    }
}
