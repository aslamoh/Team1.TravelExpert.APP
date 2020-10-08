using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Team1.TravelExpert.Data;

namespace Team1.TravelExpert.App.Models
{
    public class UserLogin
    {
        [Display(Name = "User Login")]
        [Required(ErrorMessage = "Email is Required")]
        public string UserName { get; set; }

        [Display(Name = "User Password")]
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string Id { get; set; }


    }
    







}