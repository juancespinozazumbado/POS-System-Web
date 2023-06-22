using Inventario.Models.Dominio.Usuarios;

namespace Inventario.WebApp.Areas.Ventas.Models
{
    public class UsuarioConCajaAbierta
    {
        public AplicationUser Usuario { get; set; }
        public bool TieneUnaCajaAbierta { get; set; } = false;
    }
}
