using Inventario.DA.Database;
using Inventario.Models.Dominio.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.BL.Funcionalidades.Ventas.Interfaces
{
    internal interface IrepositorioDeAperturaDeCaja
    {
        public void CrearUnaAperturaDeCaja(AperturaDeCaja aperturaDeCaja);

        public IEnumerable<AperturaDeCaja> ListarAperturasDeCaja();

        //public IEnumerable<AperturaDeCaja> AperturasDeCajaPorUsuario();


        public void CerrarUnaAperturaDeCaja(int id);

    }
}
