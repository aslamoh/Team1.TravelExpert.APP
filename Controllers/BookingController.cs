using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team1.TravelExpert.App.Models;
using Team1.TravelExpert.Data;

namespace Team1.TravelExpert.App.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult BookingData()
        {
           

            var db = new TravelExpertsEntities();
            int Id = Convert.ToInt32(Session["UserID"]);
            var bookingDetails = (from b in db.Bookings
                                  join bd in db.BookingDetails on b.BookingId equals bd.BookingId
                                  join p in db.Packages on b.PackageId equals p.PackageId
                                  where b.CustomerId == Id
                                  select new BookingViewModel
                                  {
                                      bookings = b,
                                      bookingDeatils = bd,
                                      packages = p,
                                      

                                  }).ToList();

                    

            return View(bookingDetails);
         
        }
        public ActionResult ProductData()
        {

            var db = new TravelExpertsEntities();
            int Id = Convert.ToInt32(Session["UserID"]);
            var productDetails = (from c in db.Customers
                                  join b in db.Bookings on c.CustomerId equals b.CustomerId
                                  join bd in db.BookingDetails on b.BookingId equals bd.BookingId
                                  join ps in db.Products_Suppliers on bd.ProductSupplierId equals ps.ProductSupplierId
                                  join pd in db.Products on ps.ProductId equals pd.ProductId
                                  where b.CustomerId == Id
                                  select new BookingViewModel
                                  {
                                      
                                      bookings = b,
                                      bookingDeatils = bd,
                                      customers =c,
                                      products = pd,
                                      
                                  }).ToList();

            return View(productDetails);

        }







    }
}