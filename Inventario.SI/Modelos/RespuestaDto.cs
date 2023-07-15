using Microsoft.AspNetCore.Identity;

namespace Inventario.SI.Modelos
{
    public class RespuestaDto 
    {
        public string Mensaje { get; set; } 

        public Object Respuesta { get; set; }

        public bool esSucces { get; set; } = true; 
        
        public List<IdentityError> Error { get; set; }     

    }
    
}
