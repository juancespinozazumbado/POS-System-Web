using Inventario.Models.Dominio.Usuarios;

namespace Inventario.BL.Funcionalidades.Usuarios.Interfaces
{
    public interface IrepositorioDeUsuarios
    {

        public void AgregueUnUsuario(AplicationUser user);

        public List<AplicationUser> ListeLosUsuarios();   
        
        public AplicationUser ObtengaUnUsuarioPorId(string id);     
        
        public void ElimineUnUsuario(AplicationUser usuario);


        public void AñadirUnAccesoFallido(string id);

        public void BloquearUnUsuario(string id);

        // necesita el servicio de STMP
        public void NotificarUnusuarioBloqueado(string id);


    }
}
