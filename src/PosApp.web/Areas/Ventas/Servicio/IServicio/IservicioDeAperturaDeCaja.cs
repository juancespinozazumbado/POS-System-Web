using Inventario.Models.Dominio.Ventas;
using Inventario.WebApp.Areas.Ventas.Modelos.Dtos;

namespace Inventario.WebApp.Areas.Ventas.Servicio.IServicio
{
    public interface IservicioDeAperturaDeCaja
    {
        public Task<bool> CrearUnaAperturaDeCaja(AperturadeCajaDto aperturaDeCaja);

        public Task<List<AperturaDeCaja>> AperturasDeCajaPorUsuario(string idUsuario);

        public Task<AperturaDeCaja> ObtenerUnaAperturaDeCajaPorId(int id, string id_usuario);

        public Task<bool> CerrarUnaAperturaDeCaja(int id, string id_usuario);

    }
}
