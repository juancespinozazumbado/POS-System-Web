
namespace Inventario.BL.ServicioEmail
{
    public interface IServicioDeEmail
    {

        public Task SendEmailAsync(string emisor, string password, string titulo, string cuerpo, string destinatario);   

    }
}
