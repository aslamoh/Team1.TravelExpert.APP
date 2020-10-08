using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team1.TravelExpert.App.Models;
using Team1.TravelExpert.Data;
using System.Collections;
using System.Data.Entity.Migrations.Model;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Team1.TravelExpert.App.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Create()
        {
            // create views/Customer/create.cshtml  using  Models/Customrer.cs

            return View();
        }
        //Customer / Create  :POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            var db = new TravelExpertsEntities();
            //get existing customer user name 
            var custUsername = GetCustomerInfo(customer.UserName);

            // if  new customer user name already exist
            if (custUsername != null)
            {
                ViewBag.Error = "User ID Already Exist";
                return View();
            }
            else
            {
                if (customer.CustEmail == null)
                {
                    customer.CustEmail = "";
                }
                if (customer.CustBusPhone == null)
                {
                    customer.CustBusPhone = "";
                }

                customer.Password = Password.Encrypted(customer.Password);   // encryption for user password 
                db.Customers.Add(customer);
                db.SaveChanges();

                //return View();
                //return RedirectToAction("Registration", "Home");
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult CustomerProfile()
        {

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login","Account");
            }
            else
            {
                int id = Convert.ToInt32(Session["UserID"]);

                var currentCust = GetCurrentCustomerInfo(id);

                return View(currentCust);
            }
        }
        public ActionResult EditProfile( )
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                int id = Convert.ToInt32(Session["UserID"]);

                var currentCust = GetCurrentCustomerInfo(id);

                return View(currentCust);
            }
        }

        [HttpPost]
        public ActionResult Save(Customer customerToUpdate)
        {
            var db = new TravelExpertsEntities();
            var currentCustomer = db.Customers.Where(x => x.CustomerId == customerToUpdate.CustomerId).FirstOrDefault();

            if (customerToUpdate.CustEmail == null)
            {
                customerToUpdate.CustEmail = "";
            }
            if (customerToUpdate.CustBusPhone == null)
            {
                customerToUpdate.CustBusPhone = "";
            }

            currentCustomer.CustFirstName = customerToUpdate.CustFirstName;
            currentCustomer.CustLastName = customerToUpdate.CustLastName;
            currentCustomer.CustAddress = customerToUpdate.CustAddress;
            currentCustomer.CustCity = customerToUpdate.CustCity;
            currentCustomer.CustProv = customerToUpdate.CustProv;
            currentCustomer.CustPostal = customerToUpdate.CustPostal;
            currentCustomer.CustCountry = customerToUpdate.CustCountry;
            currentCustomer.CustHomePhone = customerToUpdate.CustHomePhone;
            currentCustomer.CustBusPhone = customerToUpdate.CustBusPhone;
            currentCustomer.CustEmail = customerToUpdate.CustEmail;

          

            db.SaveChanges();
            return RedirectToAction("CustomerProfile", "Customer");
        }

        // methods 
        public static Customer GetCustomerInfo(string userName)
            {
            var db = new TravelExpertsEntities();

            var cust = db.Customers.SingleOrDefault(c => c.UserName == userName);


            return cust;

            }
        public static Customer  GetCurrentCustomerInfo(int id)
        {
            var db = new TravelExpertsEntities();

            var currentCustomer = db.Customers.SingleOrDefault(c => c.CustomerId == id);
            
            return currentCustomer;

        }

    }
}