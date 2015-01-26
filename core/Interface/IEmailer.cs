using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Core.Interface
{
    public interface IEmailer
    {
        void SendEmail(MailMessage message);
        string FormatBody(ContactForm model);
    }
}
