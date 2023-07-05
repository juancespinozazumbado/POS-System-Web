using Inventario.Models.Dominio.Productos;

namespace Inventario.BL.Funcionalidades.Inventario.Interfaces
{
    public interface IRepositorioDeInventarios
    {
        public void AgregarInventario(Inventarios inventario);
        public Task<List<Inventarios>> listeElInventarios();

        public void EliminarInventario(Inventarios inventario);

        public void EditarInventario(Inventarios inventario);

        public Inventarios ObetenerInevtarioPorId(int id);

        public IEnumerable<Inventarios> ListarInventariosPorNombre(string nombre);


    }
}
