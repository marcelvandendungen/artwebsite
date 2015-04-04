using Core.Model;
using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NetMail = System.Net.Mail;
using System.Net.Mail;
using System.Configuration;

namespace IlseLeijten.Controllers
{
    public class ContactController : Controller
    {
        private string _toAddress;
        private IEmailer _emailer;

        public ContactController(IEmailer emailer)
        {
            _emailer = emailer;
            _toAddress = ConfigurationManager.AppSettings["EmailAddress"];
        }

        // GET: /Contact/
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        // POST: /Contact/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(model.Email);
                message.To.Add(new MailAddress(_toAddress));

                message.Subject = "Contact form website: " + model.Subject;
                message.Body = _emailer.FormatBody(model);

                _emailer.SendEmail(message);

                return RedirectToAction("Index", "Home", new { alert = "success" });
            }
            else
            {
                return View();
            }
        }
    }
}
