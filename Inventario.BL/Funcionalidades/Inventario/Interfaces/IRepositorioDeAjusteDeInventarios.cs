using Inventario.Models.Dominio.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.BL.Funcionalidades.Inventario.Interfaces
{
    public interface IRepositorioDeAjusteDeInventarios
    {

        public Task<List<AjusteDeInventario>> ListarAjustesPorId(int id);

        public Task<bool> AgegarAjusteDeInventario(int id, AjusteDeInventario ajusteDeInventario);

        public Task<AjusteDeInventario> ObtenerAjustePorId(int id);


    }
}
