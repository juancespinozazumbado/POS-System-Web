using Inventario.Models.Dominio.Productos;

namespace Funcionalidades.Inventario.Interfaces
{
    public interface IRepositorioDeInventarios
    {
        public Task<bool> AgregarInventario(Inventarios inventario);
        public Task<List<Inventarios>> listeElInventarios();

        public Task<bool> EliminarInventario(Inventarios inventario);

        public Task<bool> EditarInventario(Inventarios inventario);

        public Task<Inventarios> ObetenerInevtarioPorId(int id);

        public Task<List<Inventarios>> ListarInventariosPorNombre(string nombre);


    }
}
