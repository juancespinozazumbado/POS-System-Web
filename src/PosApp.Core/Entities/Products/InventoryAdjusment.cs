
namespace Inventario.Models.Dominio.Productos
{
    public class InventoryAdjustment
    {
        public int Id { get; set; }
     
        public int CantidadActual { get; set; }
        public int Ajuste { get; set; }

        public TipoAjuste Tipo { get; set; }

        public DateTime Fecha { get; set; }

        public string? Observaciones { get; set; }

        public Guid UserId { get; set; } 
      
        public Guid Id_Inventario { get; set; }  
       // public Inventarios? Inventarios { get; set; } 

    
    }
}