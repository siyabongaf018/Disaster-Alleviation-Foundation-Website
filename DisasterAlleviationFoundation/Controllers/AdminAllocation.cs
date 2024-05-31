using DisasterAlleviationFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Controllers
{
    public class AdminAllocation : Controller
    {
        UserDetails userDetails = new UserDetails();
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ViewAllocatedMonetaryData()
        {
            double total = userDetails.GetTotalAmountOfAllocatedMonetary();
            ViewData["AllocatedAmount"] = "Total Allocated Amount: R " + total;

            return View(userDetails.AllocatedMonetaryDataDisplay());
        }

        public IActionResult ViewAllocatedGoodsData()
        {

            return View(userDetails.AllocatedGoodsDataDisplay());
        }


        public IActionResult ViewPurchasedGoods()
        {
            double total = userDetails.GetTotalAmountOfPurchasedGoods();
            ViewData["PurchasedGoodsAmount"] = "Total Purchased Goods Amount: R " + total;
            
            return View(userDetails.PurchasedGoodsDisplay());
        }
        [HttpGet]
        public IActionResult CapturePurchasedGoods()
        {
            double total = userDetails.GetTotalMonetaryDonations() - (userDetails.GetTotalAmountOfPurchasedGoods() + userDetails.GetTotalAmountOfAllocatedMonetary());
            ViewBag.AvailableAmount = "Total Available Amount: R " + total;
            ViewBag.AmountLimit = total;
            ViewBag.CityList = userDetails.getAllCategory();
            return View();
        }

        [HttpGet]
        public IActionResult MonetaryAllocation()
        {
            double total = userDetails.GetTotalMonetaryDonations() - (userDetails.GetTotalAmountOfPurchasedGoods() + userDetails.GetTotalAmountOfAllocatedMonetary());
            ViewData["AvailableAmount"] = "Total Available Amount: R " + total;
            ViewData["TotalMonetaryDonation"] = "Total monetary donations received: R " + userDetails.GetTotalMonetaryDonations();

            ViewBag.ActiveDisaster = userDetails.getAllActiveDisaster();
            return View();
        }

        [HttpGet]
        public IActionResult GoodsAllocation()
        {
            ViewBag.ActiveDisaster = userDetails.getAllActiveDisaster();
            ViewBag.Goods = userDetails.getAllGoods();
            ViewBag.GoodsQuantity = userDetails.getAllGoodsQuantity();
            return View();
        }

        [HttpPost]
        public IActionResult MonetaryAllocationValidation()
        {

            int activeDisasterID;
            double Amount;
            DateTime AllocationDate;
            try
            {
                activeDisasterID = int.Parse(Request.Form["ActiveDisasterID"].ToString());
                Amount = double.Parse(Request.Form["Amount"]);
                AllocationDate = DateTime.Parse(Request.Form["AllocationDate"].ToString());

                bool insertCategory = userDetails.captureMonetaryAllocationInfomation(activeDisasterID, Amount, AllocationDate);

                if (insertCategory)
                {

                    return RedirectToAction("ViewAllocatedMonetaryData", $"AdminAllocation");
                }
                else
                {
                    return RedirectToAction("MonetaryAllocation", $"AdminAllocation");

                }

            }
            catch (Exception)
            {

                return RedirectToAction("MonetaryAllocation", $"AdminAllocation");
            }

        }


        [HttpPost]
        public IActionResult GoodsAllocationValidation()
        {

            int activeDisasterID, itemID, NumberOfItemsLimit;

            DateTime AllocationDate;
            try
            {
                activeDisasterID = int.Parse(Request.Form["ActiveDisasterID"].ToString());
                AllocationDate = DateTime.Parse(Request.Form["AllocationDate"].ToString());
                itemID = int.Parse(Request.Form["Items"].ToString());
                NumberOfItemsLimit = int.Parse(Request.Form["NumberOfItems"].ToString());


                


                bool insertCategory = userDetails.captureGoodsAllocationInfomation(activeDisasterID, itemID, AllocationDate, NumberOfItemsLimit);

                if (insertCategory)
                {
                    insertCategory = userDetails.UpdateAvailableNumberOfGoods(itemID, NumberOfItemsLimit);
                    if (insertCategory)
                    {
                        return RedirectToAction("ViewAllocatedGoodsData", $"AdminAllocation");
                    }
                    else
                    {
                        return Content("not updated");

                    }
                }
                else
                {
                    return RedirectToAction("GoodsAllocation", $"AdminAllocation");

                }

            }
            catch (Exception)
            {

                return RedirectToAction("GoodsAllocation", $"AdminAllocation");
            }

        }

        [HttpPost]
        public IActionResult PurchasedGoodsValidation()
        {
            int numberOfItems;
            string itemDesciption;
            DateTime donationDate;
            int category;
            int DonerID = 1;
            double Amount;

            numberOfItems = int.Parse(Request.Form["NumberOfItems"]);
            itemDesciption = Request.Form["descriptionOfItems"].ToString();
            donationDate = DateTime.Parse(Request.Form["GoodsDonaionDate"].ToString());
            category = int.Parse(Request.Form["CategoryID"]);
            Amount = int.Parse(Request.Form["Amount"]);

            // test = int.Parse(Request.Form["CategoryID"]);


            // return Content(category.ToString());
            if (Amount<=1)
            {
                return RedirectToAction("CapturePurchasedGoods", $"AdminAllocation");
            }

            bool insertCategory = userDetails.AddGoodsDonation(numberOfItems, donationDate, category, itemDesciption, DonerID);

            if (insertCategory)
            {
                ViewData["MonetaryDonation"] = "1";

               
                if (userDetails.LastGoodsPurchasedGoodID() != 0)
                {

                    bool insertPurchasedGoods = userDetails.capturePurchasedGoodsAmount(userDetails.LastGoodsPurchasedGoodID(),Amount, donationDate);

                    if (insertPurchasedGoods)
                    {
                        return RedirectToAction("ViewPurchasedGoods", $"AdminAllocation");
                    }
                    
                        return RedirectToAction("CapturePurchasedGoods", $"AdminAllocation");
                    
                    // return Content(test.ToString());


                }
                else
                {
                    return RedirectToAction("CapturePurchasedGoods", $"AdminAllocation");
                }

            }
            else
            {
                return RedirectToAction("CapturePurchasedGoods", $"AdminAllocation");
            }


        }
    }
}
