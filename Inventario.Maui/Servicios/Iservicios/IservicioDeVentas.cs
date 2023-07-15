

using Inventario.Maui.Modelos.Ventas;

namespace Inventario.Maui.Servicios.Iservicios
{
    public interface IservicioDeVentas
    {
        public Task<List<AperturaDeCaja>> AperturasDeCajaPorUsuario(string idUsuario);
    }
}
