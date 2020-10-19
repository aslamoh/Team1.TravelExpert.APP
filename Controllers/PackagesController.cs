using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team1.TravelExpert.Data;
using Team1.TravelExpert.App.Models;
using System.Web.Mvc;
using System.IO;

namespace Team1.TravelExpert.App.Controllers
{
    public class PackagesController : Controller
    {
        private TravelExpertsEntities db = new TravelExpertsEntities();

        // GET: Packages
        public ActionResult Index()
        {
            return View(db.Packages.ToList());
        }

        // GET: Packages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PackageId,PkgName,PkgStartDate,PkgEndDate,PkgDesc,PkgBasePrice,PkgAgencyCommission,PkgImageFile")] Package package)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Packages.Add(package);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(package);
        //}

        [HttpPost]
        public ActionResult Create (Package p)
        {
            if(ModelState.IsValid == true)
            {

                string fileName = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
                string extension = Path.GetExtension(p.ImageFile.FileName);
                HttpPostedFileBase postedFile = p.ImageFile;
                int length = postedFile.ContentLength;
                if (extension.ToLower() == ".jpg"|| extension.ToLower() == ".png" || extension.ToLower() == ".jpeg" )
                {
                    if (length <= 1000000)
                    {
                        fileName =  fileName + extension;
                        p.PkgImageFile = "~/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Images/"),fileName);
                        p.ImageFile.SaveAs(fileName);

                        db.Packages.Add(p);
                        int row= db.SaveChanges();
                        if (row > 0)
                        {
                            TempData["InsertMessage"] = "<script>alert('Package Inserted Successfully')</script>";
                            ModelState.Clear();
                            return RedirectToAction("Index", "Packages");
                        }
                        else
                        {
                            TempData["InsertMessage"] = "<script>alert('Package not Inserted')</script>";

                        }
                    }
                    else
                    {
                        TempData["SizeMessage"] = "<script>alert('Image Size should be less then 1 MB')</script>";
                    }

                }
                else
                {
                    TempData["ExtesnionMessage"]="<script>alert('Format not Supported')</script>";
                }

            }
            return View();
        }
        public ActionResult Edit(int Id)
        {
            var PackageRow = db.Packages.Where(p => p.PackageId == Id).FirstOrDefault();
            Session["Image"] = PackageRow.PkgImageFile;
            return View(PackageRow);
        }
        [HttpPost]
        public ActionResult Edit(Package p)
        {
           if (ModelState.IsValid == true)
            {
                if(p.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
                    string extension = Path.GetExtension(p.ImageFile.FileName);
                    HttpPostedFileBase postedFile = p.ImageFile;
                    int length = postedFile.ContentLength;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                    {
                        if (length <= 1000000)
                        {
                            fileName = fileName + extension;
                            p.PkgImageFile = "~/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            p.ImageFile.SaveAs(fileName);

                            db.Entry(p).State = EntityState.Modified;
                            int row = db.SaveChanges();
                            if (row > 0)
                            {
                                TempData["UpdateMessage"] = "<script>alert('Package Updated Successfully')</script>";
                                ModelState.Clear();
                                return RedirectToAction("Index", "Packages");
                            }
                            else
                            {
                                TempData["UpdateMessage"] = "<script>alert('Package not Updated')</script>";

                            }
                        }
                        else
                        {
                            TempData["SizeMessage"] = "<script>alert('Image Size should be less then 1 MB')</script>";
                        }

                    }
                    else
                    {
                        TempData["ExtesnionMessage"] = "<script>alert('Format not Supported')</script>";
                    }



                }
                else
                {
                    p.PkgImageFile = Session["Image"].ToString();
                    db.Entry(p).State = EntityState.Modified;
                    int row = db.SaveChanges();
                    if (row > 0)
                    {
                        TempData["UpdateMessage"] = "<script>alert('Package Updated Successfully')</script>";
                        ModelState.Clear();
                        return RedirectToAction("Index", "Packages");
                    }
                    else
                    {
                        TempData["UpdateMessage"] = "<script>alert('Package not Updated')</script>";

                    }




                }
            }
            return View();
        }



    }
    


}