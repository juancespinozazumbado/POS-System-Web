using Inventario.Models.Dominio.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.BL.Funcionalidades.Inventario.Interfaces
{
    public interface IrepositorioDeInventarios
    {
        public void AgregarInventario(Inventarios inventario);
        public IEnumerable<Inventarios> listarInventarios();

        public void EliminarInventario(Inventarios inventario);

        public void EditarInventario(Inventarios inventario);

        public Inventarios ObetenerInevtarioPorId(int id);

        public IEnumerable<Inventarios> ListarInventariosPorNombre(string nombre);


    }
}
