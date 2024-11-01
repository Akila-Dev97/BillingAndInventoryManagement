using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    internal class userDAL 
    {
        static String myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select data From Database

        public DataTable Select()
        {  //Static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            // To hoid the data from databse
            DataTable dt = new DataTable();
            try
            {
                //SQL query to get Data from Database
                string sql = "SELECT * FROM tbl_users";
                // For executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Gettign Data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Database Connection Open
                conn.Open();
                // Fill the data in out data table
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Throw Message if any Error Occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // closing Connecion
                conn.Close();
            }
            // Return the value in Datatable
            return dt;
        }
        #endregion

        #region Insert Data in Database
        public bool Insert(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                String sql = "INSERT INTO tbl_users (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by) VALUES (@first_name, @last_name, @email, @username, @password, @contact,@address, @gender, @user_type, @added_date, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                //If the query is excured succesfully then the vlaue to row will be greater thean 0 else it  will be less than 0

                if (rows > 0)
                {
                    isSuccess = true;

                }
                else
                {
                    //query Failed
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

        #region Update Data in Database

        public bool Update(userBLL u)
        {

            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);


            try
            {

                string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, gender=@gender, user_type=@user_type, added_date=@added_date, added_date=@added_date, added_by=@ added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("id", u.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows>0 )
                {
                    //query succesfull
                    isSuccess = true;
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

            return isSuccess;

        }




        #endregion

        #region Delete Data from Database

        public bool Delete(userBLL user)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);


            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", user.id);
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    //Query successful
                    isSuccess = true;
                }
                else
                {
                    //Query failed
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

        #region Search User on database usingKeywords


        public DataTable Search(string keywords)
        {  //Static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            // To hoid the data from databse
            DataTable dt = new DataTable();
            try
            {
                //SQL query to get Data from Database
                string sql = "SELECT * FROM tbl_users WHERE id LIKE '%" +keywords+ "%' OR first_name LIKE '%" +keywords+ "%' OR last_name LIKE '%" +keywords+ "%' OR username LIKE '%" +keywords+ "%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Gettign Data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Database Connection Open
                conn.Open();
                // Fill the data in out data table
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Throw Message if any Error Occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // closing Connecion
                conn.Close();
            }
            // Return the value in Datatable
            return dt;
        }



        #endregion
        #region Geting User ID from username
        public userBLL GetIDFromUsername(string username)
        {
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT id FROM tbl_users WHERE username = '" + username + "'";
                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    u.id = int.Parse(dt.Rows[0]["id"].ToString());
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
            return u;
        }


        #endregion
    }

}
