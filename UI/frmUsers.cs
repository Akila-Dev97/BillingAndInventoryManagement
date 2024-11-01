using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }
        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {   //Getting username of the logged in user
            string loggedUser = frmLogin.loggedIn;

            //getting data from UI
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;

            //Getting Y=USernmae of the logged in user

            userBLL usr = dal.GetIDFromUsername(loggedUser);
            u.added_by = usr.id;


            //Inserting data into Database
            bool success = dal.Insert(u);
            // If data is succussfully inserted then the value of success will be true else it will be false

            if (success == true)
            {

                //Data Succesfully Inserted
                MessageBox.Show("User successfully created. ");
                clear();
            }
            else
            {
                // Failed to insert Data
                MessageBox.Show("Failed to add new User. ");
            }
            //Refrshing Data Grid view

            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {

            userBLL updatedUser = new userBLL();

            //Get the values from User UI
            updatedUser.id = Convert.ToInt32(txtUserID.Text);
            updatedUser.first_name = txtFirstName.Text;
            updatedUser.last_name = txtLastName.Text;
            updatedUser.email = txtEmail.Text;
            updatedUser.username = txtUsername.Text;
            updatedUser.password = txtPassword.Text;
            updatedUser.contact = txtContact.Text;
            updatedUser.address = txtAddress.Text;
            updatedUser.gender = cmbGender.Text;
            updatedUser.user_type = cmbUserType.Text;
            updatedUser.added_date = DateTime.Now;
            updatedUser.added_by = 1;

            //Updating Data into DataBase

            bool success = dal.Update(updatedUser);
            //IF data is updated succesfully the value of success will be ttru else it will be false
            if (success == true)

            {
                //Data Updated Successfully
                MessageBox.Show("User succussfully updated");
                clear();
            }
            else
            {
                //failed to pdate user
                MessageBox.Show("Failed to update user");
            }
            // Refreshing  data Gird view

            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }
        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }
        private void clear()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";
        }
        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get the index of partcular row
            int rowindex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[rowindex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowindex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowindex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowindex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[rowindex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowindex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowindex].Cells[6].Value.ToString();
            cmbGender.Text = dgvUsers.Rows[rowindex].Cells[7].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowindex].Cells[8].Value.ToString();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //getting user data from form
            u.id = Convert.ToInt32(txtUserID.Text);

            bool success = dal.Delete(u);

            //IF DATA IS DELETED THEN THE VALUE OF SUCCESS WILLL BE TRU EKSE IT WILL BE FALSE

            if (success == true)
            {
                //User Deleted Succefully
                MessageBox.Show("User deleted successfully ");

            }
            else

            {//failed to delete User
                MessageBox.Show("Failed to delete user");
            }
            
            //refrshing Data grid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //GEt Keyowrd From Text Box
            string keywords = txtSearch.Text;

            //Chec if the keywords has value or not
            if(keywords != null)
            { 
                // Showe user bases on keywords
                 DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                DataTable dt = dal.Select();
                dgvUsers.DataSource= dt;
            }
        }
    }

}
