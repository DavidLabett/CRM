using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using server.Config;

namespace server.Services;

public interface IEmailService
{
  Task SendEmailAsync(string to, string subject, string body);
}

public class EmailService : IEmailService
{
  private readonly EmailSettings _settings;

  public EmailService(EmailSettings settings) => _settings = settings;

  public async Task SendEmailAsync(string to, string subject, string body)
  {
    // Skapa ett ny email med hjälp av MimeMessage
    var email = new MimeMessage
    {
      From = { MailboxAddress.Parse(_settings.FromEmail) },  // Sätt avsändare
      To = { MailboxAddress.Parse(to) },                     // Sätt mottagare
      Subject = subject,
      Body = new TextPart("html") { Text = body }           // Sätt HTML som body - Eller "plain"
    };

    // Skapa och konfigurera SMTP klienten
    using var smtp = new SmtpClient();
    // Connect - SecureSocketOptions för TLS (Transport Layer Security
    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.StartTls);
    // Autentisera med SMTP servern
    await smtp.AuthenticateAsync(_settings.FromEmail, _settings.Password);
    // Skicka mailet
    await smtp.SendAsync(email);
    // Disconnecta / Koppla från
    await smtp.DisconnectAsync(true);
  }
}