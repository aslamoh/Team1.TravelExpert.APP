using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Team1.TravelExpert.App.Controllers
{
    public class Password
    {     // encryption for user password 
        public static string Encrypted(string value)
        {
            return Convert.ToBase64String(
                 System.Security.Cryptography.SHA256.Create()
                 .ComputeHash(Encoding.UTF8.GetBytes(value))
                 );

        }
    }
}