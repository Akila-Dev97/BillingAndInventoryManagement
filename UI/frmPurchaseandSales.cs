using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmPurchaseandSales : Form
    {
        public frmPurchaseandSales()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        DeaCustDAL dcDal = new DeaCustDAL();
        productsDAL pDAL = new productsDAL();
        userDAL uDAL = new userDAL();
        DataTable tranactionDT = new DataTable();
        transactionDAL tDAL = new transactionDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmPurchaseandSales_Load(object sender, EventArgs e)
        {
            //Get the Tranctiontyep value form useer dash bord

            string type = frmUserDashboard.transactionType;

            //Set the value on LblTop
            lblTop.Text = type;

            //sepcify Columns for out trrnaction Data table
            tranactionDT.Columns.Add("Product Name");
            tranactionDT.Columns.Add("Rate");
            tranactionDT.Columns.Add("Quantity");
            tranactionDT.Columns.Add("Total");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the keyword from form
            string keyword = txtSearch.Text;

            if (keyword == "")
            {
                //clear all textboxes
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;

            }

            //wriet the code to ge the detail and set th value on text boxes
            DeaCustBLL dc = dcDal.SearchDealerCustomerForTransaction(keyword);

            //Now Transfer or set the value from DeaCusatBll to textboxes

            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;

        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            //Get the keyword from productSearch Textbox
            string keyword = txtSearchProduct.Text;

            //Chekc if we have value to txtSearchProduct or not
            if (keyword == "")
            {
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtQty.Text = "";
                return;
            }

            //Search prodcuts and display on respective text boxes
            productsBLL p = pDAL.GetProductsForTransaction(keyword);

            //set the value on textboxes base on p object
            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Getting Product Name, Rate and Qty customer want to buy
            string ProductName = txtProductName.Text;
            decimal Rate = decimal.Parse(txtRate.Text);
            decimal Qty = decimal.Parse(txtQty.Text);

            decimal Total = Rate * Qty; //Total = Rate x Qty

            //Dsiplay the sub totoal  on texboxes
            //Get the subotal value from the text box
            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + Total;

            //Check wether thje product is scaled or not
            if (ProductName == "")
            {
                //Diplay error Message
                MessageBox.Show("Select the product first. Try again");

            }
            else
            {
                //add proidct to the Data Grid View 
                tranactionDT.Rows.Add(ProductName, Rate, Qty, Total);

                //Show Data in dat grid
                dgvAddedProducts.DataSource = tranactionDT;

                //display the Subtotal in textbox
                txtSubTotal.Text = subTotal.ToString();

                //Clear the TextBoxes
                txtSearchProduct.Text = "";
                txtProductName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "0.00";
                txtQty.Text = "0.00";
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            string value = txtDiscount.Text;

            if (value == "")
            {
                //Display Error message
                MessageBox.Show("Please add discount first");

            }
            else
            {
                //Get the dsicocunt in decimal value
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);

                //Calulate the Frabdtotal on dsicount
                decimal grandTotal = ((100 - discount) / 100) * subTotal;

                //display the grandTotal in text box
                txtGrandTotal.Text = grandTotal.ToString();

            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            //check if the grand total has value or not if it has value then calculate the discount fristt
            string check = txtGrandTotal.Text;
            if (check == "")
            {
                //diplay the error message to caluclate discount first
                MessageBox.Show("calculate the discount and set the grand total first");

            }
            else
            {
                //Calculate VAT
                //Getting the VAT percent Fisrt
                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVAT = ((100 + vat) / 100) * previousGT;

                //Dsdiplay new grand total weiht VAT
                txtGrandTotal.Text = grandTotalWithVAT.ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            //Get the Paid amount and grand total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);
            decimal returnAmount = paidAmount - grandTotal;

            //Diplay ther retunr amount as well
            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Get the valur from purchase and sales form
            transactionBLL transaction = new transactionBLL();

            transaction.type = lblTop.Text;

            // get the ID if the dealer and customer here
            //Lest get the name of th deal or customer first
            string deaCustName = txtName.Text;
            DeaCustBLL dc = dcDal.GetDeaCustIDFromName(deaCustName);

            transaction.dea_cust_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text),2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            //Get the uername of logged in user
            string username = frmLogin.loggedIn;
            userBLL u = uDAL.GetIDFromUsername(username);

            transaction.added_by = u.id;
            transaction.transactionDetails = tranactionDT;
            //Lets creat Boolean Vairable and set its value to false
            bool success = false;
            //actua; code to insert transacr=tion details
            using (TransactionScope scope = new TransactionScope())
            {
                int transactionID = -1;

                //Create a boolean value and insert transaction
                bool w = tDAL.Insert_Transaction(transaction, out transactionID);

                //User for looop to insert TraNACTION DETAILS
                for (int i = 0; i < tranactionDT.Rows.Count; i++)
                {
                    //Get all the deatials of the products
                    transcationDetailsBLL transactionDetails = new transcationDetailsBLL();

                    //get the product name and convert it to id
                    string ProductName = tranactionDT.Rows[i][0].ToString();

                    productsBLL p = pDAL.GetProductsForTransaction(ProductName);

                    transactionDetails.product_id = p.id;
                    transactionDetails.rate = decimal.Parse(tranactionDT.Rows[i][1].ToString());
                    transactionDetails.qty = decimal.Parse(tranactionDT.Rows[i][2].ToString());
                    transactionDetails.total = Math.Round(decimal.Parse(tranactionDT.Rows[i][3].ToString()), 2);
                    transactionDetails.dea_cust_id = dc.id;
                    transactionDetails.added_date = DateTime.Now;
                    transactionDetails.added_by = u.id;


                    //Here Increas Increase and Prdocut Quantity based on purchase or sales

                    string transactionType = lblTop.Text;

                    //Lest Check whter we are on purchase or sales
                    bool x = false;
                    if (transactionType == "Purschase") 
                    {
                        //Increase the Product
                         x = pDAL.IncreaseProudct(transactionDetails.product_id, transactionDetails.qty);

                    }
                    else if(transactionType =="Sales")

                    {
                        //Decrease the product Qauntity
                        x = pDAL.DecreaseProduct(transactionDetails.product_id, transactionDetails.qty);
                    }

                    //Insert Tranacation details the Databses
                    bool y = tdDAL.Insert_TransactionDetail(transactionDetails);
                    success = w && x && y;

                }

                if (success == true)

                {
                    //transction Complete
                    MessageBox.Show("Tranaction Completed successfully");
                    scope.Complete();

                    //Clear the data grid view and clear all th texboxes
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();

                    txtSearch.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtSearchProduct.Text = "";
                    txtProductName.Text = "0";
                    txtInventory.Text = "0";
                    txtRate.Text = "0";
                    txtQty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";

                }
                else
                {
                    //trancation is failed
                    MessageBox.Show("Tranaction Unsuccessfully");
                }




            }
        }
    }
}