using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team1.TravelExpert.App.Models;
using Team1.TravelExpert.Data;

namespace Team1.TravelExpert.App.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()

        {
            UserLogin logininfo = new UserLogin();  //  UserLogin from models


            return View(logininfo);
        }
        [HttpPost]
        public ActionResult Login(UserLogin login)
        {
            var db = new TravelExpertsEntities();
            var userpassword = Password.Encrypted(login.Password);
                     
            //var custData = db.Customers.ToList();

            var user = db.Customers.Where(c => c.UserName == login.UserName && c.Password == userpassword).FirstOrDefault();

            if(user!=null)
            {
                Session["UserID"] = user.CustomerId;
                Session["CustFirstName"] = (user.CustFirstName) + " " + (user.CustLastName);

                 return RedirectToAction("Index", "Home");
                //ViewBag.invalid = "valid User";
                //return View();
            }
            else

            ViewBag.invalid = "Invalid User";
            return View();
            
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session.Abandon();
            //return RedirectToAction("Registration", "Home");
            return View();
        }

    }
}   