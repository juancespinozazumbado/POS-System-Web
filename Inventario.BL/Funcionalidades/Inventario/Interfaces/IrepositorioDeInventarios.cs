using Inventario.Models.Dominio.Productos;

namespace Inventario.BL.Funcionalidades.Inventario.Interfaces
{
    public interface IrepositorioDeInventarios
    {
        public void AgregarInventario(Inventarios inventario);
        public IEnumerable<Inventarios> listeElInventarios();

        public void EliminarInventario(Inventarios inventario);

        public void EditarInventario(Inventarios inventario);

        public Inventarios ObetenerInevtarioPorId(int id);

        public IEnumerable<Inventarios> ListarInventariosPorNombre(string nombre);


    }
}
