namespace server.Config;

public record EmailSettings(
    string SmtpServer,    // SMTP server adress (t.ex, smtp.gmail.com)
    int SmtpPort,        // SMTP port nummer (vanligtvis 587 for TLS)
    string FromEmail,     // Din email som ska skicka mailet
    string Password      // App-specifikt lösenord (google security -> app password)
);