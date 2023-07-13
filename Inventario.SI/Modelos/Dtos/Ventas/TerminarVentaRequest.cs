using Inventario.Models.Dominio.Ventas;

namespace Inventario.SI.Modelos.Dtos.Ventas
{
    public class TerminarVentaRequest
    {

        public int Id_venta { get; set; }
        public TipoDePago TipoDePago { get; set; }
      
    }
}
