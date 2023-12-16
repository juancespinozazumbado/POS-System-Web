

using Inventario.BL.Funcionalidades.Inventario.Interfaces;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Productos;
using Microsoft.EntityFrameworkCore;

namespace Inventario.BL.Funcionalidades.Inventario
{
    public class ReporitorioDeInventarios : IRepositorioDeInventarios
    {
        private readonly InventarioDBContext _dbContext;
        public ReporitorioDeInventarios(InventarioDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AgregarInventario(Inventarios inventario)
        {
            await _dbContext.Inventarios.AddAsync(inventario);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditarInventario(Inventarios inventario)
        {
             _dbContext.Inventarios.Update(inventario);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarInventario(Inventarios inventario)
        {
             _dbContext.Inventarios.Remove(inventario);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<Inventarios>> listeElInventarios()
        {
            List<Inventarios> Lista = await _dbContext.Inventarios.Include(a => a.Ajustes).ToListAsync();
            return Lista;
        }

        public async Task<Inventarios> ObetenerInevtarioPorId(int id)
        {
            var Inventarios = await _dbContext.Inventarios.Include(a => a.Ajustes).ToListAsync();
            return Inventarios.Find(i => i.Id == id);
        }

        public async Task<List<Inventarios>> ListarInventariosPorNombre(string nombre)
        {
            return await _dbContext.Inventarios.Where(i => i.Nombre.Contains(nombre)).ToListAsync();
        }
    }
}
