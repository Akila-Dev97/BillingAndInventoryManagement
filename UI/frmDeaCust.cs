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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //close button for the Dea and Cut Module
            this.Hide();
        }

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();

        userDAL uDal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get all the va;lue from Product Form
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            //Getting User name of logged users
            String loggedUser = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUser);
            dc.added_by = usr.id;

            //Creat Boolean vairable to chkc if the product is added succsfully or not
            bool success = dcDal.Insert(dc);
            //If the product is added succussfully then the value of success will bbe tru rlse it will be false
            if (success == true)
            {
                //Product inseted succssfully
                MessageBox.Show("Dealer or Customer added Succfully");
                //Calling th Clear mehod
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //failed to add new product
                MessageBox.Show("Failed to add new Product");
            }
        }
        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            //Refreshing Dat Frid view
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource= dt;
        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //Findiong the row Index of thew Row clicked on Data Grid View
            int RowIndex = e.RowIndex;

            txtDeaCustID.Text = dgvDeaCust.Rows[RowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[RowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[RowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[RowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[RowIndex].Cells[5].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the vlaues from Form
            dc.id = int.Parse(txtDeaCustID.Text);
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            //Getting ID by to logged in user and pasign its vlaue in dealer or customer module
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUser);

            //Passign the id of logged in user in added by field
            dc.added_by = usr.id;

            //Creatig boolean variables to update dealer or customer
            bool success = dcDal.Update(dc);
            //If the catergory updatesd succesfully then the value of success will be true else it will be false
            if (success == true)
            {

                //dealer or customer updated Succesfully
                MessageBox.Show("dealer or custmoer Updated Successfully");
                Clear();

                //Refreshing Data grid View
                DataTable dt = new DataTable();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Failed to update dealer or customer
                MessageBox.Show("Failed to Update dealer or customer");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the ID of dealer or customer to be deleted
            dc.id = int.Parse(txtDeaCustID.Text);


            //Crewating Boolean variable to dlete the dealer or customer
            bool success = dcDal.Delete(dc);

            //If the dealer or customer id Deleted Succesfully then the value of success will be true else it will be false
            if (success == true)
            {

                //dealer or customer ID Deleted succusfully
                MessageBox.Show("dealer or customert Deleted Succesfully");
                Clear();
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Failed to Delete dealer or customer
                MessageBox.Show("Failed to Delete dealer or customerCategory");
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the keyword from form
            string keyword = txtSearch.Text;

            if (keyword != null)
            {
                //Search the dealer and customer
                DataTable dt = dcDal.Search(keyword);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Display all the dealer and customer
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
        }
    }
}
