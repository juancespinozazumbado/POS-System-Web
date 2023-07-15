
using Inventario.Maui.Modelos;

namespace Inventario.Maui.Servicios.Iservicios
{
    public interface IServicioDeInventario
    {
        Task<List<InventarioModelo>> ListarInventarios();
        Task<InventarioModelo> InvenatrioPorId(int id);
        Task<List<InventarioModelo>> InventariosPorNombre(string nombre);
       
    }
}
