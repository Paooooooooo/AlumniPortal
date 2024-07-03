using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;
using System.Web.Mvc;
using AlumniPortal.Models;
using Newtonsoft.Json;
using static AlumniPortal.Models.JobPostIndexModel;

namespace AlumniPortal.Controllers
{
    public class AlumniController : Controller
    {
        private AlumPortalDbEntities db = new AlumPortalDbEntities();

        // GET: Alumni
        public ActionResult Index()
        {
            var jobPosts = (from job in db.job_post_tbl
                            join comp in db.comp_tbl on job.comp_id equals comp.comp_id
                            select new JobPostIndexModel
                            {
                                JobId = job.job_id,
                                JobTitle = job.job_title,
                                JobLocation = job.job_loc,
                                CompanyName = comp.comp_name
                            }).ToList();

            return View(jobPosts);
        }

        public ActionResult JobDetails(int id)
        {
            var jobPost = (from job in db.job_post_tbl
                           join comp in db.comp_tbl on job.comp_id equals comp.comp_id
                           where job.job_id == id
                           select new JobPostDetailsModel
                           {
                               JobTitle = job.job_title,
                               CompanyName = comp.comp_name,
                               JobSpecifications = job.job_specs,
                               JobSalary = job.job_salary,
                               JobType = job.job_type,
                               JobTarget = job.job_target,
                               CompanyContactName = comp.comp_con_name,
                               CompanyContactNumber = comp.comp_con_num,
                               CompanyEmail = comp.comp_email
                           }).FirstOrDefault();

            if (jobPost == null)
            {
                return HttpNotFound();
            }

            return View(jobPost);
        }

        public ActionResult PartnerCompanies() // Display List of Partnered Companies
        {
            List<comp_tbl> companies = db.comp_tbl.ToList(); // Retrieve companies data from database
            return View(companies);
        }

        public ActionResult CompanyJobPosts(int compId)
        {
            // Retrieve job posts for the selected company from the database
            var jobPosts = db.job_post_tbl.Where(j => j.comp_id == compId).ToList();
            return View(jobPosts);
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
                Experiences = cv.experiences,
                Skills = cv.skills,
                EducAttainments = cv.educ_attain
            };

            return View(viewModel);
        }

        // POST: EditCV
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCV(EditCVViewModel model)
        {
            var alumniNum = Session["AlumniId"] as string;

            if (string.IsNullOrEmpty(alumniNum))
            {
                return RedirectToAction("LoginAlumni", "Account");
            }

            if (ModelState.IsValid)
            {
                var cv = db.cv_tbl.SingleOrDefault(c => c.cv_id == model.cv_id && c.alum_num == alumniNum);
                if (cv == null)
                {
                    return HttpNotFound("CV record not found.");
                }

                // Update values
                cv.experiences = model.Experiences;
                cv.skills = model.Skills;
                cv.educ_attain = model.EducAttainments;

                db.SaveChanges();
                return RedirectToAction("ManageCV");
            }

            return View(model);
        }



        [HttpGet]
        public ActionResult AppendCV(int id)
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

            var viewModel = new AppendCVViewModel
            {
                cv_id = cv.cv_id,
                alum_num = alumniNum,
                // Populate with existing data if needed
                Experiences = new List<string> { cv.experiences },
                Skills = new List<string> { cv.skills },
                EducAttainments = new List<string> { cv.educ_attain }
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppendCV(AppendCVViewModel model)
        {
            var alumniNum = Session["AlumniId"] as string;

            if (string.IsNullOrEmpty(alumniNum))
            {
                return RedirectToAction("LoginAlumni", "Account");
            }

            if (ModelState.IsValid)
            {
                // Assign alum_num
                model.alum_num = alumniNum;

                // Example of saving multiple entries
                foreach (var experience in model.Experiences)
                {
                    var newCV = new cv_tbl
                    {
                        experiences = experience,
                        skills = string.Join(", ", model.Skills),
                        educ_attain = string.Join(", ", model.EducAttainments),
                        alum_num = model.alum_num
                    };

                    db.cv_tbl.Add(newCV);
                }

                db.SaveChanges();

                return RedirectToAction("ManageCV");
            }

            return View(model);
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

        [HttpGet]
        public ActionResult Events() // Display Events
        {
            List<event_tbl> events = db.event_tbl.ToList(); 
            return View(events);
        }

        public ActionResult AlumniProfile(string id)
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

            var viewModel = new AlumniProfileViewModel
            {
                AlumNum = alumni.alum_num,
                DeptId = alumni.dept_id,
                AlumName = alumni.alum_name,
                AlumSex = alumni.alum_sex,
                AlumBdate = alumni.alum_bdate,
                YrGrad = alumni.yr_grad,
                AlumEmail = alumni.alum_email,
                AlumCon = alumni.alum_con
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlumniProfile(AlumniProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var alumni = db.alum_tbl.SingleOrDefault(a => a.alum_num == viewModel.AlumNum);
                if (alumni == null)
                {
                    return HttpNotFound("Alumni not found.");
                }

                alumni.dept_id = viewModel.DeptId;
                alumni.alum_name = viewModel.AlumName;
                alumni.alum_sex = viewModel.AlumSex;
                alumni.alum_bdate = viewModel.AlumBdate;
                alumni.yr_grad = viewModel.YrGrad;
                alumni.alum_email = viewModel.AlumEmail;
                alumni.alum_con = viewModel.AlumCon;

                db.Entry(alumni).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("AlumniProfile", new { id = alumni.alum_num });
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var alumniNum = Session["AlumniId"] as string;
            if (string.IsNullOrEmpty(alumniNum))
            {
                return RedirectToAction("LoginAlumni", "Account");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

            if (alumni.alum_password != model.CurrentPassword)
            {
                TempData["ErrorMessage"] = "Current password is incorrect.";
                return View(model);
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("", "New password and confirm password do not match.");
                return View(model);
            }

            alumni.alum_password = model.NewPassword;
            db.Entry(alumni).State = System.Data.Entity.EntityState.Modified;
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
