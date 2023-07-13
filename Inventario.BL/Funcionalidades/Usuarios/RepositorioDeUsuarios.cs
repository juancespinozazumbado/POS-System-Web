using Inventario.BL.Funcionalidades.Usuarios.Interfaces;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Inventario.BL.Funcionalidades.Usuarios
{
    public class RepositorioDeUsuarios : IRepositorioDeUsuarios
    {

        private readonly InventarioDBContext dbContext;

        public RepositorioDeUsuarios(InventarioDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AgregueUnUsuario(AplicationUser user)
        {
            await dbContext.Usuarios.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return true;    

        }

        public async Task<bool> AñadirUnAccesoFallido(string id)
        {
            AplicationUser? usuario = dbContext.Usuarios.
               Where(u => u.Id.Equals(id)).FirstOrDefault();
              usuario.AccessFailedCount += 1;
            dbContext.Update(usuario);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BloquearUnUsuario(string id)
        {
            AplicationUser? usuario = dbContext.Usuarios.
                Where(u=> u.Id.Equals(id)).FirstOrDefault();
            usuario.LockoutEnd = DateTime.Now.AddMinutes(10);
            usuario.AccessFailedCount = 0;
            dbContext.Update(usuario);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public void ElimineUnUsuario(AplicationUser usuario)
        {
            dbContext.Usuarios.Remove(usuario);
        }

        public async Task<List<AplicationUser>> ListeLosUsuarios()
        {
            return await dbContext.Usuarios.ToListAsync();
        }

        // se necesita servicio STPM
        public void NotificarUnusuarioBloqueado(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<AplicationUser> ObtengaUnUsuarioPorId(string id)
        {
             var usuarios = await  ListeLosUsuarios();
            return  usuarios.FirstOrDefault(u => u.Id.Equals(id));
        }

        public async Task<AplicationUser> ObtengaUnUsuarioPorUserName(string username)
        {
            var usuarios = await ListeLosUsuarios();

            return usuarios.FirstOrDefault(u => u.UserName.Equals(username));
        }

        public async Task<AplicationUser> ObtengaUnUsuarioPorEmial(string email)
        {
            var usuarios = await ListeLosUsuarios();

            return usuarios.FirstOrDefault(u => u.Email.Equals(email));
        }
    }
}
