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

                string sql = "INSERT INTO tbl_transaction (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by)";

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

    }
}
