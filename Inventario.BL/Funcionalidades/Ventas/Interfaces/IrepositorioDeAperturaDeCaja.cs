using Inventario.Models.Dominio.Ventas;

namespace Inventario.BL.Funcionalidades.Ventas.Interfaces
{
    public interface IrepositorioDeAperturaDeCaja
    {
        public void CrearUnaAperturaDeCaja(AperturaDeCaja aperturaDeCaja);

        public IEnumerable<AperturaDeCaja> ListarAperturasDeCaja();

        public IEnumerable<AperturaDeCaja> AperturasDeCajaPorUsuario(string idUsuario);

        public AperturaDeCaja ObtenerUnaAperturaDeCajaPorId(int id);

        public void CerrarUnaAperturaDeCaja(int id);

    }
}
