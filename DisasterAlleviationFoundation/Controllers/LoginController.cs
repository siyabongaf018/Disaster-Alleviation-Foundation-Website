using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Controllers
{
    public class LoginController : Controller
    {

        UserDetails userDetails = new UserDetails();
        public IActionResult Login()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult PublicPage()
        {
            double total = userDetails.GetTotalMonetaryDonations() - (userDetails.GetTotalAmountOfPurchasedGoods() + userDetails.GetTotalAmountOfAllocatedMonetary());
            ViewData["AvailableAmount"] = "Total Available Amount: R " + total;
            ViewData["TotalMonetaryDonation"] = "Total monetary donations received: R " + userDetails.GetTotalMonetaryDonations();
            ViewData["TotalNumberOfGoodsDonation"] = "Total number of goods received: " + userDetails.GetTotalNumberOfGoodsDonation();
            // ViewData["TotalNumberOfGoodsDonation"] = "Total number of goods received: " + userDetails.GetTotalNumberOfGoodsDonation();
            ViewBag.ActiveDisasters = userDetails.ActiveDisasterDataDisplay();

            return View();
        }

        [HttpPost]
        public IActionResult ValidateLogin()
        {
            string Email = null, Password = null;

            // variable that taks the vlue from he form. Request.Form["inputname"].ToString();
            Email = Request.Form["Email"].ToString();
            Password = Request.Form["Password"].ToString();
            bool isValid = userDetails.ValidateLoginCredentials(Email,Password);


            if (isValid)
            { // if he user exst the user will be directed to the index page
                ViewData["Eror"] = "";

                HttpContext.Session.SetInt32("UserID", userDetails.GetUserID(Email, Password));
                if (userDetails.GetUserType(Email, Password) == 1)
                {

                    return RedirectToAction("MonetaryAllocation", $"AdminAllocation");
                }
                return RedirectToAction("Index", $"Home");
            }
            else
            {
                ViewBag.Message = "Invalid Credentials";

                ViewData["Eror"] = "Invalid Credentials";
                return RedirectToAction("Login", "Login");  // this return the user to the log i page
            }
           // return View();
        }
        
        [HttpPost]
        public IActionResult ValidateRegistration()
        {
            
            string Email = null, Password = null, confirmPasword = null, name = null, surname= null;

            string invalidCredential ="invalid credentials";
            // variable that taks the vlue from he form. Request.Form["inputname"].ToString();
            
            name = Request.Form["Name"].ToString();
            surname = Request.Form["Surname"].ToString();
            Email = Request.Form["Email"].ToString();
            Password = Request.Form["Password"].ToString();
            confirmPasword = Request.Form["ConfirmPassword"].ToString();
           

           
            if (Password.Equals(confirmPasword) && Password.Length >3)
            { 

                bool insertUserDetails = userDetails.UserRegister(Email, Password, confirmPasword, name, surname);
                //return Content(insertUserDetails.ToString());
                if (insertUserDetails==true)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    return RedirectToAction("Login", "Register");  // this return the user to the log i page
                }
            }
            else
            {
                return  View("Register");  // this return the user to the log i page
            }
            // return View();
        }





    }
}
