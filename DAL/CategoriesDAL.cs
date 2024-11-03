using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class CategoriesDAL
    {
        //Static String fpr Database Connection string
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        public DataTable Select()
        {
            //Creaing database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                //Writng SQL Query to get all the Dat freom database
                string sql = "SELECT * FROM tbl_categories";

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
        #region Insert New Catergories

        public bool Insert(CategoriesBLL c)
        {
            //Creating A Boolean variable and set its default value to false
            bool isSuccess = false;

            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing query to Add New catergory

                string sql = "INSERT INTO tbl_categories (title, description, added_date, added_by) VALUES (@title, @description, @added_date, @added_by)";

                //Creating SQl Command to pass values in our query

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Values through parameter

                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

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
        #region Update categories

        public bool Update(CategoriesBLL c)
        {

            //Creating A Boolean variable and set its default value to false
            bool isSuccess = false;

            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to Update Category
                string sql = "UPDATE tbl_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";
                //SQL Command to Pass the Value on sql Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Value usign cmd
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open DataBase Connection
                conn.Open();

                //Create Int Vairable to excute query
                int rows = cmd.ExecuteNonQuery();

                //if the query is successfully excuted then the value will be greater than zero
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
        #region Delete Catergory
        public bool Delete(CategoriesBLL c)

        {

            //Create a Booleab vaiable and ser its valaue to false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL Query to Delete from Database

                string sql = "DELETE FROM tbl_categories WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the value using cmd
                cmd.Parameters.AddWithValue("@id", c.id);

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

                //SQL Query to search catergories from Databse
                string sql = "SELECT * FROM tbl_categories WHERE id LIKE '%"+keywords+"%' OR title LIKE '%"+keywords+"%' OR description LIKE '%"+keywords+"%'";
    

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
    }
}