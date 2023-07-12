namespace Inventario.SI.Modelos.Dtos.Usuarios
{
    public class CambioDeContraseñaRequestDto
    {
        
        public string username {  get; set; }       
        public string Contraseña { get; set; }

        public string ConfirmarContraseña { get; set; }

        public string NuevaContraseña { get; set; }

    }
}
