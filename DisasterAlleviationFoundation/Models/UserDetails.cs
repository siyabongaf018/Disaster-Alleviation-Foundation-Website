using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace DisasterAlleviationFoundation.Models
{
    public class RecordsDisplay
    {
        public DateTime MDonationDate { get; set; }
        public double Amount { get; set; }

    }

    public class DisasterDisplay
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string  location { get; set; }
        public string descripion { get; set; }
        public string  requiredAid { get; set; }
    }
    public class GoodsDonationDisplay
    {
        public int NumberOfItems { get; set; }
        public int AvailableNumberOfItems { get; set; }
        public DateTime DonationDate { get; set; }
        public string Category { get; set; }
        public string Desciption { get; set; }
        public int DonerID { get; set; }
    }



    public class PuchasedGoodsDonationDisplay
    {
        public int NumberOfItems { get; set; }
        public DateTime DonationDate { get; set; }
        public string Category { get; set; }
        public string Desciption { get; set; }
        public int DonerID { get; set; }
        public double  Amount { get; set; }
    }
    //part 2 view data
    public class AllocateMonetary
    {
        //DisasterID, Location, Description, AllocationDate, Amount
        public int DisasterID { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime AllocationDate { get; set; }
        public double Amount { get; set; }


    }
    public class AllocateGoods
    {
        public int DisasterID { get; set; }
        public DateTime AllocationDate { get; set; }
        public string Location { get; set; }
        public string DescriptionOfDisaster { get; set; }
        public string DescriptionOfItem { get; set; }
        public int NumberOfItems { get; set; }

    }

    public class GoodsQuantity
    {
        public int GoodID { get; set; }
        public int AvailableNumberOfItems { get; set; }
       
    }
    public class UserDetails
    {
        public String Category { get; set; }
        public string selectededCategory { get; set; }
        public string selectededItem { get; set; }

        public int UserID { get; set; }
        public int UseType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }



        //SqlConnection conn = new SqlConnection(@"Server=tcp:st10131002sever.database.windows.net,1433;Initial Catalog=ST10131002DB;Persist Security Info=False;User ID=s10131002-sever;Password=Obese/towny5;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DisasterEllevationFoundation;Integrated Security=True");


        SqlCommand command;
        SqlDataAdapter dataAdapter;
        string sql = "";
        DataTable dt2 = new DataTable();




        //display data fro monetary donation
        public List<RecordsDisplay> MonetaryDataDisplay()
        {
            List<RecordsDisplay> displays;

            try
            {
                sql = $"select DonationDate,Amount from MonetaryDonation";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                

                conn.Open();
                cmdSelect.Fill(ds);
                 displays = new List<RecordsDisplay>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    displays.Add(new RecordsDisplay
                    {
                        
                        MDonationDate = DateTime.Parse(dr["DonationDate"].ToString()),
                        Amount  = double.Parse(dr["Amount"].ToString())
                    }); 
                }

                conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return displays;

        }

        public List<AllocateMonetary> AllocatedMonetaryDataDisplay()
        {
            List<AllocateMonetary> displays;

            try
            {
                sql = $"select dma.DisasterID, dd.Location, dd.Description, dma.AllocationDate, dma.Amount from DisasterDetails dd " +
                    $"inner join DisasterMonetaryAllocation dma on dd.DisasterID = dma.DisasterID";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();


                conn.Open();
                cmdSelect.Fill(ds);
                displays = new List<AllocateMonetary>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    displays.Add(new AllocateMonetary
                    {
                        //DisasterID, Location, Description, AllocationDate, Amount

                        DisasterID = int.Parse(dr["DisasterID"].ToString()),
                        Location = dr["Location"].ToString(),
                        Description = dr["Description"].ToString(),
                        AllocationDate = DateTime.Parse(dr["AllocationDate"].ToString()),
                        Amount = double.Parse(dr["Amount"].ToString())
                    });
                }

                conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return displays;

        }

        public List<AllocateGoods> AllocatedGoodsDataDisplay()
        {
            List<AllocateGoods> displays;

            try
            {
                sql = $"select dd.DisasterID, dd.Description,dd.Location, dga.NumberOfItems ,gd.DescriptionOfItems, dga.AllocationDate from GoodsDonation gd  " +
                    $"inner join DisasterGoodsAllocation dga on gd.GoodsDonationID = dga.GoodsDonationID " +
                    $"inner join DisasterDetails dd on dd.DisasterID = dga.DisasterID";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();

                
                conn.Open();
                cmdSelect.Fill(ds);
                displays = new List<AllocateGoods>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    displays.Add(new AllocateGoods
                    {
                        //DisasterID, Location, Description, AllocationDate, Amount

                        DisasterID = int.Parse(dr["DisasterID"].ToString()),
                        Location = dr["Location"].ToString(),
                        DescriptionOfItem = dr["DescriptionOfItems"].ToString(),
                        DescriptionOfDisaster = dr["Description"].ToString(),
                        AllocationDate = DateTime.Parse(dr["AllocationDate"].ToString()),
                        NumberOfItems = int.Parse(dr["NumberOfItems"].ToString())
                    });
                }

                conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return displays;

        }

        public List<DisasterDisplay> DisasterDataDisplay()
        {
            List<DisasterDisplay> displaysDisaster;
            try
            {
                sql = $"select StartDate,EndDate,Location,RequiredAid,Description from DisasterDetails";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();


                conn.Open();
                cmdSelect.Fill(ds);
                displaysDisaster = new List<DisasterDisplay>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    displaysDisaster.Add(new DisasterDisplay
                    {

                        startDate = DateTime.Parse(dr["StartDate"].ToString().Replace("00:00:00", "")),
                        endDate = DateTime.Parse(dr["EndDate"].ToString().Replace("00:00:00", "")),
                        location = dr["Location"].ToString(),
                        requiredAid = dr["RequiredAid"].ToString(),
                        descripion = dr["Description"].ToString()
                    });
                }

                conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return displaysDisaster;

        }

        /// <summary>
        /// this meathod gets all the Goods donation data from the database
        /// </summary>
        /// <returns>List<GoodsDonationDisplay></returns>
        public List<GoodsDonationDisplay> GoodsDonationDataDisplay()
        {
            List<GoodsDonationDisplay> displaysDisasterGoodsDojnation;
            try
            {
                sql = $"select gd.NumberOfItems,gd.DonationDate,c.CategoryName, gd.DescripTionOfItems from Category c " +
                    $" inner join GoodsDonation gd on c.CategoryID = gd.CategoryID";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();


                conn.Open();
                cmdSelect.Fill(ds);
                displaysDisasterGoodsDojnation = new List<GoodsDonationDisplay>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    displaysDisasterGoodsDojnation.Add(new GoodsDonationDisplay
                    {
                        /* public int NumberOfItems { get; set; }
        public DateTime DonationDate { get; set; }
        public string Category { get; set; }
        public string Desciption { get; set; }
        public int DonerID { get; set; }*/
                        DonationDate = DateTime.Parse(dr["DonationDate"].ToString().Replace("00:00:00", "")),
                        NumberOfItems = int.Parse(dr["NumberOfItems"].ToString()),

                        Category = dr["CategoryName"].ToString(),
                        Desciption = dr["DescripTionOfItems"].ToString()
                    });
                }

                conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return displaysDisasterGoodsDojnation;

        }

        public List<PuchasedGoodsDonationDisplay> PurchasedGoodsDisplay()
        {
            List<PuchasedGoodsDonationDisplay> displaysPurchaedGoodsDojnation;
            try
            {
                sql = $"SELECT gd.NumberOfItems,gd.DonationDate,c.CategoryName, gd.DescripTionOfItems, pg.Amount FROM GoodsDonation gd " +
                    $"inner join PurchasedGoods pg on gd.GoodsDonationID = pg.GoodsDonationID " +
                    $"inner join Category c on c.CategoryID = gd.CategoryID";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();


                conn.Open();
                cmdSelect.Fill(ds);
                displaysPurchaedGoodsDojnation = new List<PuchasedGoodsDonationDisplay>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    displaysPurchaedGoodsDojnation.Add(new PuchasedGoodsDonationDisplay
                    {
                        DonationDate = DateTime.Parse(dr["DonationDate"].ToString().Replace("00:00:00", "")),
                        NumberOfItems = int.Parse(dr["NumberOfItems"].ToString()),
                        Category = dr["CategoryName"].ToString(),
                        Desciption = dr["DescripTionOfItems"].ToString(),
                        Amount = double.Parse(dr["Amount"].ToString())
                    });
                }

                conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return displaysPurchaedGoodsDojnation;

        }

        // HashingPassword hashingPassword = new HashingPassword();

        public string me()
        {
            string me = "me ->" + this.Email;
            return me;
        }

        public Boolean captureDisasterInfomation(String location, string disasterDescription, string RequiredAid, DateTime startDate, DateTime endDate,string userid)
        {
            int rowCount = 0;

            try
            {
                conn.Open();
                //sql = $"Insert into Disaster (StartDate, EndDate, Location, Description, RequiredAid, DonerID  ) values('{startDate}','{endDate}','{location}','{disasterDescription}','{RequiredAid}',donerID)";
                sql = $"Insert into DisasterDetails (StartDate, EndDate, Location, Description, RequiredAid, DonerID  ) values('{startDate}','{endDate}','{location}','{disasterDescription}','{RequiredAid}',{userid})";
                //sql = "Select * from Doner ";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                rowCount = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                // errorMessage = e.Message;
            }
           // return rowCount < 0;
            if (rowCount < 0)
            {
                // ViewData["Message"] = "Your application description page."; = "Record Inserted successfully";
                return false;
            }
            else
            {
                return true;
            }

        }


        public bool UserRegister(string Email, string Password, string confirmPasword, string name, string surname)
        {
            int rowCount = 0;

            try
            {
                conn.Open();
                //sql = $"Insert into Doner (Name , Surname , Email , Password ) values('{Name}' , '{surname}' , '{Email}' , '{hashingPassword.EncryptionPassword(Password)}')";

                sql = $"Insert into Doner (Name , Surname , Email , Password, UserType ) values('{name}' , '{surname}' , '{Email}' , '{Password}',DEFAULT)";
                //sql = "Select * from Doner ";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                rowCount = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                // errorMessage = e.Message;
            }
            if (rowCount < 0)
            {
                // ViewData["Message"] = "Your application description page."; = "Record Inserted successfully";
                return false;
            }
            else
            {
                return true;
            }




        }

        public Boolean captureMonetaryAllocationInfomation(int activeDisasterID,double Amount,DateTime AllocationDate)
        {
            int rowCount = 0;

            try
            {
                if (UserID<=0)
                {
                    UserID = 1;
                }
                conn.Open();
                 sql = $"Insert into DisasterMonetaryAllocation (AllocationDate, Amount, DisasterID, DonerID ) values('{AllocationDate}', {Amount}, {activeDisasterID},{UserID})";


                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                rowCount = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                // errorMessage = e.Message;
            }
            if (rowCount < 0)
            {
                // ViewData["Message"] = "Your application description page."; = "Record Inserted successfully";
                return false;
            }
            else
            {
                return true;
            }

        }

        public Boolean captureGoodsAllocationInfomation(int activeDisasterID,int itemID, DateTime AllocationDate, int NumberOfItems)
        {
            int rowCount = 0;

            try
            {

                if (UserID <= 0)
                {
                    UserID = 1;
                }
            //    var w = HttpContext.Session.Getint32("UserID");
                // var userid = HttpStyleUriParser.
                conn.Open();
                sql = $"Insert into DisasterGoodsAllocation (AllocationDate, DisasterID, GoodsDonationID, DonerID,NumberOfItems ) values('{AllocationDate}', {activeDisasterID},  {itemID},{UserID},{NumberOfItems})";


                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                rowCount = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                // errorMessage = e.Message;
            }
            if (rowCount < 0)
            {
                // ViewData["Message"] = "Your application description page."; = "Record Inserted successfully";
                return false;
            }
            else
            {
                return true;
            }

        }

        public Boolean capturePurchasedGoodsAmount(int GoodDonationID, double  Amount, DateTime PurchaseDate)
        {
            int rowCount = 0;

            try
            {
                
                conn.Open();
                sql = $"Insert into PurchasedGoods (GoodsDonationID, Amount, PurchaseDate ) values({GoodDonationID},{Amount},'{PurchaseDate}')";


                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                rowCount = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                // errorMessage = e.Message;
            }
            if (rowCount < 0)
            {
                // ViewData["Message"] = "Your application description page."; = "Record Inserted successfully";
                return false;
            }
            else
            {
                return true;
            }

        }

        public int uidRecord()
        {
            return int.Parse(dt2.Rows[0][0].ToString());
        }

        public string userRecord()
        {
            return dt2.Rows[0][3].ToString();
        }



        public bool ValidateLoginCredentials(string email, string password)
        {

            try
            {
                sql = $"select * from Doner where Email = '{email}'  AND Password = '{password}' ";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                


                conn.Open();
                cmdSelect.Fill(dt2);

                conn.Close();

                if (dt2.Rows.Count > 0)
                {
                    Email = email;
                    return true;
                }
                else { return false; }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }




        }

        public int GetUserID(string email, string password)
        {

            try
            {
                sql = $"select * from Doner where Email = '{email}'  AND Password = '{password}' ";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);



                conn.Open();
                cmdSelect.Fill(dt2);

                conn.Close();
                int UserId = 0;
                if (dt2.Rows.Count > 0)
                {
                    Email = email;
                    UserId = int.Parse(dt2.Rows[0][0].ToString());
                    return UserId;
                }
                else { return UserId; }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }




        }

        public int GetUserType(string email, string password)
        {

            try
            {
                sql = $"select * from Doner where Email = '{email}'  AND Password = '{password}' ";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);



                conn.Open();
                cmdSelect.Fill(dt2);

                conn.Close();

                if (dt2.Rows.Count > 0)
                {
                    Email = email;
                    this.UseType = int.Parse(dt2.Rows[0][5].ToString());
                    return this.UseType;
                }
                else { return this.UseType; }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }




        }
        public SelectList getAllActiveDisaster()
        {
            try
            {
                sql = $"select * from DisasterDetails where EndDate>GETDATE()";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataTable _dt = new DataTable();
                conn.Open();
                cmdSelect.Fill(_dt);

                conn.Close();


                List<SelectListItem> list = new List<SelectListItem>();

                foreach (DataRow row in _dt.Rows)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = row[4].ToString(),
                        Value = row[0].ToString()
                    });
                }

                return new SelectList(list, "Value", "Text");




            }
            catch (Exception)
            {

                throw;
            }

        }

        public SelectList getAllGoods()
        {
            try
            {
                sql = $"select * from GoodsDonation where AvailableNumberOfItems>0 ";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataTable _dt = new DataTable();
                conn.Open();
                cmdSelect.Fill(_dt);

                conn.Close();


                List<SelectListItem> list = new List<SelectListItem>();

                foreach (DataRow row in _dt.Rows)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = row[5].ToString(),
                        Value = row[0].ToString()
                    });
                }

                return new SelectList(list, "Value", "Text");




            }
            catch (Exception)
            {

                throw;
            }

        }

        public int[] getAllGoodsQuantity()
        {
            try
            {
                sql = $"select * from GoodsDonation where AvailableNumberOfItems>0 ";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataTable _dt = new DataTable();
                conn.Open();
                cmdSelect.Fill(_dt);

                conn.Close();

                int count = getAllGoods().Count();
                int[] list = new int[count];

                int i= 0;
                foreach (DataRow row in _dt.Rows)
                {
                    list[i] = int.Parse(row[2].ToString());
                    i += 1;
                }

                return list;




            }
            catch (Exception)
            {

                throw;
            }

        }








        public SelectList getAllCategory()
        {
            try
            {
                sql = $"select * from Category";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataTable _dt = new DataTable();
                conn.Open();
                cmdSelect.Fill(_dt);

                conn.Close();


                List<SelectListItem> list = new List<SelectListItem>();

                foreach (DataRow row in _dt.Rows)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = row[1].ToString(),
                        Value = row[0].ToString()
                    });
                }

                return new SelectList(list, "Value", "Text");




            }
            catch (Exception)
            {

                throw;
            }

        }

        public int LastGoodsPurchasedGoodID()
        {
            try
            {
                sql = $"SELECT TOP 1 * FROM GoodsDonation order by GoodsDonationID desc";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataTable _dt = new DataTable();
                conn.Open();
                cmdSelect.Fill(_dt);

                conn.Close();
                int GoodsID = 0;
                
                if (_dt.Rows.Count > 0)
                {

                    GoodsID = int.Parse(_dt.Rows[0][0].ToString());
                    return GoodsID;
                }
                else { return GoodsID; }



            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool AddMonetaryDonation(int amount, DateTime donationDate)
        {
            int rowCount = 0;
            try
            {
                conn.Open();
                sql = $"Insert into MonetaryDonation (DonationDate,Amount,DonerID) values('{donationDate}', {amount}, 1 )";
                //sql = "Select * from Doner ";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                rowCount = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                // errorMessage = e.Message;
            }
            if (rowCount < 0)
            {
                // ViewData["Message"] = "Your application description page."; = "Record Inserted successfully";
                return false;
            }
            else
            {
                return true;
            }

        }

        public Boolean AddCategory(string CategoryName)
        {
            int rowCount = 0;
            try
            {
                conn.Open();

                sql = $"Insert into Category (CategoryName) values('{CategoryName}')";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                rowCount = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                // errorMessage = e.Message;
            }
            if (rowCount < 0)
            {
                // ViewData["Message"] = "Your application description page."; = "Record Inserted successfully";
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool AddGoodsDonation(int amountOfItems, DateTime GoodsdonationDate,int Category, string Desciption, int DonerID)
        {
            int rowCount = 0;
            try
            {
                conn.Open();
                sql = $"Insert into GoodsDonation (NumberOfItems,AvailableNumberOfItems,DonationDate,CategoryID,DescriptionOfItems,DonerID) values({amountOfItems},{amountOfItems},'{GoodsdonationDate}',{Category}, '{Desciption}', {DonerID} )";
                //sql = "Select * from Doner ";

                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                rowCount = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                // errorMessage = e.Message;
            }
            if (rowCount < 0)
            {
                // ViewData["Message"] = "Your application description page."; = "Record Inserted successfully";
                return false;
            }
            else
            {
                return true;
            }

        }


        //part 3 poe

        public double GetTotalMonetaryDonations()
        {
            double total = 0;
            try
            {
              

                sql = $"select  sum(Amount) from MonetaryDonation HAVING SUM(Amount)>0";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataTable dt2 = new DataTable();
                conn.Open();
                cmdSelect.Fill(dt2);

                conn.Close();

                if (dt2.Rows.Count > 0)
                {

                    total =  double.Parse(dt2.Rows[0][0].ToString());
                        //total = int.Parse(dt2.Rows[0][0].ToString());
                    
                }
                else {  total = 0 ; }


                return total;


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }

        }

        public double GetTotalAmountOfPurchasedGoods()
        {
            double total = 0;
            try
            {


                sql = $"select sum(Amount) as Amount from PurchasedGoods HAVING SUM(Amount)>0";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataTable dt2 = new DataTable();
                conn.Open();
                cmdSelect.Fill(dt2);

                conn.Close();

                if (dt2.Rows.Count > 0)
                {

                    total = double.Parse(dt2.Rows[0][0].ToString());
                    //total = int.Parse(dt2.Rows[0][0].ToString());

                }
                else { total = 0; }


                return total;


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }

        }

        public double GetTotalAmountOfAllocatedMonetary()
        {
            double total = 0;
            try
            {


                sql = $"select sum(Amount) as Amount from DisasterMonetaryAllocation HAVING SUM(Amount)>0";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataTable dt2 = new DataTable();
                conn.Open();
                cmdSelect.Fill(dt2);

                conn.Close();

                if (dt2.Rows.Count > 0)
                {

                    total = double.Parse(dt2.Rows[0][0].ToString());
                    //total = int.Parse(dt2.Rows[0][0].ToString());

                }
                else { total = 0; }


                return total;


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }

        }


        public int GetTotalNumberOfGoodsDonation()
        {
            int total = 0;
            try
            {
                sql = $"select  sum(GoodsDonationID) from GoodsDonation HAVING SUM(GoodsDonationID)>0";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);

                conn.Open();
                cmdSelect.Fill(dt2);

                conn.Close();

                if (dt2.Rows.Count > 0)
                {
                    total = int.Parse(dt2.Rows[0][0].ToString());
                    return total;
                }
                else { return total; }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }

        }

        /// <summary>
        /// get all active disasters from the database
        /// </summary>
        /// <returns>List<DisasterDisplay></returns>
        public List<DisasterDisplay> ActiveDisasterDataDisplay()
        {
            List<DisasterDisplay> displaysDisaster;
            try
            {
                sql = $"select StartDate,EndDate,Location,RequiredAid,Description from DisasterDetails where EndDate>GETDATE()";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();


                conn.Open();
                cmdSelect.Fill(ds);
                displaysDisaster = new List<DisasterDisplay>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    displaysDisaster.Add(new DisasterDisplay
                    {

                        startDate = DateTime.Parse(dr["StartDate"].ToString().Replace("00:00:00", "")),
                        endDate = DateTime.Parse(dr["EndDate"].ToString().Replace("00:00:00", "")),
                        location = dr["Location"].ToString(),
                        requiredAid = dr["RequiredAid"].ToString(),
                        descripion = dr["Description"].ToString()
                    });
                }

                conn.Close();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return displaysDisaster;

        }

        public int GetTotalNumberOfGoodsAvailable(int GoodsDonationID)
        {
            int total = 0;
            try
            {
                sql = $"select AvailableNumberOfItems from GoodsDonation WHERE GoodsDonationID ={GoodsDonationID}";
                SqlDataAdapter cmdSelect = new SqlDataAdapter(sql, conn);

                conn.Open();
                cmdSelect.Fill(dt2);

                conn.Close();

                if (dt2.Rows.Count > 0)
                {
                    total = int.Parse(dt2.Rows[0][0].ToString());
                    return total;
                }
                else { return total; }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }

        }

        public Boolean UpdateAvailableNumberOfGoods(int GoodDonationID, int AvailableNumberOfItems)
        {
            int rowCount = 0;

            try
            {
                int value = GetTotalNumberOfGoodsAvailable(GoodDonationID)  -  AvailableNumberOfItems;
            conn.Open();
                sql = $"UPDATE GoodsDonation SET AvailableNumberOfItems = {value} WHERE GoodsDonationID = {GoodDonationID}";
              //  sql = $"UPDATE GoodsDonation SET AvailableNumberOfItems =  {AvailableNumberOfItems} WHERE GoodsDonationID = {GoodDonationID}";


                command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                rowCount = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {
                // errorMessage = e.Message;
            }
            if (rowCount < 0)
            {
                // ViewData["Message"] = "Your application description page."; = "Record Inserted successfully";
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
