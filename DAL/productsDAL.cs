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
    class productsDAL
    {
        //Creating static String Methods fo DB connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method for products
        public DataTable Select()
        {
            //Creaing database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                //Writng SQL Query to get all the Dat freom database
                string sql = "SELECT * FROM tbl_products";

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

        #region method to insert product in databse

        public bool Insert(productsBLL p)
        {
            //Creating A Boolean variable and set its default value to false
            bool isSuccess = false;

            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing query to  insert Products to databse

                string sql = "INSERT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";

                //Creating SQl Command to pass values in our query

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Values through parameter

                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

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

        #region Update Products module

        public bool Update(productsBLL p)
        {

            //Creating A Boolean variable and set its default value to false
            bool isSuccess = false;

            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Query to Update Category
                string sql = "UPDATE tbl_categories SET name=@name, category=@category description=@description, rate=@rate, added_date=@added_date, added_by=@added_by WHERE id=@id";
                //SQL Command to Pass the Value on sql Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Value usign cmd
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);

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

        #region Delete Catergory
        public bool Delete(productsBLL p)

        {

            //Create a Booleab vaiable and ser its valaue to false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL Query to Delete from Database

                string sql = "DELETE FROM tbl_products WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the value using cmd
                cmd.Parameters.AddWithValue("@id", p.id);

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
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%'";


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

        #region methoed serach prodcut in trnasction module
        public productsBLL GetProductsForTransaction(string keyword)
        { 
        //Creat an oobject of prdocutBLL and retun it
        productsBLL p = new productsBLL();
        //SQL connection
        SqlConnection conn = new SqlConnection(myconnstrng);
        //dataTable  to store data temporiryl
        DataTable dt = new DataTable();
         try
         {
             //SQL Query to get the deatisl 
             string sql = "SELECT name, rate, qty FROM tbl_products WHERE id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%'";

      
                //Creating SQL command to excute the Query
        SqlCommand cmd = new SqlCommand(sql, conn);

        //Getting Data from Database
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        //Open Database
        conn.Open();
                //Paassing value from adpater to data table
                adapter.Fill(dt);

                //If we have any values on dt then set the values to prodcutBll

                if(dt.Rows.Count > 0)
                {

                    p.name = dt.Rows[0]["Name"].ToString();
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }

            }
           
            catch(Exception ex)

            {

                MessageBox.Show(ex.Message);
            }
            finally

            {
              conn.Close();
            }

           return p;
         }
          #endregion
          
    }
}
 

