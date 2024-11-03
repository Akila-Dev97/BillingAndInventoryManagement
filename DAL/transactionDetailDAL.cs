using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    internal class transactionDetailDAL
    {
        //Creating static String Methods fo DB connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

         #region TranscationID details Insert emthoed

        public bool Insert_TransactionDetail(transcationDetailsBLL td) 

        {

            //Create a boolean value and set its for default value to false
            bool isSuccess = false;

            //Create SQL connection first
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Writing query to  insert transactions to databse

                string sql = "INSERT INTO tbl_transaction_details (product_id, rate, qty, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                //Creating SQl Command to pass values in our query

                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing Values through parameter

                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@qty", td.qty);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@added_date", td.added_date);
                cmd.Parameters.AddWithValue("@added_by", td.added_by);

                //Open Database Connection
                conn.Open();

                //Declare the int variable and excute the query 
                int rows = cmd.ExecuteNonQuery();


                if (rows>0)
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
    }
}
