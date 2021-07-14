using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendEmail(string email, string link)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("orkhawshi@mitaoe.ac.in", "9921670012");

                MailMessage msg = new MailMessage();
                msg.To.Add(email);
                msg.From = new MailAddress("orkhawshi@mitaoe.ac.in");
                msg.Subject = "Password Reset Link Fundoo Application";
                msg.Body = $"www.fundooapplication.com/reset-password/{link}";
                client.Send(msg);
            }
        }
    }
}
