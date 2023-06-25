using Inventario.BL.Funcionalidades.Usuarios.Interfaces;
using Inventario.DA.Database;
using Inventario.Models.Dominio.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Inventario.BL.Funcionalidades.Usuarios
{
    public class RepositorioDeUsuarios : IrepositorioDeUsuarios
    {

        private readonly InventarioDBContext dbContext;

        public RepositorioDeUsuarios(InventarioDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AgregueUnUsuario(AplicationUser user)
        {
            dbContext.Usuarios.Add(user);
            dbContext.SaveChanges();

        }

        public void AñadirUnAccesoFallido(string id)
        {
            AplicationUser? usuario = dbContext.Usuarios.
               Where(u => u.Id.Equals(id)).FirstOrDefault();
            usuario.AccessFailedCount += 1;
            dbContext.Update(usuario);
            dbContext.SaveChanges();
        }

        public void BloquearUnUsuario(string id)
        {
            AplicationUser? usuario = dbContext.Usuarios.
                Where(u=> u.Id.Equals(id)).FirstOrDefault();
            usuario.LockoutEnd = DateTime.Now.AddMinutes(10);
            usuario.AccessFailedCount = 0;
            dbContext.Update(usuario);
            dbContext.SaveChanges();
        }

        public void ElimineUnUsuario(AplicationUser usuario)
        {
            dbContext.Usuarios.Remove(usuario);
        }

        public List<AplicationUser> ListeLosUsuarios()
        {
            return dbContext.Usuarios.ToList();
        }

        // se necesita servicio STPM
        public void NotificarUnusuarioBloqueado(string id)
        {
            throw new NotImplementedException();
        }

        public AplicationUser ObtengaUnUsuarioPorId(string id)
        {
            return ListeLosUsuarios().Find(u => u.Id.Equals(id));
        }

        public AplicationUser ObtengaUnUsuarioPorEmail(string email)
        {
            return ListeLosUsuarios().Find(u => u.Email.Equals(email));
        }
    }
}
