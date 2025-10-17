using MailKit.Net.Smtp;
using MimeKit;

namespace GuessingGame_BusinessLogic
{
    class EmailService
    {
        public void SendEmail(string userName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Guessing Word Game", "my-example-email@game.com"));
            message.To.Add(new MailboxAddress("Player Username", "user@example.com"));
            message.Subject = "New High Score";
            message.Body = new TextPart("plain")
            {
                Text = $"Account {userName}\n\n" +
                        "You got a new high score!\n\n"
            };

            using (var client  = new SmtpClient())
            {
                var smtpHost = "sandbox.smtp.mailtrap.io";
                var smtpPort = 2525;
                var tls = MailKit.Security.SecureSocketOptions.StartTls;

                client.Connect(smtpHost, smtpPort, tls);

                var username = "00d8ffba7b62bc";
                var password = "a188a040a0a342";

                client.Authenticate(username, password);
                
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
