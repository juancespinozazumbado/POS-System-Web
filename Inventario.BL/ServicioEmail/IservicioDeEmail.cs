
namespace Inventario.BL.ServicioEmail
{
    public interface IservicioDeEmail
    {

        public Task SendEmailAsync(string emisor, string password, string titulo, string cuerpo, string destinatario);   

    }
}
