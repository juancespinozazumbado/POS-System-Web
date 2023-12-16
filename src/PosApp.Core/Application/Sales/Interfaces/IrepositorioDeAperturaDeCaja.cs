using Inventario.Models.Dominio.Ventas;

namespace Inventario.BL.Funcionalidades.Ventas.Interfaces
{
    public interface IrepositorioDeAperturaDeCaja
    {
        public Task<bool> CrearUnaAperturaDeCaja(AperturaDeCaja aperturaDeCaja);

        public Task<List<AperturaDeCaja>> ListarAperturasDeCaja();

        public Task<List<AperturaDeCaja>> AperturasDeCajaPorUsuario(string idUsuario);

        public Task<AperturaDeCaja> ObtenerUnaAperturaDeCajaPorId(int id);

        public Task<bool> CerrarUnaAperturaDeCaja(int id);

    }
}
