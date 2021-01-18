using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentDetails.ClientApp
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dgStudDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgStudDetails.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgStudDetails.SelectedRows) 
                {
                    txtName.Text = row.Cells[0].Value.ToString();
                    txtLastName.Text = row.Cells[1].Value.ToString();
                    txtCellNumber.Text = row.Cells[2].Value.ToString();
                    txtEmailAddress.Text = row.Cells[3].Value.ToString();
                    dtpDOB.Value = DateTime.ParseExact(row.Cells[4].Value.ToString(), "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                    cmbGender.SelectedItem = row.Cells[5].Value.ToString();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtCellNumber.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            dtpDOB.Value = DateTime.Now;
            cmbGender.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
