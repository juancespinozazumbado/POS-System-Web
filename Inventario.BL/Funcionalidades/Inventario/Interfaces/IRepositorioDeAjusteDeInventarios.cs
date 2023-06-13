using Inventario.Models.Dominio.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.BL.Funcionalidades.Inventario.Interfaces
{
    public interface RepositorioDeInventarios
    {

        public IEnumerable<AjusteDeInventario> ListarAjustesPorId(int id);

        public void AgegarAjusteDeInventario(int id, AjusteDeInventario ajusteDeInventario);

        public AjusteDeInventario ObtenerAjustePorId(int id);


    }
}
