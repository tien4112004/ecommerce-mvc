using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DotNetEnv;

namespace EcommerceMVC.Areas.Admin.Services;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public EmailService()
    {
        _smtpServer = Env.GetString("SMTP_SERVER");
        _smtpPort = Env.GetInt("SMTP_PORT");
        _smtpUser = Env.GetString("SMTP_USER");
        _smtpPass = Env.GetString("SMTP_PASS");
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using (var client = new SmtpClient(_smtpServer, _smtpPort))
        {
            client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new InvalidOperationException("Could not send email", ex);
            }
        }
    }
}