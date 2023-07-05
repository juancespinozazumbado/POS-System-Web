

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
        public void AgregarInventario(Inventarios inventario)
        {
            _dbContext.Inventarios.Add(inventario);
            _dbContext.SaveChanges();
        }

        public void EditarInventario(Inventarios inventario)
        {
            _dbContext.Inventarios.Update(inventario);
            _dbContext.SaveChanges();
        }

        public void EliminarInventario(Inventarios inventario)
        {
            _dbContext.Inventarios.Remove(inventario);
            _dbContext.SaveChanges();
        }

        public async Task<List<Inventarios>> listeElInventarios()
        {
            List<Inventarios> Lista = await _dbContext.Inventarios.Include(a => a.Ajustes).ToListAsync();
            return Lista;
        }

        public Inventarios ObetenerInevtarioPorId(int id)
        {
            return _dbContext.Inventarios.Include(a => a.Ajustes).ToList().Find(i => i.Id == id);
        }

        public IEnumerable<Inventarios> ListarInventariosPorNombre(string nombre)
        {
            return _dbContext.Inventarios.Where(i => i.Nombre.Contains(nombre)).ToList();
        }
    }
}
