using Inventario.BL.Funcionalidades.Ventas.Interfaces;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Ventas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.BL.Funcionalidades.Ventas
{
    public class RepositorioDeAperturaDeCaja : IrepositorioDeAperturaDeCaja
    {
        private readonly InventarioDBContext context;

        public RepositorioDeAperturaDeCaja(InventarioDBContext context)
        {
            this.context = context;
        }


        public IEnumerable<AperturaDeCaja> AperturasDeCajaPorCliente()
        {
            throw new NotImplementedException();
        }

        public void CerrarUnaAperturaDeCaja(int id)
        {
            throw new NotImplementedException();
        }

        public void CrearUnaAperturaDeCaja(AperturaDeCaja aperturaDeCaja)
        {
           context.AperturasDeCajas.Add(aperturaDeCaja);    
           context.SaveChanges();
        }

        public IEnumerable<AperturaDeCaja> ListarAperturasDeCaja()
        {
            //return context.AperturasDeCajas.Include(a => a.Ventas).ToList();
            throw new NotImplementedException();
        }
    }
}
