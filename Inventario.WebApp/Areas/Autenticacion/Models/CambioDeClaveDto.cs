using System.ComponentModel.DataAnnotations;

namespace Inventario.WebApp.Areas.Autenticacion.Models
{
    public class CambioDeClaveDto
    {
       
        public string NombreUsario { get; set; }

        [Required]
        
        public string Contraseña { get; set; }

        [Required]
        public string NuevaContraseña { get; set; }
        [Required]
        public string ConfirmarContraseña { get; set; }
    }
}
