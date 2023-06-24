
using Microsoft.AspNetCore.Identity;

namespace Inventario.Models.Dominio.Productos
{
    public class AjusteDeInventario
    {
        public int Id { get; set; }
     
        public int CantidadActual { get; set; }
        public int Ajuste { get; set; }

        public TipoAjuste Tipo { get; set; }

  
        public DateTime Fecha { get; set; }

        public string? Observaciones { get; set; }

        public string UserId { get; set; } 
      
        public int Id_Inventario { get; set; }  
        public Inventarios? Inventarios { get; set; } 

    
    }
}