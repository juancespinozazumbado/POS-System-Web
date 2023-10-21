using Inventario.WebApp.Models.Dto;

namespace Inventario.WebApp.Servicios.IServicio
{
    public interface IServicioBase
    {
        Task<RespuestaRestDto?> SendAsync(ConsultaRestDto requestDto, bool conBearer = true);
    }
}
