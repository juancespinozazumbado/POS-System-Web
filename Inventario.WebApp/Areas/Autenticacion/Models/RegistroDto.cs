using System.ComponentModel.DataAnnotations;

namespace Inventario.WebApp.Areas.Autenticacion.Models
{
    public class RegistroDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Contraseña { get; set; }
        [Required]
        public string ComfirmacionDeContraseña { get; set; }
    }
}
