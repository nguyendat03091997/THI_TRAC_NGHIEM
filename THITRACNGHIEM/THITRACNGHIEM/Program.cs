﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using System.Data;
using System.Data.SqlClient;

// form
using THITRACNGHIEM.form;

namespace THITRACNGHIEM
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static String rootSeverName = "Data Source=DAT_KOOL;Initial Catalog=TN_CSDLPT;Persist Security Info=True;User ID=sa;Password=123456";
        public static SqlConnection conn = new SqlConnection();
        public static String connstr;
        public static SqlDataReader myReader;
        public static String servername = "";
        public static String mUserId = "";
        public static String mlogin = "";
        public static String password = "";
        public static String malop = "";
        public static String tenlop = "";

        // LOGIN SINHVIEN
        public static String svLogin = "SVLOGIN";
        public static String svPassword = "123456";


        public static String database = "TN_CSDLPT";
        public static String remotelogin = "HTKN"; //HTKN 
        public static String remotepassword = "123456";
        public static String mloginDN = "";
        public static String passwordDN = "";
        public static String mNhom = "";
        public static String mHoTen = "";
        public static String mLoai = ""; // giangvien or sinhvien
        public static int mChiNhanh = 0;

        public static BindingSource bds_dspm = new BindingSource();  // giữ bdsPM khi đăng nhập
        public static String Control = "";

        // form
        public static frmAccess fAccess;
        public static frmMain fMain;
        public static frmLogin fLogin;

        public static int KetNoi()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.connstr = "Data Source=" + Program.servername + ";Initial Catalog=" +
                      Program.database + ";User ID=" +
                      Program.mlogin + ";password=" + Program.password;

                Program.conn.ConnectionString = Program.connstr;
                Program.conn.Open();
                return 1;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + ex.Message + Program.connstr, "", MessageBoxButtons.OK);
                return 0;
            }
        }
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader(); return myreader;


            }
            catch (SqlException ex)
            {
                Program.conn.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static DataTable ExecSqlDataTable(String cmd)
        {
            DataTable dt = new DataTable();
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public static int execStoreProcedureWithReturnValue(SqlCommand sqlcmd)
        {
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlParameter retval = sqlcmd.Parameters.Add("@return_value", SqlDbType.Int);
            retval.Direction = ParameterDirection.ReturnValue;
            try { sqlcmd.ExecuteNonQuery(); }
            catch (Exception) { }
            string tmp = sqlcmd.Parameters["@return_value"].Value.ToString();
            if (tmp == null)
            {
                MessageBox.Show("Lỗi lập trình! Sp hoặc đối số của sp có vấn đề!");
            }
            return int.Parse(tmp);

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();

            Program.fAccess = new frmAccess();
            Program.fMain = new frmMain();
            Program.fLogin = new frmLogin();

            Application.Run(Program.fAccess);
        }
    }
}
