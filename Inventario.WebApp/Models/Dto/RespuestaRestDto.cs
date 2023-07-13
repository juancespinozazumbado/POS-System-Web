namespace Inventario.WebApp.Models.Dto
{
    public class RespuestaRestDto 
    {
        public bool esSucces { get; set; } = true;
        public Object Respuesta { get; set; }    
        public string Mensaje { get; set; }    

    }
}
