using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Sender;
public class EmailSender
{
    private readonly IConfiguration _configuration;
    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void SendEmail(string toEmail, string subject, string body)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");

        using (var client = new SmtpClient(emailSettings["Host"], int.Parse(emailSettings["Port"])))
        {
            client.Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"]);
            client.EnableSsl = bool.Parse(emailSettings["EnableSSL"]);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings["Username"], emailSettings["SenderName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);
            client.Send(mailMessage);
        }
    }
}