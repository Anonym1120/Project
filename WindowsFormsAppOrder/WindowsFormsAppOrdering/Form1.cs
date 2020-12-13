using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsAppOrdering
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-QC55GV4\SQLEXPRESS;Initial Catalog=myDB;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tSQL = "Select * From [User] Where Username = '" + txtUsername.Text.Trim() + "' and Password = '" + txtPassword.Text.Trim() + "'";
            SqlDataAdapter sqa = new SqlDataAdapter(tSQL, con);
            DataTable dt = new DataTable();
            sqa.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0][3].ToString() == "1")
                {
                    GlobalVar.name = dt.Rows[0][1].ToString();
                    GlobalVar.user = "管理者";
                    GlobalVar.level = dt.Rows[0][3].ToString();
                }
                else if (dt.Rows[0][3].ToString() == "2")
                {
                    GlobalVar.name = dt.Rows[0][1].ToString();
                    GlobalVar.user = "值日生";
                    GlobalVar.level = dt.Rows[0][3].ToString();
                }
                else if (dt.Rows[0][3].ToString() == "3")
                {
                    GlobalVar.name = dt.Rows[0][1].ToString();
                    GlobalVar.user ="學生";
                    GlobalVar.level = dt.Rows[0][3].ToString();
                }

                Form2 form2 = new Form2();
                form2.ShowDialog();

            }
            else
            {
                MessageBox.Show("請重新輸入帳號密碼");
            }
        }
    }
}
