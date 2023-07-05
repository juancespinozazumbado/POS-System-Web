namespace Inventario.SI.Modelos.Dtos.Usuarios
{
    public class RegistroRequestDto
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public string Contraseña { get; set; }

        public string ComfirmacionDeContraseña { get; set; }

    }
}
