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
        DataTable tranactionDT = new DataTable();

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
            if(keyword =="")
            {
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                TxtQty.Text = "";
                return ;
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
            string ProdcutName = txtProductName.Text;
            decimal Rate =  decimal.Parse(txtRate.Text); 
            decimal Qty = decimal.Parse(TxtQty.Text);

            decimal Total = Rate * Qty; //Total = Rate x Qty

            //Check wether thje product is scaled or not
            if(ProductName == "")
            {
                //Diplay error Message
                MessageBox.Show("Select the product first. Try again");

            }
            else
            {
                //add proidct to the Data Grid View 
                tranactionDT.Rows.Add(ProductName,Rate,Qty,Total);

                //Show Data in dat grid
                dgvAddedProducts.DataSource = tranactionDT;
            }
        }
    }
}