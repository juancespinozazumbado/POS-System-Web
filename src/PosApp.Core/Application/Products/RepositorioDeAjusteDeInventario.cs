using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Microsoft.EntityFrameworkCore;

namespace Inventario.BL.Funcionalidades.Inventario
{
    public class RepositorioDeAjusteDeInventario : IRepositorioDeAjusteDeInventarios
    {
        private readonly InventarioDBContext _dbContext;
        public RepositorioDeAjusteDeInventario(InventarioDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> AgegarAjusteDeInventario(int id, AjusteDeInventario ajuste)
        {
            var inventarios = await _dbContext.Inventarios.Include(a => a.Ajustes).ToListAsync();
            Inventarios inventario = inventarios.Find(i => i.Id == id);

            if (inventario != null)
            {
                if (ajuste.Tipo == TipoAjuste.Aumento)
                {
                    inventario.Cantidad += ajuste.Ajuste;
                    inventario.Ajustes.Add(ajuste);
                    _dbContext.Inventarios.Update(inventario);
                    await _dbContext.SaveChangesAsync();

                }
                if (inventario.Cantidad >= ajuste.Ajuste && ajuste.Tipo == TipoAjuste.Disminucion)
                {

                    inventario.Cantidad += -(ajuste.Ajuste);
                    inventario.Ajustes.Add(ajuste);
                    _dbContext.Inventarios.Update(inventario);
                    await _dbContext.SaveChangesAsync();

                }
                return true;
            }
            else return false;
            
        }

        public async Task<List<AjusteDeInventario>> ListarAjustesPorId(int id)
        {
            return _dbContext.Inventarios.ToList().Find(i => i.Id == id).Ajustes;  
        }

        public async Task<AjusteDeInventario> ObtenerAjustePorId(int id)
        {
            var ajustes = await _dbContext.AjusteDeInventarios.ToListAsync();
            return ajustes.Find(Aj => Aj.Id == id);  
        }
    }
}