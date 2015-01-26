using Core.Interface;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Emailer : IEmailer
    {
        private string _username;
        private string _password;
        private string _smtpHost;

        public Emailer(string username, string password, string smtpHost)
        {
            this._username = username;
            this._password = password;
            this._smtpHost = smtpHost;
        }
        public void SendEmail(MailMessage message)
        {
            NetworkCredential credentials = new NetworkCredential();
            credentials.UserName = _username;
            credentials.Password = _password;

            var smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credentials;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.Host = _smtpHost;

            smtpClient.Send(message);
        }
        
        public string FormatBody(ContactForm model)
        {
            var sb = new StringBuilder();

            sb.Append("From: ");            sb.AppendLine(model.Email);
            sb.Append("Firstname: ");       sb.AppendLine(model.FirstName);
            sb.Append("Lastname: ");        sb.AppendLine(model.LastName);
            sb.Append("Phone number: ");    sb.AppendLine(model.PhoneNumber);
            sb.Append("Subject: ");         sb.AppendLine(model.Subject);
            sb.Append("Message: ");         sb.AppendLine(model.Message);

            return sb.ToString();
        }
    }
}
