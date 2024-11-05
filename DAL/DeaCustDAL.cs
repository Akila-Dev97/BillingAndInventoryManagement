using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    internal class DeaCustDAL
    {
        //Creating static String Methods fo DB connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method for Dealer and Customer
        public DataTable Select()
        {
            //Creaing database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                //Writng SQL Query to get all the Dat freom database
                string sql = "SELECT * FROM tbl_dea_cust";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                // Opene databse connection
                conn.Open();

                //Adding value from Adapter to DataTable dt
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion 
        #region method to insert deatils to dealer and customer

        public bool Insert(DeaCustBLL dc)
        {
            //Creating A Boolean variable and set its default value to false
            bool isSuccess = false;

            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing query to  insert Products to databse

                string sql = "INSERT INTO tbl_dea_cust (type, name, email, contact, address, added_date, added_by) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by)";

                //Creating SQl Command to pass values in our query

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Values through parameter

                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                //Open Database Connection
                conn.Open();

                // Creating the int varaile to excute query
                int rows = cmd.ExecuteNonQuery();

                //If the query excuted successfully then its value wwill be greater than 0 else it will be less than 0

                if (rows > 0)
                {
                    //Query Excuted Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to excute Query
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        #region Update method for Dealer and Customer Module

        public bool Update(DeaCustBLL dc)
        {

            //Creating A Boolean variable and set its default value to false
            bool isSuccess = false;

            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to Update Category
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name email=@email, contact=@contact, address=@address added_date=@added_date, added_by=@added_by WHERE id=@id";
                //SQL Command to Pass the Value on sql Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Value usign cmd
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                //Open DataBase Connection
                conn.Open();

                //Create Int Vairable to excute query
                int rows = cmd.ExecuteNonQuery();

                //then the value will be greater than zero
                if (rows > 0)
                {
                    //Query Excuted Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Excute query
                    isSuccess = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();

            }
            return isSuccess;

        }
        #endregion
        #region Delete method for Delaer and Customer Module
        public bool Delete(DeaCustBLL dc)

        {

            //Create a Booleab vaiable and ser its valaue to false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL Query to Delete from Database

                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the value using cmd
                cmd.Parameters.AddWithValue("@id", dc.id);

                //Open SqlConnection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                //If the Query is excuted Succesfully then the vlaue of rows will greater than zero rles it will less than 0
                if (rows > 0)
                {
                    //Querty Excuted Succesfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to excute Query
                    isSuccess = false;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion
        #region Method for search functionalty
        public DataTable Search(string keywords)
        {
            //SQL Connection Fro Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Creating Data Table to Hold the data From Databse Temporiraly
            DataTable dt = new DataTable();

            try
            {

                //SQL Query to search dealer or customer from Databse
                string sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%" + keywords + "%' OR type LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%'";


                //Creating SQL command to excute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database
                conn.Open();
                //Paassing value from adpater to data table
                adapter.Fill(dt);

            }
            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

                conn.Close();
            }
            return dt;

        }
        #endregion
        #region Methoed to search dealer or customer for trancation module

        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {


            //Create an object for deaCustBLL class
            DeaCustBLL dc = new DeaCustBLL();

            //Create a database caonnection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create a data aTable to hold h vlaue Temporiraly
            DataTable dt = new DataTable();

            try
            {

                //SQL Query to selct Dealer or customer baes on keyowrds
                string sql = "SELECT name, email, contact, address FROM tbl_dea_cust WHERE id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";


                //Creating SQL command to excute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database
                conn.Open();

                //Paassing value from adpater to data table
                adapter.Fill(dt);

                //If we have values on dt we need to save it in Dealer Cutomer BLL
                if (dt.Rows.Count > 0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                }

            }
            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

                conn.Close();
            }

            return dc;

        }

        #endregion
        #region method to get ID oe thew dealer or Custoimer Based on Name
        public DeaCustBLL GetDeaCustIDFromName(string Name) 
        {


            //Create an object for deaCustBLL class
            DeaCustBLL dc = new DeaCustBLL();

            //Create a database caonnection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Create a data aTable to hold h vlaue Temporiraly
            DataTable dt = new DataTable();

            try
            {

                //SQL Query to selct Dealer or customer baes on keyowrds
                string sql = "SELECT id FROM tbl_dea_cust WHERE name = '"+Name+"'";


                //Creating SQL command to excute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Database
                conn.Open();

                //Paassing value from adpater to data table
                adapter.Fill(dt);

                //If we have values on dt we need to save it in Dealer Cutomer BLL
                if (dt.Rows.Count > 0)
                {
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());
                }

            }
            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

                conn.Close();
            }

            return dc;

        }

        #endregion


    }
}
