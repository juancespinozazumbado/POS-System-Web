using Inventario.WebApp.Areas.Autenticacion.Models;
using Inventario.WebApp.Models.Dto;

namespace Inventario.WebApp.Areas.Autenticacion.Servicio
{
    public interface IServicioDeAutenticacion
    {
        Task<RespuestaRestDto?> LoginAsync(LoginDto request);
        Task<RespuestaRestDto?> Registro(RegistroDto request);
        public Task<RespuestaRestDto?> CambiarClave(CambioDeClaveDto request);


    }
}
