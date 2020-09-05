using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THITRACNGHIEM.form
{
    public partial class frmAccess : Form
    {
        public frmAccess()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {

            if (cbbAccess.SelectedIndex == 0)
            {
                Program.fLogin.isTeacher = true;
            }
            else if(cbbAccess.SelectedIndex == 1)
            {
                Program.fLogin.isTeacher = false;
            }

            Program.fLogin.Show();
            this.Hide();
        }

        private void frmAccess_Load(object sender, EventArgs e)
        {
            cbbAccess.Items.Add("Giáo Viên");
            cbbAccess.Items.Add("Sinh Viên");
            cbbAccess.SelectedIndex = 0;
        }
    }
}
