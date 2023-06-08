
namespace Inventario.Models.Dominio.Ventas
{
    public class AperturaDeCaja
    {
        public int Id { get; set; } 
        public DateTime FechaDeInicio { get; set; }

        public DateTime FechaDeCierre { get; set; }

        public string Observaciones { get; set; }   

        public EstadoVenta  estado { get; set; }

        public int UserId { get; set; } 
    
        public List<Venta> Ventas { get; set; } 


    }
}
