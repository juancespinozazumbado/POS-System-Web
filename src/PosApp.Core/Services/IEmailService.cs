
namespace Application.Services;

  public interface IEmailService
    {

        public Task SendEmailAsync(
            string emisor, string password, string Title, string body, string destinatario);   

    }

