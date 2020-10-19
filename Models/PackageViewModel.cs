using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team1.TravelExpert.App.Models
{
    public class PackageViewModel
    {
        public int PackageId { get; set; }
        [Required]
        [Display(Name = "PackageName")]
        public string PkgName { get; set; }
        [Required]
        [DataType(DataType.Date), DisplayName("PackageStartDate")]
        public Nullable<System.DateTime> PkgStartDate { get; set; }
        [Required]
        [DataType(DataType.Date), DisplayName("PackagesEndDate")]
        public Nullable<System.DateTime> PkgEndDate { get; set; }
        [Required]
        [Display(Name = "Package Description*")]
       
        public string PkgDesc { get; set; }
        [Required]
        [Display(Name ="PackageBasePrice")]
        public decimal PkgBasePrice { get; set; }
        [Required]
        [Display(Name ="PackageAgencyCommission")]
        public Nullable<decimal> PkgAgencyCommission { get; set; }
        public string PkgImageFile { get; set; }







    }
}