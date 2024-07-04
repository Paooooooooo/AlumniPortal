using AlumniPortal.Models;
using System.Web.Mvc;
using System.Linq;
using System;

namespace AlumniPortal.Controllers
{
    public class AccountController : Controller
    {
        private AlumPortalDbEntities _db = new AlumPortalDbEntities();
        // GET: Account/SelectUserType
        public ActionResult SelectUserType()
        {
            return View();
        }

        // POST: Account/SelectUserType
        [HttpPost]
        public ActionResult SelectUserType(UserTypeModel model)
        {
            switch (model.UserType)
            {
                case "Alumni":
                    return RedirectToAction("AlumniLogin");
                case "Company":
                    return RedirectToAction("CompanyLogin");
                case "Admin":
                    return RedirectToAction("AdminLogin");
                default:
                    return RedirectToAction("SelectUserType");
            }
        }

        // GET: Account/AlumniLogin
        public ActionResult AlumniLogin()
        {
            return View();
        }

        // GET: Account/CompanyLogin
        public ActionResult CompanyLogin()
        {
            return View();
        }

        // GET: Account/AdminLogin
        public ActionResult AdminLogin()
        {
            return View();
        }

        // POST: Account/LoginAlumni
        [HttpPost]
        public ActionResult LoginAlumni(AlumniLoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Replace with your authentication logic
                var alumni = _db.alum_tbl.SingleOrDefault(a => a.alum_num == model.alum_num && a.alum_password == model.alum_password);
                if (alumni != null)
                {
                    string sessionId = Guid.NewGuid().ToString();

                    // Store the session ID in the session
                    Session["SessionId"] = sessionId;
                    Session["AlumniId"] = alumni.alum_num; // Store alumni ID as well for further use

                    // Authentication successful
                    // Set authentication cookie or session as needed
                    return RedirectToAction("Index", "Alumni");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid alumni number or password.");
                }
            }

            return View("AlumniLogin",model);
        }

        // POST: Account/LoginCompany
        [HttpPost]
        public ActionResult LoginCompany(CompanyLoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Replace with your authentication logic
                var company = _db.comp_tbl.SingleOrDefault(a => a.comp_email == model.comp_email && a.comp_password == model.comp_password);
                if (company != null)
                {
                    string sessionId = Guid.NewGuid().ToString();

                    // Store the session ID in the session
                    Session["SessionId"] = sessionId;
                    Session["CompanyId"] = company.comp_id; 

                    // Authentication successful
                    // Set authentication cookie or session as needed
                    return RedirectToAction("Index", "Company");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid company email or password.");
                }
            }

            return View("CompanyLogin", model);
        }

        // POST: Account/LoginAdmin
        [HttpPost]
        public ActionResult LoginAdmin(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Replace with your authentication logic
                var company = _db.admin_tbl.SingleOrDefault(a => a.admin_id == model.admin_id && a.admin_password == model.admin_password);
                if (company != null)
                {
                    string sessionId = Guid.NewGuid().ToString();

                    // Store the session ID in the session
                    Session["SessionId"] = sessionId;
                    Session["AdminId"] = company.admin_id; // Store alumni ID as well for further use

                    // Authentication successful
                    // Set authentication cookie or session as needed
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid admin ID or password.");
                }
            }

            return View("AdminLogin", model);
        }
    }
}
