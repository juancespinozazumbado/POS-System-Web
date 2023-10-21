
using System.Net.Http;
using System.Net;
using System.Net.Mail;


namespace ServicioEmail
{
    public class ServicioDeEmail : IServicioDeEmail
    {
        public Task SendEmailAsync(string emisor,string password, string titulo, string cuerpo, string destinatario)
        {
           
            MailMessage Mensaje = new MailMessage();
            Mensaje.From = new MailAddress(emisor);

            Mensaje.To.Add( new MailAddress(destinatario));

            Mensaje.Subject = titulo;
            Mensaje.Body = cuerpo;

            SmtpClient clienteSMTP = new SmtpClient("smtp-mail.outlook.com", 587);

            clienteSMTP.EnableSsl = true;
            clienteSMTP.UseDefaultCredentials = false;
            clienteSMTP.Credentials = new NetworkCredential(emisor, password);

            try
            {
                clienteSMTP.Send(Mensaje);
                
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error : " + ex.Message);
            }

            return null;


        }
    }
}
