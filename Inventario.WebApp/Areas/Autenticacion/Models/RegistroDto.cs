using System.ComponentModel.DataAnnotations;

namespace Inventario.WebApp.Areas.Autenticacion.Models
{
    public class RegistroDto
    {
        [Required]
        public string NombreUsario { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Contraseña { get; set; }
        [Required]
        public string ConfirmarContraseña { get; set; }
    }
}
