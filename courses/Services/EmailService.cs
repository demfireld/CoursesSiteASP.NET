using System;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace courses.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("caginewtest@mail.ru"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Connect("smtp.mail.ru", 465, true);
                    smtp.Authenticate("caginewtest@mail.ru", "WjD5reigPnCcbW8etuam");
                    smtp.Send(emailToSend);
                    smtp.Disconnect(true);
                    Console.WriteLine("Ok");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
