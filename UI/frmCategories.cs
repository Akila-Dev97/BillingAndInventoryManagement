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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        CategoriesBLL c = new CategoriesBLL();
        CategoriesDAL dal = new CategoriesDAL();
        userDAL udal = new userDAL();

        private void btnADD_Click(object sender, EventArgs e)
        {
            //Get the value from catergory form
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;


            //Getting ID by Added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            //Passign the id of logged in user in added by field
            c.added_by = usr.id;

            //Creating Boolean Method to insert data into Database
            bool success = dal.Insert(c);

            //If the ctargory is inserted usccesfully thhen th evalue of the success will be true else it will be false

            if (success == true)
            {
                //new Catergory Inserted Succesfully
                MessageBox.Show("New catergory Inserted Succesfully.");
                Clear();
                //refrshing DataViewsGrid
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to insert new catergory.");
            }
        }

        public void Clear()
        {
            txtCatergoryID.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
        }


        private void frmCategories_Load(object sender, EventArgs e)
        {
            //Here write the code to display all the categories
            DataTable dt = dal.Select();
            dgvCategories.DataSource = dt;
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Findiong the row Index of thew Row clicked on Data Grid View
            int RowIndex = e.RowIndex;

            txtCatergoryID.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the vlaues from Catergory Form
            c.id = int.Parse(txtCatergoryID.Text);
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            //Getting ID by Added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);

            //Passign the id of logged in user in added by field
            c.added_by = usr.id;

            //Creatig boolean variables to update categories and check
            bool success = dal.Update(c);
            //If the catergory updatesd succesfully then the value of success will be true else it will be false
            if (success == true)
            {
                
                //Catergory updated Succesfully
                MessageBox.Show("Category Updated Successfully");
                Clear();

                //Refreshing Data grid View
                DataTable dt = new DataTable();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Failed to update category
                MessageBox.Show("Failed to Update Catergory");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Get the ID of the Catergory Wiht me Want to Delete

            c.id = int.Parse(txtCatergoryID.Text);

            //Crewating Boolean variable to dlete the ctaerory
            bool success = dal.Delete(c);

            //If the catergory id Deleted Succesfully then the value of success will be true else it will be false
            if(success == true)
            {

                //txtCatergoryID Deleted succusfully
                MessageBox.Show("Category Deleted Succesfully");
                Clear();
                DataTable dt = new DataTable();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Failed to Delete Category
                MessageBox.Show("Failed to Delete Category");
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the keywords 
            string keywords = txtSearch.Text;

            //Fill the method to dsplay on keyowrds 
            if(keywords != null)
            {
                //use search Methoed to dispoay catgories
                DataTable dt = dal.Search(keywords);
                dgvCategories.DataSource = dt;

            }
            else
            {
                //Use select Method to disppay All charges
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }

        }
    }
}