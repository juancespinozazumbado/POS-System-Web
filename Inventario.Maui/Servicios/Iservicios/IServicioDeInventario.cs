
using Inventario.Models.Dominio.Productos;

namespace Inventario.Maui.Servicios.Iservicios
{
    public interface IServicioDeInventario
    {
        Task<List<Inventarios>> ListarInventarios();
        Task<Inventarios> InvenatrioPorId(int id);
        Task<List<Inventarios>> InventariosPorNombre(string nombre);
       
    }
}
