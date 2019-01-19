using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using jobwebsitetwo.Models;
using System.IO;
using static System.Net.WebRequestMethods;
using Microsoft.AspNet.Identity;

namespace jobwebsitetwo.Controllers
{
    public class jobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: jobs
        public ActionResult Index()
        {
            var jobs = db.jobs.Include(j => j.category);
            return View(jobs.ToList());
        }

        // GET: jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // GET: jobs/Create
        public ActionResult Create()
        {
            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryname");
            return View();
        }

        // POST: jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(jobs jobs,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = Path.Combine(Server.MapPath("~/uploads"), upload.FileName);
                    upload.SaveAs(path);
                   
                }
                jobs.userid = User.Identity.GetUserId();
                jobs.jobimage = upload.FileName;
                db.jobs.Add(jobs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryname", jobs.categoryid);
            return View(jobs);
        }

        // GET: jobs/Edit/5
        public ActionResult Edit(int id)
        {
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryname", jobs.categoryid);
            ViewBag.myimage = jobs.jobimage;
            return View(jobs);
        }

        // POST: jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(jobs job, HttpPostedFileBase upload)
        {
            jobs n = db.jobs.Find(job.id);
            if (ModelState.IsValid)
            {

                if (upload != null)
                { 
                    string oldpath = Path.Combine(Server.MapPath("~/uploads"), n.jobimage.ToString());
                    string path = Path.Combine(Server.MapPath("~/uploads"), upload.FileName);
                    upload.SaveAs(path);
                    n.jobimage = upload.FileName;
                    System.IO.File.Delete(oldpath);
                }          
                n.jobtitle = job.jobtitle;
                n.jobcontent = job.jobcontent;
                n.categoryid = job.categoryid;
                db.Entry(n).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryid = new SelectList(db.categories, "id", "categoryname", job.categoryid);
            return View(n);
        }

        // GET: jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // POST: jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jobs jobs = db.jobs.Find(id);
            db.jobs.Remove(jobs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
