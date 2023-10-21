using System.ComponentModel.DataAnnotations;

namespace Inventario.SI.Modelos.Dtos.Usuarios
{
    public class CambioDeContraseñaRequestDto
    {

        public string NombreUsario { get; set; }

        public string Contraseña { get; set; }

        public string NuevaContraseña { get; set; }
      
        public string ConfirmarContraseña { get; set; }

    }
}
