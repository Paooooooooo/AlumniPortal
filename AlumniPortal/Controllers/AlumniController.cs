using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Models;
using Newtonsoft.Json;

namespace AlumniPortal.Controllers
{
    public class AlumniController : Controller
    {
        private AlumPortalDbEntities db = new AlumPortalDbEntities();

        // GET: Alumni
        public ActionResult Index()
        {
            List<job_post_tbl> jobPost = db.job_post_tbl.ToList(); 
            return View(jobPost);
        }

        public ActionResult PartnerCompanies() // Display List of Partnered Companies
        {
            List<comp_tbl> companies = db.comp_tbl.ToList(); // Retrieve companies data from database
            return View(companies);
        }

        public ActionResult ManageCV()
        {
            var alumniNum = Session["AlumniId"] as string;

            if (string.IsNullOrEmpty(alumniNum))
            {
                return RedirectToAction("LoginAlumni", "Account");
            }

            var alumni = db.alum_tbl.SingleOrDefault(a => a.alum_num == alumniNum);
            if (alumni == null)
            {
                return HttpNotFound("Alumni not found.");
            }

            var cvs = db.cv_tbl.Where(c => c.alum_num == alumniNum).ToList();

            bool hasCVEntries = cvs != null && cvs.Any();

            var viewModel = new ManageCVViewModel
            {
                AlumniInfo = alumni,
                CVs = cvs,
                HasCVEntries = hasCVEntries
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult AddCV()
        {
            var alumniNum = Session["AlumniId"] as string;

            if (string.IsNullOrEmpty(alumniNum))
            {
                return RedirectToAction("LoginAlumni", "Account");
            }

            var viewModel = new AddCVViewModel
            {
                alum_num = alumniNum,
                Experiences = new List<string>(),
                Skills = new List<string>(),
                EducAttainments = new List<string>()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCV(AddCVViewModel model)
        {
            if (ModelState.IsValid)
            {
                var alumniNum = Session["AlumniId"] as string;

                if (string.IsNullOrEmpty(alumniNum))
                {
                    return RedirectToAction("LoginAlumni", "Account");
                }

                var newCV = new cv_tbl
                {
                    alum_num = alumniNum,
                    experiences = string.Join(",", model.Experiences),
                    skills = string.Join(",", model.Skills),
                    educ_attain = string.Join(",", model.EducAttainments)
                };

                db.cv_tbl.Add(newCV);
                db.SaveChanges();

                return RedirectToAction("ManageCV");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditCV(int id)
        {
            var alumniNum = Session["AlumniId"] as string;

            if (string.IsNullOrEmpty(alumniNum))
            {
                return RedirectToAction("LoginAlumni", "Account");
            }

            var cv = db.cv_tbl.SingleOrDefault(c => c.cv_id == id && c.alum_num == alumniNum);
            if (cv == null)
            {
                return HttpNotFound("CV record not found.");
            }

            var viewModel = new EditCVViewModel
            {
                cv_id = cv.cv_id,
                alum_num = alumniNum,
                Experiences = cv.experiences.Split(',').ToList(),
                Skills = cv.skills.Split(',').ToList(),
                EducAttainments = cv.educ_attain.Split(',').ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCV(EditCVViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var alumniNum = Session["AlumniId"] as string;

                if (string.IsNullOrEmpty(alumniNum))
                {
                    return RedirectToAction("LoginAlumni", "Account");
                }

                var cv = db.cv_tbl.SingleOrDefault(c => c.cv_id == model.cv_id && c.alum_num == alumniNum);

                if (cv == null)
                {
                    return HttpNotFound("CV record not found.");
                }

                // Update experiences
                UpdateList(cv, model.Experiences, "experiences");

                // Update skills
                UpdateList(cv, model.Skills, "skills");

                // Update educational attainments
                UpdateList(cv, model.EducAttainments, "educ_attain");

                db.SaveChanges();

                return RedirectToAction("ManageCV");
            }

            return View(model);
        }

        private void UpdateList(cv_tbl cv, List<string> newItems, string propertyName)
        {
            var existingItems = cv.GetType().GetProperty(propertyName).GetValue(cv, null)?.ToString()?.Split(',') ?? new string[0];

            var updatedItems = new List<string>();

            // Handle updates (delete and replace)
            foreach (var existingItem in existingItems)
            {
                if (newItems.Contains(existingItem.Trim()))
                {
                    updatedItems.Add(existingItem.Trim()); // Add existing item
                }
            }

            // Handle additions
            foreach (var newItem in newItems)
            {
                if (!existingItems.Contains(newItem.Trim()))
                {
                    updatedItems.Add(newItem.Trim()); // Add new item
                }
            }

            cv.GetType().GetProperty(propertyName).SetValue(cv, string.Join(",", updatedItems));
        }

        public ActionResult DeleteCV(int id)
        {
            var cv = db.cv_tbl.Find(id);
            if (cv == null)
            {
                return HttpNotFound("CV entry not found.");
            }

            db.cv_tbl.Remove(cv);
            db.SaveChanges();

            // Redirect back to ManageCV action after deletion
            return RedirectToAction("ManageCV");
        }

        public ActionResult Events() // Display Events
        {
            List<event_tbl> events = db.event_tbl.ToList(); 
            return View(events);
        }

        public ActionResult AlumniProfile()
        {
            var alumniNum = Session["AlumniId"] as string;

            if (string.IsNullOrEmpty(alumniNum))
            {
                return RedirectToAction("LoginAlumni", "Account");
            }

            var alumni = db.alum_tbl.SingleOrDefault(a => a.alum_num == alumniNum);
            if (alumni == null)
            {
                return HttpNotFound("Alumni not found.");
            }

            return View(alumni);
        }

        public ActionResult ChangePassword()
        {
            // Implement change password logic here
            // Placeholder for demonstration
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear(); // Clear the session
            return RedirectToAction("Index", "Home");
        }
    }
}
