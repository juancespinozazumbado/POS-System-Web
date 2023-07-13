namespace Inventario.WebApp.Areas.Autenticacion.Models
{
    public class RespuestaLoginDto
    {
        public UsarioDto Usario { get; set; }   
        public string Token { get; set; }       
    }
}
