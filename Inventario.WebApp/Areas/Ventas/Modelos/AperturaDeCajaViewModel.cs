using Inventario.Models.Dominio.Usuarios;
using Inventario.Models.Dominio.Ventas;

namespace Inventario.WebApp.Areas.Ventas.Modelos
{
    public class AperturaDeCajaViewModel
    {
        public AplicationUser Usuario { get; set; }
        public bool TieneUnaCajaAbierta { get; set; } = false;
        public List<AperturaDeCaja> Cajas { get; set; }

        public AperturaDeCaja Caja { get; set; }

        public Dictionary<string, List<Venta>> Totales { get; set; }


    }
}
