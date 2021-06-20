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
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("orkhawshi@mitaoe.ac.in", "******************");

                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(email);
                msgObj.From = new MailAddress("orkhawshi@mitaoe.ac.in");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = $"www.fundooapp.com/reset-password/{link}";
                client.Send(msgObj);
            }
        }
    }
}
