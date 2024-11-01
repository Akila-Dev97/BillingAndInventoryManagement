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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pBoxClose_Click(object sender, EventArgs e)
        {
            //Code close this form
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUsername.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = comboBox1.Text.Trim();

            //Checking the Lgin credintails
            bool success = dal.loginCheck(l);
            if (success == true)
            {
                MessageBox.Show(" Login Succesfull.");
                loggedIn = l.username;
                //Need to open Respective forms based on User Type

                switch(l.user_type) 
                {
                    case "Admin":
                        {
                            //Diplay Admin Dashboard
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        } 
                        break;

                    case "User":
                        {
                            //Display User Dashboard
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();   
                        }
                        break;

                    default:
                        {
                            //Display any error n=message
                            MessageBox.Show("Invalid User Type.");
                        }
                        break ;
                }
            }
            else
            {
                //Login failed
                MessageBox.Show(" Login Failed. Try again.");
            }
        }
    }
}
