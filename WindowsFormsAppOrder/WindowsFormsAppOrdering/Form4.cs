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
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-QC55GV4\SQLEXPRESS;Initial Catalog=myDB;Integrated Security=True");

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "")
            {
                con.Open();
                string tSQL = "Select * From [User] Where [Username] like @SearchName";
                SqlCommand cmd = new SqlCommand(tSQL, con);
                cmd.Parameters.AddWithValue("@SearchName", "%" + txtUsername.Text + "%");
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtId.Text = string.Format("{0}", (int)reader["Id"]);
                    txtUsername.Text = string.Format("{0}", (string)reader["Username"]);
                    txtPassword.Text = string.Format("{0}", (string)reader["Password"]);
                    txtLevel.Text = string.Format("{0}", (int)reader["Level"]);
                }
                else 
                {
                    MessageBox.Show("查無此帳號");
                    txtId.Text = "";
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtLevel.Text = "";
                }

                reader.Close();
                con.Close();

            }
            else
            {
                MessageBox.Show("請輸入帳號來搜尋");
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if ((txtUsername.Text !="") && (txtLevel.Text != ""))
            {
                con.Open();
                string tSQL = "Insert Into [User] Values(@NewUsername,@NewPassword,@NewLevel)";
                SqlCommand cmd = new SqlCommand(tSQL, con);
                cmd.Parameters.AddWithValue("@NewUsername", txtUsername.Text);
                cmd.Parameters.AddWithValue("@NewPassword", txtPassword.Text);

                int intLevel = 0;
                Int32.TryParse(txtLevel.Text, out intLevel);
                cmd.Parameters.AddWithValue("@NewLevel", intLevel);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("新增成功");
            }
            else 
            {
                MessageBox.Show("請輸入帳號與職等");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int intID = 0;
            Int32.TryParse(txtId.Text, out intID);
            if (intID > 0)
            {
                con.Open();
                string tSQL = "Update [User] set [Username] = @NewUsername, [Password]= @NewPassword, [Level] = @NewLevel where [Id] = @serchID";
                SqlCommand cmd = new SqlCommand(tSQL, con);
                cmd.Parameters.AddWithValue("serchID", intID);
                cmd.Parameters.AddWithValue("@NewUsername", txtUsername.Text);
                cmd.Parameters.AddWithValue("@NewPassword", txtPassword.Text);

                int intLevel = 0;
                Int32.TryParse(txtLevel.Text, out intLevel);
                cmd.Parameters.AddWithValue("@NewLevel", intLevel);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("修改成功");
            }
            else
            {
                //序號有誤
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int intID = 0;
            Int32.TryParse(txtId.Text, out intID);

            if (intID > 0)
            {
                con.Open();
                string tSQL = "Delete From [User] Where [Id] = @SearchID";
                SqlCommand cmd = new SqlCommand(tSQL, con);
                cmd.Parameters.AddWithValue("@SearchID", intID);
                cmd.ExecuteNonQuery();
                con.Close();

                txtId.Text = "";
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtLevel.Text = "";

                MessageBox.Show("刪除成功");
            }
            else
            {
                //無此序號
            }

        }
    }
}
