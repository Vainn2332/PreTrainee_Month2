using Castle.Core.Smtp;
using PreTrainee_Month2.ApplicationLayer.ServiceInterfaces;
using PreTrainee_Month2.CoreLayer.Entities.Static_Entities;
using System.Net;
using System.Net.Mail;

namespace PreTrainee_Month2.ApplicationLayer.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }


        public async Task SendEmailAsync(string receiverEmail,string subject,string body)
        {
            _logger.LogInformation($"отправка почты адресату {receiverEmail}");
            var mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.From=new MailAddress("testAuthentication@example.com","проверятор Пароля");
            mail.To.Add(receiverEmail);
            mail.Subject = subject;//было "Проверка пароля";
            mail.Body = body;
            using SmtpClient client = new SmtpClient(EmailServiceConfiguration.SMTP_CLIENT);
            
                client.Port = EmailServiceConfiguration.PORT;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(EmailServiceConfiguration.EMAIL_SENDER, EmailServiceConfiguration.APP_PASSWORD);
                await client.SendMailAsync(mail);
            _logger.LogInformation($"Почта отправлена!");
        }
    }
}
