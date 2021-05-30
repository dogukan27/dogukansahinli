using System.Threading.Tasks;
namespace shopapp.webui.EmailService
{
    public interface IEmailSender
    {
         Task SendEmailAsync(string email,string subject,string htmlMessage);
    }
}