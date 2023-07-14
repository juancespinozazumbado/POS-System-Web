using Inventario.Maui.Servicios.Iservicios;
using Inventario.Models.Dominio.Productos;

namespace Inventario.Maui.Servicios
{

    public class ServicioDeInventario : IServicioDeInventario
    {
        Task<Inventarios> IServicioDeInventario.InvenatrioPorId(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Inventarios>> IServicioDeInventario.InventariosPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        Task<List<Inventarios>> IServicioDeInventario.ListarInventarios()
        {
            throw new NotImplementedException();
        }
    }
}
