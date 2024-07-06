using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Models;

namespace AlumniPortal.Controllers
{
    public class AdminController : Controller
    {
        private AlumPortalDbEntities db = new AlumPortalDbEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Events()
        {
            var events = db.event_tbl.ToList();
            return View(events);
        }

        public ActionResult CreateEvent()
        {
            return View();
        }

        // POST: Admin/CreateEvent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(event_tbl eventModel)
        {
            if (ModelState.IsValid)
            {
                eventModel.event_post_date = DateTime.Now;
                eventModel.admin_id = (string)Session["AdminId"]; // Get admin_id from session
                db.event_tbl.Add(eventModel);
                db.SaveChanges();
                return RedirectToAction("Events");
            }
            return View(eventModel);
        }

        // GET: Admin/EditEvent/5
        public ActionResult EditEvent(int id)
        {
            var eventModel = db.event_tbl.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            return View(eventModel);
        }

        // POST: Admin/EditEvent/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent(event_tbl eventModel)
        {
            if (ModelState.IsValid)
            {
                eventModel.admin_id = (string)Session["AdminId"]; // Get admin_id from session
                db.Entry(eventModel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Events");
            }
            return View(eventModel);
        }

        // GET: Admin/DeleteEvent/5
        public ActionResult DeleteEvent(int id)
        {
            var eventModel = db.event_tbl.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            return View(eventModel);
        }

        // POST: Admin/DeleteEvent/5
        [HttpPost, ActionName("DeleteEvent")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var eventModel = db.event_tbl.Find(id);
            if (eventModel != null)
            {
                db.event_tbl.Remove(eventModel);
                db.SaveChanges();
            }
            return RedirectToAction("Events");
        }

        // GET: Admin/DetailsEvent/5
        public ActionResult DetailsEvent(int id)
        {
            var eventModel = db.event_tbl.Find(id);
            if (eventModel == null)
            {
                return HttpNotFound();
            }
            return View(eventModel);
        }

        public ActionResult ManageAlumni()
        {
            var alumni = db.alum_tbl.ToList();
            return View(alumni);
        }

        public ActionResult CreateAlumni()
        {
            return View();
        }

        // POST: Admin/CreateAlumni
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAlumni(alum_tbl alumni)
        {
            if (ModelState.IsValid)
            {
                db.alum_tbl.Add(alumni);
                db.SaveChanges();
                return RedirectToAction("ManageAlumni");
            }
            return View(alumni);
        }

        // GET: Admin/EditAlumni/5
        public ActionResult EditAlumni(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alum_tbl alumni = db.alum_tbl.Find(id);
            if (alumni == null)
            {
                return HttpNotFound();
            }
            return View(alumni);
        }

        // POST: Admin/EditAlumni/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAlumni(alum_tbl alumni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageAlumni");
            }
            return View(alumni);
        }

        // GET: Admin/DeleteAlumni/5
        public ActionResult DeleteAlumni(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alum_tbl alumni = db.alum_tbl.Find(id);
            if (alumni == null)
            {
                return HttpNotFound();
            }
            return View(alumni);
        }

        // POST: Admin/DeleteAlumni/5
        [HttpPost, ActionName("DeleteAlumni")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            alum_tbl alumni = db.alum_tbl.Find(id);
            db.alum_tbl.Remove(alumni);
            db.SaveChanges();
            return RedirectToAction("ManageAlumni");
        }

        // GET: Admin/DetailsAlumni/5
        public ActionResult DetailsAlumni(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alum_tbl alumni = db.alum_tbl.Find(id);
            if (alumni == null)
            {
                return HttpNotFound();
            }
            return View(alumni);
        }

        public ActionResult ManageCompanies()
        {
            var companies = db.comp_tbl.ToList();
            return View(companies);
        }

        public ActionResult CompanyDetails(int id)
        {
            var company = db.comp_tbl.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
        }

        // GET: AddCompany
        public ActionResult AddCompany()
        {
            return View();
        }

        // POST: AddCompany
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCompany(comp_tbl company)
        {
            if (ModelState.IsValid)
            {
                db.comp_tbl.Add(company);
                db.SaveChanges();
                return RedirectToAction("ManageCompanies");
            }

            return View(company);
        }

        // GET: EditCompany
        public ActionResult EditCompany(int id)
        {
            var company = db.comp_tbl.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
        }

        // POST: EditCompany
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompany(comp_tbl company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageCompanies");
            }

            return View(company);
        }

        // GET: DeleteCompany
        public ActionResult DeleteCompany(int id)
        {
            var company = db.comp_tbl.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
        }

        // POST: DeleteCompany
        [HttpPost, ActionName("DeleteCompany")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCompanyConfirmed(int id)
        {
            var company = db.comp_tbl.Find(id);
            db.comp_tbl.Remove(company);
            db.SaveChanges();
            return RedirectToAction("ManageCompanies");
        }

        // GET: EditJobPost
        public ActionResult EditJobPost(int id)
        {
            var jobPost = db.job_post_tbl.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }

            return View(jobPost);
        }

        // POST: EditJobPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobPost(job_post_tbl jobPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobPost).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CompanyDetails", new { id = jobPost.comp_id });
            }

            return View(jobPost);
        }

        // GET: DeleteJobPost
        public ActionResult DeleteJobPost(int id)
        {
            var jobPost = db.job_post_tbl.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }

            return View(jobPost);
        }

        // POST: DeleteJobPost
        [HttpPost, ActionName("DeleteJobPost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteJobPostConfirmed(int id)
        {
            var jobPost = db.job_post_tbl.Find(id);
            var companyId = jobPost.comp_id;
            db.job_post_tbl.Remove(jobPost);
            db.SaveChanges();
            return RedirectToAction("CompanyDetails", new { id = companyId });
        }

        // GET: Admin/EditProfile
        public ActionResult EditProfile(string id)
        {
            var adminId = Session["AdminId"] as string;

            if (string.IsNullOrEmpty(adminId))
            {
                return RedirectToAction("LoginAdmin", "Account");
            }

            var admin = db.admin_tbl.SingleOrDefault(a => a.admin_id == adminId);
            if (admin == null)
            {
                return HttpNotFound("Admin not found.");
            }

            var viewModel = new AdminProfileView
            {
                AdminId = admin.admin_id,
                AdminName = admin.admin_name,
                AdminEmail = admin.admin_email,
                AdminCon = admin.admin_con,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(AdminProfileView viewModel)
        {
            if (ModelState.IsValid)
            {
                var admin = db.admin_tbl.SingleOrDefault(a => a.admin_id == viewModel.AdminId);
                if (admin == null)
                {
                    return HttpNotFound("Admin not found.");
                }

                admin.admin_id = viewModel.AdminId;
                admin.admin_name = viewModel.AdminName;
                admin.admin_email = viewModel.AdminEmail;
                admin.admin_con = viewModel.AdminCon;

                db.Entry(admin).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                // Update session AdminId if it was changed
                Session["AdminId"] = admin.admin_id;

                TempData["ProfileUpdated"] = "Profile updated successfully!";
                return RedirectToAction("EditProfile", new { id = admin.admin_id });
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var adminId = Session["AdminId"] as string;
            if (string.IsNullOrEmpty(adminId))
            {
                return RedirectToAction("LoginAdmin", "Account");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(AdminChangePasswordView model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var adminId = Session["AdminId"] as string;
            if (string.IsNullOrEmpty(adminId))
            {
                return RedirectToAction("LoginAdmin", "Account");
            }

            var admin = db.admin_tbl.SingleOrDefault(a => a.admin_id == adminId);
            if (admin == null)
            {
                return HttpNotFound("Admin not found.");
            }

            if (admin.admin_password != model.CurrentPassword)
            {
                TempData["ErrorMessage"] = "Current password is incorrect.";
                return View(model);
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("", "New password and confirm password do not match.");
                return View(model);
            }

            admin.admin_password = model.NewPassword;
            db.Entry(admin).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Password changed successfully.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear(); // Clear the session
            return RedirectToAction("Index", "Home");
        }
    }
}