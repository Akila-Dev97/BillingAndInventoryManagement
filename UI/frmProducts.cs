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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the vlaues from Catergory Form
            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;

            //Getting ID by Added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            //Passign the id of logged in user in added by field
            p.added_by = usr.id;

            //Creatig boolean variables to update categories and check
            bool success = pdal.Update(p);
            //If the catergory updatesd succesfully then the value of success will be true else it will be false
            if (success == true)
            {

                //Catergory updated Succesfully
                MessageBox.Show("Category Updated Successfully");
                Clear();

                //Refreshing Data grid View
                DataTable dt = new DataTable();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to update category
                MessageBox.Show("Failed to Update Catergory");
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //ADD code to hide this form
            this.Hide();
        }

        CategoriesDAL cdal = new CategoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
            //creating data table to hold the categroies from database
            DataTable categoriesDT = cdal.Select();
            //Specify datasource for atergory comboBox
            cmbCategory.DataSource = categoriesDT;
            //Specify Display and Value member for comboBox
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember= "title";

            //Load all the products in data Grid view
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get all the va;lue from Product Form
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;

            //Getting User name of logged users
            String loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            p.added_by = usr.id;

            //Creat Boolean vairable to chkc if the product is added succsfully or not
            bool success = pdal.Insert(p);
            //If the product is added succussfully then the value of success will bbe tru rlse it will be false
            if (success == true)
            {
                //Product inseted succssfully
                MessageBox.Show("Product added Successully");
                //Calling th Clear mehod
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //failed to add new product
                MessageBox.Show("Failed to add new Product");
            }
        }
        public void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Findiong the row Index of thew Row clicked on Data Grid View
            int RowIndex = e.RowIndex;

            txtID.Text = dgvProducts.Rows[RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[RowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[RowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[RowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[RowIndex].Cells[4].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the ID of the Product to be deleted
            p.id = int.Parse(txtID.Text);
        

            //Crewating Boolean variable to dlete the ctaerory
            bool success = pdal.Delete(p);

            //If the catergory id Deleted Succesfully then the value of success will be true else it will be false
            if (success == true)
            {

                //txtCatergoryID Deleted succusfully
                MessageBox.Show("Product Deleted Succesfully");
                Clear();
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Delete Category
                MessageBox.Show("Failed to Delete Category");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the keyword from form
            string keywords = txtSearch.Text;

            if(keywords != null)
            {
                //Search the Products
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource= dt;
            }
            else
            {
                //Display all the Products
                DataTable dt =pdal.Select();
                dgvProducts.DataSource = dt;
            }
        }
    }
}
