using BugTracker.Models;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net;

namespace BugTracker.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto emailDto);
    }

    public class EmailService : IEmailService
    {
        public void SendEmail(EmailDto emailDto)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sender Name", emailDto.Username));
            message.To.Add(new MailboxAddress("Recipient Name", emailDto.ToEmail));
            message.Subject = emailDto.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = emailDto.Body;

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(emailDto.Host, emailDto.Port, SecureSocketOptions.Auto);
                client.Authenticate(new NetworkCredential(emailDto.Username, emailDto.Password));
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
