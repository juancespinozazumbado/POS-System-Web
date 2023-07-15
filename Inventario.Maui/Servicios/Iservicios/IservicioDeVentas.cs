using Inventario.Models.Dominio.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Maui.Servicios.Iservicios
{
    public interface IservicioDeVentas
    {
        public Task<List<AperturaDeCaja>> AperturasDeCajaPorUsuario(string idUsuario);
    }
}
