using AnyStore.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore
{
    public partial class frmUserDashboard : Form
    {
        public frmUserDashboard()
        {
            InitializeComponent();
        }
        // Set a Publct statci methoed to specify where the form is purschse or sales
        public static string transactionType;

        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            lblUserLoggedIn.Text = frmLogin.loggedIn;
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set the vlaue of trnaction type methoed to purchase
            transactionType = "Purchase";

            frmPurchaseandSales purchase = new frmPurchaseandSales();
            purchase.Show();

           
        }

        private void salesFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set the vlaue of trnaction type methoed to sales
            transactionType = "Sales";

            frmPurchaseandSales purchase = new frmPurchaseandSales();
            purchase.Show();
            
        }

        private void menuStripTop_Click(object sender, EventArgs e)
        {
            frmInventory invnetory = new frmInventory();
            invnetory.Show();
        }
    }
}
