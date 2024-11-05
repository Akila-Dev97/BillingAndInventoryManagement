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
    internal class transactionDAL
    {
        //Creating static String Methods fo DB connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Transcation Insert emthoed

        public bool Insert_Transaction(transactionBLL t, out int transacayionID)
        {

            //Create a boolean value and set its for default value to false
            bool isSuccess = false;

            //Set the TransctionID value to negative 1 i.e -1
            transacayionID = -1;

            //Create SQL connection first
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing query to  insert transactions to databse

                string sql = "INSERT INTO tbl_transactions (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY";

                //Creating SQl Command to pass values in our query

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Values through parameter

                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id);
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                cmd.Parameters.AddWithValue("@tax", t.tax);
                cmd.Parameters.AddWithValue("@discount", t.discount);
                cmd.Parameters.AddWithValue("@added_by", t.added_by);

                //Open Database Connection
                conn.Open();

                //Excute the query
                object o = cmd.ExecuteScalar();

                //If the query is excyted succfully the vlaue wiill not be null elase it will be null

                if (o != null)
                {
                    //Query Excuted successfully
                    isSuccess = true;
                }
                else
                {
                    //failed to excute Query
                    isSuccess = false;
                }
            }

            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {

                //Finally we close the connection
                conn.Close();
            }
            return isSuccess;
        }

        #endregion

        #region Method to display al the trnasaction

        public DataTable DisplayAllTheTransactions()
        {
            //Sql Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Create a data table to hold the data from databss etempo
            DataTable dt = new DataTable();

            try
            {
                //Write the SQL Query toi DDiplay all trnasctions
                string sql = "SELECT * FROM tbl_transactions";

                //Sql Comant to excute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SqkData Adpoater to excute Query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open DataBase connection
                conn.Open();

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

        #region Method to display tranaction based on tranaction type

        public DataTable DisplayTransactionByType(string type)
        {
            //CreateSQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //CreeateDatTable
            DataTable dt = new DataTable();

            try
            {
                //Writing SQL Query
                string sql = "SELECT * FROM tbl_Transactions WHERE type='"+type+"'";

                //SQL command to excute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

                //SQL DatApater to hold dat from Dat base
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open Datbase connection
                conn.Open();
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
