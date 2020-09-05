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
    public partial class frmLogin : Form
    {
        public Boolean isTeacher;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tN_CSDLPTDataSet.V_DS_PHANMANH' table. You can move, or remove it, as needed.
            this.v_DS_PHANMANHTableAdapter.Fill(this.tN_CSDLPTDataSet.V_DS_PHANMANH);
            tENCNComboBox.SelectedIndex = 0;
            Program.servername = tENCNComboBox.SelectedValue.ToString();

            // clear data
            tbUsername.Text = "";
            tbPassword.Text = "";

            if (isTeacher)
            {
                pnlPassword.Show();
            }
            else
            {
                pnlPassword.Hide();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        // DANG NHAP
        private void button1_Click(object sender, EventArgs e)
        {
            if (isValidate() == false)
            {
                return;
            }

            // pass validate

            String userName = tbUsername.Text.Trim();
            String password = tbPassword.Text.Trim();

            if (isTeacher)
            {
                Program.mlogin = userName;
                Program.password = password;
            }
            else // Sinh Vien
            {
                Program.mlogin = Program.svLogin;
                Program.password = Program.svPassword;
            }

            if (Program.KetNoi() == 0)
            {
                return;
            }

            // Kết nối thành công, tiến hành gọi sp đăng nhập

            String connectionStr = "EXEC dbo.SP_DANGNHAP '" + Program.mlogin + "'" + ",'" + userName + "'";
            Program.myReader = Program.ExecSqlDataReader(connectionStr);

            if (Program.myReader == null)
            {
                return;
            }

            Program.myReader.Read();

            // đăng nhập thành công
            Program.mUserId = Program.myReader.GetString(0);
            Program.mHoTen = Program.myReader.GetString(1);
            Program.mNhom = Program.myReader.GetString(2);

            Console.WriteLine("UserName : " + Program.mUserId);
            Console.WriteLine("Ho ten : " + Program.mHoTen);
            Console.WriteLine("Nhom : " + Program.mNhom);

            if (isTeacher)
            {
                Program.fMain.Show();
                Program.fLogin.Hide();
            }
            else // Sinh Viên
            {

            }
        }

        public Boolean isValidate()
        {
            String userName = tbUsername.Text.Trim();
            String password = tbPassword.Text.Trim();

            if (userName == "")
            {
                MessageBox.Show(this, "Tên đăng nhập không được để trống", "THÔNG BÁO LỖI", MessageBoxButtons.OK);
                return false;
            }

            if (isTeacher && password == "")
            {
                MessageBox.Show(this, "Mật khẩu không được để trống", "THÔNG BÁO LỖI", MessageBoxButtons.OK);
                return false;
            }
            
            return true;
        }

        private void tENCNComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tENCNComboBox.SelectedValue == null)
            {
                return;
            }
            Program.servername = tENCNComboBox.SelectedValue.ToString();
            Console.WriteLine(Program.servername);
        }
    }
}
