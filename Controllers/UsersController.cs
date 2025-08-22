using SignUpCode.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignUpCode.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult PassengerOperationPage()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }


        private registrationDB db = new registrationDB();

        public ActionResult PassengerSignUp()
        {
            return View();
        }
        [HttpPost]


        public ActionResult PassengerSignUp(User obj)
        {
            try
            {
                if (db.Users.Any(x => x.Email == obj.Email))
                {
                    ViewBag.Notification = "An account with the same Email(Username) already exists";
                    return View();
                }
                else if (ModelState.IsValid)
                {
                    db.Users.Add(obj);
                    db.SaveChanges();

                    // Set session variables
                    Session["Email"] = obj.Email;
                    Session["Passenger_Name"] = obj.Passenger_Name;

                    // Redirect to SignIn Page
                    return RedirectToAction("PassengerSignIn");
                }

            }
            catch (DbEntityValidationException ex)
            {
                // Log or inspect the validation errors
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // You can log these errors or display them to the user
                ViewBag.Notifica = string.Join(", ", "Enter all fields");
                return View();
            }
            return View();

        }


        //CUSTOMER SIGN IN PAGE

        [HttpGet]
        public ActionResult PassengerSignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PassengerSignIn(User logain)
        {
           

            using (registrationDB db = new registrationDB())
            {
                var Password = db.Users.Where(x => x.Password.Equals(logain.Password)).FirstOrDefault();
                var Username = db.Users.Where(x => x.Email.Equals(logain.Email)).FirstOrDefault();
                var both = db.Users.Where(x => x.Email.Equals(logain.Email) && x.Password.Equals(logain.Password)).FirstOrDefault();

                if (both != null)
                {
                    Session["int"] = logain.Email.ToString();
                    Session["Email"] = logain.Email.ToString();
                    return RedirectToAction("PassengerOperationPage");
                }
                else if (Password == null)
                {
                    ViewBag.NotifyPass = "Wrong Password";
                }
                else if (Username == null)
                {
                    ViewBag.NotifyUserN = "Wrong Username";
                }
                else
                {
                    ViewBag.Notify = "n";

                }
            }
            return View();

        }


    }
}