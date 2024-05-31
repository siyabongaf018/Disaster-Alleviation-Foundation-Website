using DisasterAlleviationFoundation.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FundationUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        UserDetails userDetails = new UserDetails();

        /// <summary>
        /// checks if the username and password exist in the database by validating login credentials
        /// </summary>
        [Test]
        public void LoginTest()
        {
            //Arrange 
            string username = "Anonymous@gmail.com";
            string password = "1024A";
            bool expected = true;


            //actual
            bool actual = userDetails.ValidateLoginCredentials(username, password);


            //asset
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// check i the user is an admin or a normal user
        /// </summary>
        [Test]
        public void GetUserTyper()
        {
            //Arrange 
            string username = "Anonymous@gmail.com";
            string password = "1024A";
            int expected = 0;


            //actual
            int actual = userDetails.GetUserType(username, password);


            //asset
            Assert.AreEqual(expected, actual);
         
        }

        [Test]
        public void GetUserID()
        {
            //Arrange 
            string username = "Anonymous@gmail.com";
            string password = "1024A";
            int expected = 1;


            //actual
            int actual = userDetails.GetUserID(username, password);


            //asset
            Assert.AreEqual(expected, actual);

        }

        //insert statments
        [Test]
        public void InsertUserRegister()
        {
            //Arrange 
            string Email ="siya@gmail.com", 
                Password = "104A", 
                confirmPasword = "1024A", 
                name = "S.F", 
                surname = "C";
            bool expected = true;
          

            //actual
            bool actual = userDetails.UserRegister( Email,  Password,  confirmPasword,  name,  surname);

            //asset
            Assert.AreEqual(expected, actual);
        }

        //2022/10/24 00:00:00
        [Test]
        public void InsertMonetaryDonation()
        {
            //Arrange 
            DateTime DonationDate = DateTime.Parse("2022/10/24 00:00:00");
            bool expected = true;
            int amount = 6000;

            //actual
            bool actual = userDetails.AddMonetaryDonation(amount, DonationDate);

            //asset
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InsertDisasterInfomation()
        {
            //Arrange 
           
            String location = "PTA Central ", 
                disasterDescription = "heavy rain lead to shortage of supplies", 
                RequiredAid = "clothes, food";
            DateTime startDate = DateTime.Parse("2022/10/24 00:00:00"), 
                endDate = DateTime.Parse("2022/11/24 00:00:00");
            string userid ="1";
            bool expected = true;
       
            //actual
            bool actual = userDetails.captureDisasterInfomation(location,  disasterDescription,  RequiredAid,  startDate,  endDate,  userid);

            //asset
            Assert.AreEqual(expected, actual);
        }

        //MonetaryAllocationInfomation
        [Test]
        public void InsertMonetaryAllocationInfomation()
        {
            //Arrange 
            int activeDisasterID = 1;
            double Amount = 1200;
            DateTime AllocationDate = DateTime.Parse("2022/11/09 00:00:00");
            bool expected = true;

            //actual
            bool actual = userDetails.captureMonetaryAllocationInfomation( activeDisasterID,  Amount,  AllocationDate);

            //asset
            Assert.AreEqual(expected, actual);
        }

        //captureGoodsAllocationInfomation
        [Test]
        public void InsertGoodsAllocationInfomation()
        {
            //Arrange 
            int activeDisasterID =1;
            int itemID = 1;
            int numberOfItems = 1;
            DateTime AllocationDate = DateTime.Parse("2022/11/09 00:00:00");
            bool expected = true;

            //actual
            bool actual = userDetails.captureGoodsAllocationInfomation( activeDisasterID,  itemID,  AllocationDate, numberOfItems);

            //asset
            Assert.AreEqual(expected, actual);
        }

        //AddGoodsDonation
        [Test]
        public void InsertAddGoodsDonation()
        {
            //Arrange 
            int amountOfItems = 1;
            DateTime GoodsdonationDate = DateTime.Parse("2022/11/09 00:00:00");
            int Category=1;
            string Desciption = "Trauser";
            int DonerID = 1;
            bool expected = true;
            //actual
            bool actual = userDetails.AddGoodsDonation( amountOfItems,  GoodsdonationDate,  Category,  Desciption,  DonerID);

            //asset
            Assert.AreEqual(expected, actual);
        }

        //capturePurchasedGoodsAmount
        [Test]
        public void InsertPurchasedGoodsAmount()
        {
            //Arrange 
            int GoodDonationID=1;
            double Amount=1000;
            DateTime PurchaseDate = DateTime.Parse("2022/11/09 00:00:00");
            bool expected = true;
            //actual
            bool actual = userDetails.capturePurchasedGoodsAmount( GoodDonationID,   Amount,  PurchaseDate);

            //asset
            Assert.AreEqual(expected, actual);
        }
        //AddMonetaryDonation
        [Test]
        public void InsertAddMonetaryDonation()
        {
            //Arrange 
            int amount = 500;
            DateTime donationDate = DateTime.Parse("2022/11/09 00:00:00");
            bool expected = true;
            //actual
            bool actual = userDetails.AddMonetaryDonation( amount,  donationDate);

            //asset
            Assert.AreEqual(expected, actual);
        }
        //AddCategory
        [Test]
        public void InsertAddCategory()
        {
            //Arrange 
            string CategoryName = "Electronic Gadget";
            bool expected = true;
            //actual
            bool actual = userDetails.AddCategory( CategoryName);

            //asset
            Assert.AreEqual(expected, actual);
        }

        // test methods retun a list to check if the list has values
        //MonetaryDataDisplay
        [Test]
        public void SelectMonetaryDataDisplay()
        {
            //Arrange 
            bool expected = true;
            //actual
            bool actual = userDetails.MonetaryDataDisplay().Count > 0;

            //asset
            Assert.AreEqual(expected, actual);
        }

        //AllocatedMonetaryDataDisplay
        [Test]
        public void SelectAllocatedMonetaryDataDisplay()
        {
            //Arrange 
            bool expected = true;
            //actual
            bool actual = userDetails.AllocatedMonetaryDataDisplay().Count > 0;

            //asset
            Assert.AreEqual(expected, actual);
        }

        //AllocatedGoodsDataDisplay
        [Test]
        public void SelectAllocatedGoodsDataDisplay()
        {
            //Arrange 
            bool expected = true;
            //actual
            bool actual = userDetails.AllocatedGoodsDataDisplay().Count > 0;

            //asset
            Assert.AreEqual(expected, actual);
        }

        //DisasterDataDisplay
        [Test]
        public void SelectDisasterDataDisplay()
        {
            //Arrange 
            bool expected = true;
            //actual
            bool actual = userDetails.DisasterDataDisplay().Count > 0;

            //asset
            Assert.AreEqual(expected, actual);
        }

        //GoodsDonationDataDisplay
        [Test]
        public void SelectGoodsDonationDataDisplay()
        {
            //Arrange 
            bool expected = true;
            //actual
            bool actual = userDetails.GoodsDonationDataDisplay().Count > 0;

            //asset
            Assert.AreEqual(expected, actual);
        }
        //PurchasedGoodsDisplay
        [Test]
        public void SelectPurchasedGoodsDisplay()
        {
            //Arrange 
            bool expected = true;
            //actual
            bool actual = userDetails.PurchasedGoodsDisplay().Count > 0;

            //asset
            Assert.AreEqual(expected, actual);
        }


        //GetTotalNumberOfGoodsAvailable
        [Test]
        public void SelectTotalNumberOfGoodsAvailable()
        {
            //Arrange 
            bool expected = true;
            int goodID = 1;
            //actual
            bool actual = userDetails.GetTotalNumberOfGoodsAvailable(goodID) > 0;

            //asset
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateAvailableNumberOfGood()
        {
            //Arrange 
            int GoodsID = 1;
            int numberOFItems = 1;
            bool expected = true;
            //actual
            bool actual = userDetails.UpdateAvailableNumberOfGoods(GoodsID, numberOFItems);

            //asset
            Assert.AreEqual(expected, actual);
        }
    }
}