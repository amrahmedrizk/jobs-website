using jobwebsitetwo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
  
    public class HomeController : Controller
    {
      private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult details(int id)
        {
            var jobs = db.jobs.Find(id);
            Session["jobid"] = id;
            return View(jobs);
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult contact(contactmodel cont)
        { 
            var mail = new MailMessage();
            var logininfo = new NetworkCredential("cutecenacena@gmail.com","1qazXSW@@");
            mail.From = new MailAddress(cont.email);
            mail.To.Add(new MailAddress("cutecena@hotmail.com"));
            mail.Subject = cont.subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل:  " + cont.name + "<br>" +
                "بريد المرسل:" + cont.email + "<br>" +
                "عنوان الرسالة: " + cont.subject + "<br>" +
                "نص الرسالة: <b>" + cont.message + "</b>";
            mail.Body = body;
            var smtpclient = new SmtpClient("smtp.gmail.com", 587);
            smtpclient.EnableSsl = true;
            smtpclient.Credentials = logininfo;
            smtpclient.Send(mail);
            return RedirectToAction("index");

        }
        public ActionResult apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult apply(applyforjobs jobs)
        {
            if (ModelState.IsValid)
            {
                var h = User.Identity.GetUserId();
                var hhh= (int)Session["jobid"];
                var hh = db.applyforjobs.Where(a => a.userid == h && a.jobsid== hhh).ToList();
                if (hh.Count < 1)
                {
                    jobs.userid = h;
                    jobs.applaydate = DateTime.Now;
                    jobs.jobsid = hhh;
                    db.applyforjobs.Add(jobs);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(jobs);
        }
        public ActionResult getjobsbyapplaier()
        {
            var user = User.Identity.GetUserId();
            var jobs = db.applyforjobs.Where(a => a.userid == user).ToList();
            return View(jobs);
        }
        public ActionResult edit(int id)
        {
            var apllyforjob = db.applyforjobs.Find(id);
            return View(apllyforjob);
        }
        [HttpPost]
        public ActionResult edit(applyforjobs jobs)
        {
            var g = db.applyforjobs.Find(jobs.id);
            if (ModelState.IsValid)
            {                
                g.massage = jobs.massage;
                g.applaydate = DateTime.Now;
                db.Entry(g).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("getjobsbyapplaier");

            }
            return View(g);
        }
        public ActionResult detailsof(int id)
        {
            var t=db.applyforjobs.Find(id);
            return View(t);
        }
        public ActionResult delete(int id)
        {
            var j = db.applyforjobs.Find(id);
            return View(j);
        }
        [HttpPost]
        public ActionResult delete(applyforjobs jobs)
        {
            var h = db.applyforjobs.Find(jobs.id);
            db.applyforjobs.Remove(h);
            db.SaveChanges();
            return RedirectToAction("getjobsbyapplaier");
        }
        public ActionResult getjobsbypublisher()
        {
            var user = User.Identity.GetUserId();
            var jobs = from app in db.applyforjobs
                       join j in db.jobs
                       on app.jobsid equals j.id
                       where j.user.Id == user
                       select app;
            var grouped = from h in jobs
                          group h by h.jobs.jobtitle
                        into g
                          select new jobsbypublish
                          {
                              hh = g.Key,
                              items = g
                          };
            return View(grouped.ToList());
        }
        public ActionResult search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult search(string searchname)
        {
            var result = db.jobs.Where(a => a.jobtitle.Contains(searchname) || a.jobcontent.Contains(searchname) || a.category.categoryname.Contains(searchname)).ToList();
            return View(result);
        }
    }
}