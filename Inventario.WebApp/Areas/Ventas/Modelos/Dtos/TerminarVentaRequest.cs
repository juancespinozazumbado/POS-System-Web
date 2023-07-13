using Inventario.Models.Dominio.Ventas;

namespace Inventario.WebApp.Areas.Ventas.Modelos.Dtos
{
    public class TerminarVentaRequest
    {

        public int Id_venta { get; set; }
        public TipoDePago TipoDePago { get; set; }
      
    }
}
