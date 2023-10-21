using System.ComponentModel.DataAnnotations;

namespace Inventario.WebApp.Areas.Autenticacion.Models
{
    public class LoginDto
    {
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}
