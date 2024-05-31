using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        UserDetails userDetails = new UserDetails();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            // ViewData["Email"] = userDetails.userRecord();
            //ViewData["Email"] = "siyabonga@gmail.com";


            //if (userDetails.uidRecord()!>0)
            //{
            //    return Content("non");
            //}
            //else
            //return Content(userDetails.uidRecord().ToString());


           // ViewData["display"] = userDetails.MonetaryDataDisplay();
            ViewData["TotalMonetaryDonation"] = "Total monetary donations received: R " + userDetails.GetTotalMonetaryDonations();

            return View(userDetails.MonetaryDataDisplay());
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Goods()
        {
            ViewBag.CityList = userDetails.getAllCategory();
            return View();
        }





        public IActionResult Disaster()
        {
            return View();
        }

        public IActionResult Category()
        {
            return View();
        }

        public IActionResult Donate()
        {
            return View();
        }


        public IActionResult GoodsDonationDataDisplay()
        {
            ViewData["TotalNumberOfGoodsDonation"] = "Total number of goods received: " + userDetails.GetTotalNumberOfGoodsDonation();
            return View(userDetails.GoodsDonationDataDisplay());
        }

        public IActionResult DisplayDisasterData()
        {
            return View(userDetails.DisasterDataDisplay());
        }

        //display data methodes
        public IActionResult DisasterDisplay2()
        {
            return View(userDetails.DisasterDataDisplay());
        }

        //for good donation 
        [HttpPost]
        public IActionResult GoodsDonationValidation()
        {
            int numberOfItems;
            string itemDesciption;
            DateTime donationDate;
            int category;
            int DonerID = 1;
            try
            {
                numberOfItems = int.Parse(Request.Form["NumberOfItems"]);
                itemDesciption = Request.Form["descriptionOfItems"].ToString();
                donationDate = DateTime.Parse(Request.Form["GoodsDonaionDate"].ToString());
                category = int.Parse(Request.Form["CategoryID"]);
                // test = int.Parse(Request.Form["CategoryID"]);


                // return Content(category.ToString());

                bool insertCategory = userDetails.AddGoodsDonation(numberOfItems, donationDate, category, itemDesciption, DonerID);

                if (insertCategory)
                {
                    ViewData["MonetaryDonation"] = "1";

                    // return Content(test.ToString());

                    return RedirectToAction("GoodsDonationDataDisplay", $"Home");
                }
                else
                {
                    
                    return RedirectToAction("Goods", $"Home");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Goods", $"Home");
            }
           
        }


        /// <summary>
        /// / need attention
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult MonetaryDonationValidation()
        {
            int Amount;
            DateTime DonationDate;
            Amount = int.Parse(Request.Form["Amount"]);
            DonationDate = DateTime.Parse(Request.Form["DonaionDate"].ToString());


            // bool insertCategory = userDetails.AddCategory(categoryName);

            bool insertCategory = userDetails.AddMonetaryDonation(Amount, DonationDate);

            if (insertCategory)
            {
                ViewData["MonetaryDonation"] = "1";
                // return RedirectToAction("Index", $"Home");
                return RedirectToAction("Index", $"Home");
            }
            else
            {

                return View("Donate");
            }

        }
        [HttpPost]
        public IActionResult CategoryValidation()
        {
            string categoryName = null;


            categoryName = Request.Form["CategoryName"].ToString();

            bool insertCategory = userDetails.AddCategory(categoryName);

            if (insertCategory)
            {
                return RedirectToAction("Goods", $"Home");
                //return View("Goods");
            }
            else
            {
                return View("Category");
            }

        }


        [HttpPost]
        public IActionResult DisasterValidation()
        {
            string location, disasterDescription, RequiredAid;
            DateTime startDate, endDate;

            try
            {

                location = Request.Form["Location"].ToString();
                disasterDescription = Request.Form["descriptionOfDisaster"].ToString();
                RequiredAid = Request.Form["SpecifyRequiresAid"].ToString();

                startDate = DateTime.Parse(Request.Form["startDate"].ToString());
                endDate = DateTime.Parse(Request.Form["EndDate"].ToString());


                if (startDate < endDate)
                {
                    string userID = HttpContext.Session.GetInt32("UserID").ToString();
                    bool insert = userDetails.captureDisasterInfomation(location, disasterDescription, RequiredAid, startDate, endDate, userID);
                    // return Content(insert.ToString());
                    //if the sql failsto insert 
                    // return Content(insert.ToString());
                    if (insert)
                    {

                        return RedirectToAction("DisplayDisasterData", $"Home");
                    }
                    else
                    {
                       
                        return RedirectToAction("Disaster", $"Home");
                    }

                }
                else
                {
                    return RedirectToAction("Disaster", $"Home");

                }
            }
            catch (Exception)
            {
                return RedirectToAction("Disaster", $"Home");

            }


        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //part 3 POE
        public IActionResult Statitics()
        {
            double total = userDetails.GetTotalMonetaryDonations() - (userDetails.GetTotalAmountOfPurchasedGoods() + userDetails.GetTotalAmountOfAllocatedMonetary());
            ViewData["AvailableAmount"] = "Total Available Amount: R " + total;
            ViewData["TotalMonetaryDonation"] = "Total monetary donations received: R " + userDetails.GetTotalMonetaryDonations();
            ViewData["TotalNumberOfGoodsDonation"] = "Total number of goods received: " + userDetails.GetTotalNumberOfGoodsDonation();
            // ViewData["TotalNumberOfGoodsDonation"] = "Total number of goods received: " + userDetails.GetTotalNumberOfGoodsDonation();
            return View(userDetails.AllocatedGoodsDataDisplay());
        }
        //ActiveDisasterDataDisplay
        public IActionResult ActiveDisasters()
        {
            double total = userDetails.GetTotalMonetaryDonations() - (userDetails.GetTotalAmountOfPurchasedGoods() + userDetails.GetTotalAmountOfAllocatedMonetary());
            ViewData["AvailableAmount"] = "Total Available Amount: R " + total;
            ViewData["TotalMonetaryDonation"] = "Total monetary donations received: R " + userDetails.GetTotalMonetaryDonations();
            ViewData["TotalNumberOfGoodsDonation"] = "Total number of goods received: " + userDetails.GetTotalNumberOfGoodsDonation();
            // ViewData["TotalNumberOfGoodsDonation"] = "Total number of goods received: " + userDetails.GetTotalNumberOfGoodsDonation();
            ViewBag.ActiveDisasters = userDetails.ActiveDisasterDataDisplay();


            return View();
        }
    }
}
