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
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
            InitializeComponent();
        }
        CategoriesDAL cdal = new CategoriesDAL();
        productsDAL pdal = new productsDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            //diplay all the transction

            DataTable cDt = cdal.Select();

            cmbCategories.DataSource = cDt;

            //Give thte Value member and display member for combox

            cmbCategories.DisplayMember ="title";
            cmbCategories.ValueMember = "title";

            //Disaplay alle thr pdicuts in data grid view when from is laoded
            DataTable pdt = pdal.Select();
            dgvProducts.DataSource = pdt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            DataTable pdt = pdal.Select();
            dgvProducts.DataSource= pdt;
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = cmbCategories.Text;
            DataTable pdt = pdal.DisplayProductsByCategory(category);
            dgvProducts.DataSource= pdt;
        }
    }
}
