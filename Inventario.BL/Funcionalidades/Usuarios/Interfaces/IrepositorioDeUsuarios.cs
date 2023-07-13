using Inventario.DA.Database;
using Inventario.Models.Dominio.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Inventario.BL.Funcionalidades.Usuarios.Interfaces
{
    public interface IRepositorioDeUsuarios
    {


        public Task<bool> AgregueUnUsuario(AplicationUser user);

        public  Task<bool> AñadirUnAccesoFallido(string id);

        public Task<bool> BloquearUnUsuario(string id);


        public void ElimineUnUsuario(AplicationUser usuario);

        public Task<List<AplicationUser>> ListeLosUsuarios();


        // se necesita servicio STPM
        public void NotificarUnusuarioBloqueado(string id);


        public Task<AplicationUser> ObtengaUnUsuarioPorId(string id);


        public Task<AplicationUser> ObtengaUnUsuarioPorUserName(string username);

        public Task<AplicationUser> ObtengaUnUsuarioPorEmial(string email);




    }
}
