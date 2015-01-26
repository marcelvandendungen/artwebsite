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

namespace IlseLeijten.Controllers
{
    public class ContactController : Controller
    {
        private IEmailer _emailer;

        public ContactController(IEmailer emailer)
        {
            _emailer = emailer;
        }

        // GET: /Contact/
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        // POST: /Contact/
        [HttpPost]
        public ActionResult Send(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                MailMessage message = new MailMessage();    // TODO: hide behind interface?
                message.From = new MailAddress(model.Email);
                message.To.Add(new MailAddress("marcel.vandendungen@gmail.com"));

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
