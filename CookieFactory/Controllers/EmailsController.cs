using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookieFactory.Data;
using CookieFactory.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace CookieFactory.Controllers
{
    public class EmailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;

        public EmailsController(ApplicationDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            this._hostEnviroment = hostEnviroment;
        }

        // GET: Emails
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Email.ToListAsync());
        }

        // GET: Emails/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // GET: Emails/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("EmailId,UserName,UserEmail,Message,AttatchmentFile")] Email email)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _hostEnviroment.WebRootPath;
            
                string filename = Path.GetFileNameWithoutExtension(email.AttatchmentFile.FileName);
                string extension = Path.GetExtension(email.AttatchmentFile.FileName);
                email.Attatchment = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Attatchments/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await email.AttatchmentFile.CopyToAsync(fileStream);
                }
                
                
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("cookiefactoryenterprise@gmail.com");
                mail.To.Add("cookiefactoryenterprise@gmail.com");
                mail.Subject = email.UserName + " - " + email.UserEmail;
                mail.Body = email.Message;

                mail.Attachments.Add(new Attachment(path));

                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("cookiefactoryenterprise@gmail.com", "cookiefactory666") as ICredentialsByHost;
                smtpServer.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    { return true; };
                smtpServer.Send(mail);
                smtpServer.Dispose();

                _context.Add(email);
                await _context.SaveChangesAsync();
                return RedirectToAction("ControlPanel", "Home");
            }
            return View(email);
        }

        // POST: Emails/Reply
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Reply([Bind("EmailId,UserName,UserEmail,Message,AttatchmentFile")] Email email)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _hostEnviroment.WebRootPath;

                string filename = Path.GetFileNameWithoutExtension(email.AttatchmentFile.FileName);
                string extension = Path.GetExtension(email.AttatchmentFile.FileName);
                email.Attatchment = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Attatchments/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await email.AttatchmentFile.CopyToAsync(fileStream);
                }

                


                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("cookiefactoryenterprise@gmail.com");
                mail.To.Add(email.UserEmail);
                mail.Subject = "Reply to your Request";
                mail.Body = email.Message;

                mail.Attachments.Add(new Attachment(path));

                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("cookiefactoryenterprise@gmail.com", "cookiefactory666") as ICredentialsByHost;
                smtpServer.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    { return true; };
                smtpServer.Send(mail);
                smtpServer.Dispose();

                email.UserName = "Admin";
                email.UserEmail = "cookiefactoryenterprise@gmail.com";

                _context.Add(email);
                await _context.SaveChangesAsync();
                return RedirectToAction("ControlPanel", "Home");
            }
            return View(email);
        }


        // GET: Emails/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            return View(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("EmailId,UserName,UserEmail,Message,Attatchment")] Email email)
        {
            if (id != email.EmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(email);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(email.EmailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(email);
        }

        // GET: Emails/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Email
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            var email = await _context.Email.FindAsync(id);

            var filePath = Path.Combine(_hostEnviroment.WebRootPath, "attatchments", email.Attatchment);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _context.Email.Remove(email);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailExists(int id)
        {
            return _context.Email.Any(e => e.EmailId == id);
        }
    }
}
