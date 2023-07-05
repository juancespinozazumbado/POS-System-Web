using Microsoft.AspNetCore.Identity;

namespace Inventario.SI.Modelos
{
    public class RespuestaDeAutenticacion<T> where T : class 
    {
        public string Mensaje { get; set; } 

        public T EntidadDto { get; set; }  
        
        public List<IdentityError> Error { get; set; }

    }
    
}
