using AlumniPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AlumniPortal.Controllers
{
    public class CompanyController : Controller
    {
        private AlumPortalDbEntities db = new AlumPortalDbEntities();
        // GET: Company
        public ActionResult Index()
        {
            List<alum_tbl> alumniList = db.alum_tbl.ToList();
            return View(alumniList);
        }

        public ActionResult AlumniCVDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Fetch the alumni record based on alum_num
            alum_tbl alumni = db.alum_tbl.Find(id);

            if (alumni == null)
            {
                return HttpNotFound();
            }

            // Fetch CV records related to this alumni
            var cvRecords = db.cv_tbl.Where(cv => cv.alum_num == id).ToList();

            // Pass both alumni and CV records to the view
            ViewBag.Alumni = alumni;
            ViewBag.CVRecords = cvRecords;

            return View(alumni);
        }

        public ActionResult JobPosting()
        {
            // Retrieve comp_id from session
            var compId = Session["CompanyId"] as int?;

            // Check if comp_id is null or not found
            if (compId == null)
            {
                // Handle the case where comp_id session variable is not set
                // For example, redirect to login or handle the error
                return RedirectToAction("Login", "Account"); // Example redirect to login
            }

            // Retrieve job posts for the logged-in company
            var jobPosts = db.job_post_tbl
                            .Where(j => j.comp_id == compId)
                            .ToList();

            return View(jobPosts);
        }

        // GET: Create Job Post
        public ActionResult CreateJobPost()
        {
            return View();
        }

        // POST: Create Job Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJobPost(job_post_tbl jobPost)
        {
            if (ModelState.IsValid)
            {
                jobPost.comp_id = (int)Session["CompanyId"];
                jobPost.job_post_date = DateTime.Now;
                db.job_post_tbl.Add(jobPost);
                db.SaveChanges();
                return RedirectToAction("JobPosting");
            }
            return View(jobPost);
        }

        // GET: Edit Job Post
        public ActionResult EditJobPost(int id)
        {
            var jobPost = db.job_post_tbl.Find(id);
            if (jobPost == null || jobPost.comp_id != (int)Session["CompanyId"])
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // POST: Edit Job Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobPost(job_post_tbl jobPost)
        {
            if (ModelState.IsValid)
            {
                jobPost.comp_id = (int)Session["CompanyId"];
                db.Entry(jobPost).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("JobPosting");
            }
            return View(jobPost);
        }

        // GET: Delete Job Post
        public ActionResult DeleteJobPost(int id)
        {
            var jobPost = db.job_post_tbl.Find(id);
            if (jobPost == null || jobPost.comp_id != (int)Session["CompanyId"])
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // POST: Delete Job Post
        [HttpPost, ActionName("DeleteJobPost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var jobPost = db.job_post_tbl.Find(id);
            db.job_post_tbl.Remove(jobPost);
            db.SaveChanges();
            return RedirectToAction("JobPosting");
        }
        public ActionResult Events()
        {
            List<event_tbl> events = db.event_tbl.ToList(); 
            return View(events);
        }
        public ActionResult Logout()
        {
            Session.Clear(); // Clear the session
            return RedirectToAction("Index", "Home");
        }
    }
}