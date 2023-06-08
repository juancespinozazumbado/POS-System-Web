﻿using Inventario.DA.Database;
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


        public void AgegarAjusteDeInventario(int id, AjusteDeInventario ajuste)
        {
            Inventarios inventario = _dbContext.Inventarios.Include(a=> a.Ajustes).ToList().Find(i => i.Id == id);

            if (ajuste.Tipo == TipoAjuste.Aumento)
            {
                inventario.Cantidad += ajuste.Ajuste;
            }

            if (ajuste.Tipo == TipoAjuste.Disminucion)
            {
                if(inventario.Cantidad >= ajuste.Ajuste)
                inventario.Cantidad += -(ajuste.Ajuste);
            }
            inventario.Ajustes.Add(ajuste);
            _dbContext.Inventarios.Update(inventario);  
            _dbContext.SaveChanges();   
        }

        public IEnumerable<AjusteDeInventario> ListarAjustesPorId(int id)
        {
            return _dbContext.Inventarios.ToList().Find(i => i.Id == id).Ajustes;  
        }

        public AjusteDeInventario ObtenerAjustePorId(int id)
        {
            return _dbContext.AjusteDeInventarios.ToList().Find(Aj => Aj.Id == id);  
        }

        private void AjusteDeInventario()
        {

        }
    }
}