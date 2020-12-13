using System;
using System.Collections;
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
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-QC55GV4\SQLEXPRESS;Initial Catalog=myDB;Integrated Security=True");

        List<string> ListGrade = new List<string>();
        List<string> ListRestaurant = new List<string>();
        string restaurant = "";
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            txtGrade.Text = GlobalVar.grade;
            txtOrderer.Text = GlobalVar.name;
            lblUser.Text = GlobalVar.user;

            ListRestaurant.Add("米寶便當");
            ListRestaurant.Add("極品拉麵");

            foreach (string item in ListRestaurant)
            {
                cboxRestaurant.Items.Add(item);
            }
            cboxRestaurant.SelectedIndex = 0;
            restaurant = ListRestaurant[0];

            if ((GlobalVar.AmountList1.Count > 0) || (GlobalVar.AmountList2.Count > 0))
            {
                lboxAmount.Items.Clear();

                foreach (ArrayList myorder in GlobalVar.AmountList1)
                {
                    //string grade = (string)myorder[0];
                    //string name = (string)myorder[1];
                    string restaurant = (string)myorder[0];
                    string item1 = (string)myorder[1];
                    //int unitprice1 = (int)myorder[4];
                    int count = (int)myorder[2];
                    //string TotalPrice = (string)myorder[6];
                    //string time = (string)myorder[7];
                    string strMyorder1 = string.Format("{0} {1} {2}個", restaurant, item1, count);
                    lboxAmount.Items.Add(strMyorder1);
                }

                foreach (ArrayList myorder in GlobalVar.AmountList2)
                {
                    //string grade = (string)myorder[0];
                    //string name = (string)myorder[1];
                    string restaurant = (string)myorder[0];
                    string item2 = (string)myorder[1];
                    //int unitprice2 = (int)myorder[4];
                    int count = (int)myorder[2];
                    //string TotalPrice = (string)myorder[6];
                    //string time = (string)myorder[7];
                    string strMyorder2 = string.Format("{0} {1} {2}個", restaurant, item2, count);
                    lboxAmount.Items.Add(strMyorder2);
                }
            }
        }

        private void cboxRestaurant_SelectedIndexChanged(object sender, EventArgs e)
        {
            lboxOrder.Items.Clear();

            if ((GlobalVar.orderList1.Count > 0) || (GlobalVar.orderList2.Count > 0))
            {
                if (cboxRestaurant.SelectedIndex == 0)
                {
                    foreach (ArrayList myorder in GlobalVar.orderList1) 
                    {
                        //string grade = (string)myorder[0];
                        //string name = (string)myorder[1];
                        //string restaurant = (string)myorder[0];
                        string item1 = (string)myorder[0];
                        int unitprice1 = (int)myorder[2];
                        int count = (int)myorder[1];
                        //string TotalPrice = (string)myorder[6];
                        //string time = (string)myorder[7];
                        string strMyorder1 = string.Format("{0} {1}個 單價{2}元", item1, count, unitprice1);
                        lboxOrder.Items.Add(strMyorder1);
                    }
                }
                else if (cboxRestaurant.SelectedIndex == 1)
                {
                    foreach (ArrayList myorder in GlobalVar.orderList2)
                    {
                        //string grade = (string)myorder[0];
                        //string name = (string)myorder[1];
                        //string restaurant = (string)myorder[0];
                        string item2 = (string)myorder[0];
                        int unitprice2 = (int)myorder[2];
                        int count = (int)myorder[1];
                        //string TotalPrice = (string)myorder[6];
                        //string time = (string)myorder[7];
                        string strMyorder2 = string.Format("{0} {1}個 單價{2}元", item2, count, unitprice2);
                        lboxOrder.Items.Add(strMyorder2);
                    }
                }
                Num1();
            }
        }

        int Num1()
        {
            int num = 0;
            int num1 = 0;
            int num2 = 0;
            foreach (ArrayList myContent in GlobalVar.orderList1)
            {
                num1 += (int)myContent[1] * (int)myContent[2];
            }
            foreach (ArrayList myContent2 in GlobalVar.orderList2)
            {
                num2 += (int)myContent2[1] * (int)myContent2[2];
            }
            num += num1 + num2;
            lblTotalPrice.Text = num.ToString();
            return num;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            GlobalVar.orderList1.Clear();
            GlobalVar.orderList2.Clear();
            lboxOrder.Items.Clear();

            GlobalVar.AmountList1.Clear();
            GlobalVar.AmountList2.Clear();
            lboxAmount.Items.Clear();

            GlobalVar.Save1.Clear();
            GlobalVar.Save2.Clear();

            Num1();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GlobalVar.Save1.Count > 0)
            {
                foreach (ArrayList mydb in GlobalVar.Save1)
                {
                    string grade = (string)mydb[0];
                    string orderer = (string)mydb[1];
                    string restaurant = (string)mydb[2];
                    string item1 = (string)mydb[3];
                    int unitprice1 = (int)mydb[4];
                    int count = (int)mydb[5];
                    string TotalPrice = (string)mydb[6];
                    string time = (string)mydb[7];

                    con.Open();
                    string tSQL = "insert into [OrderDetail] values(@Grade, @Customer, @Restaurant, @OrderItems, @UnitPrice, @Quantity, @TotalPrice, @OrderTime)";
                    SqlCommand cmd = new SqlCommand(tSQL, con);
                    cmd.Parameters.AddWithValue("@Grade", grade);
                    cmd.Parameters.AddWithValue("@Customer", orderer);
                    cmd.Parameters.AddWithValue("@Restaurant", restaurant);
                    cmd.Parameters.AddWithValue("@OrderItems", item1);
                    cmd.Parameters.AddWithValue("@UnitPrice", unitprice1);
                    cmd.Parameters.AddWithValue("@Quantity", count);
                    cmd.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                    cmd.Parameters.AddWithValue("@OrderTime", time);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show("訂單已送出");

                }
            }
            else if (GlobalVar.Save2.Count > 0) 
            {
                foreach (ArrayList mydb in GlobalVar.Save2) 
                {
                    string grade = (string)mydb[0];
                    string orderer = (string)mydb[1];
                    string restaurant = (string)mydb[2];
                    string item2 = (string)mydb[3];
                    int unitprice2 = (int)mydb[4];
                    int count = (int)mydb[5];
                    string TotalPrice = (string)mydb[6];
                    string time = (string)mydb[7];

                    con.Open();
                    string tSQL = "insert into [OrderDetail] values(@Grade, @Customer, @Restaurant, @OrderItems, @UnitPrice, @Quantity, @TotalPrice, @OrderTime)";
                    SqlCommand cmd = new SqlCommand(tSQL, con);
                    cmd.Parameters.AddWithValue("@Grade", grade);
                    cmd.Parameters.AddWithValue("@Customer", orderer);
                    cmd.Parameters.AddWithValue("@Restaurant", restaurant);
                    cmd.Parameters.AddWithValue("@OrderItems", item2);
                    cmd.Parameters.AddWithValue("@UnitPrice", unitprice2);
                    cmd.Parameters.AddWithValue("@Quantity", count);
                    cmd.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                    cmd.Parameters.AddWithValue("@OrderTime", time);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show("訂單已送出");
                    

                }
            }
        }
    }
}
