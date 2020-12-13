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
using System.IO;

namespace WindowsFormsAppOrdering
{
    public partial class Form2 : Form
    {
        List<string> ListGrade = new List<string>(); // 班級
        List<string> ListRestaurant = new List<string>(); // 餐廳
        List<string> ListItems1 = new List<string>(); // 餐廳1的菜單
        List<string> ListItems2 = new List<string>(); // 餐廳2的菜單
        List<int> ListPrice1 = new List<int>(); // 餐廳1菜單的價格
        List<int> ListPrice2 = new List<int>(); // 餐廳2菜單的價格
        string grade = "";
        string restaurant = ""; // 餐廳
        int count = 0; // 數量
        int unitprice1 = 0, unitprice2 = 0; // 餐廳1價格 , 餐廳2價格
        string item1 = "", item2 = ""; // 餐廳1項目 , 餐廳2項目

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtOrderer.Text = GlobalVar.name;
            lblUser.Text = GlobalVar.user;
            lblTime.Text = DateTime.Now.ToString("MM/dd/yyyy");

            //權限管理 Authority 職等 
            if (GlobalVar.level == "1")
            {
                // admin
            }
            else if (GlobalVar.level == "2")
            {
                // dutyStudent

                btnAdmin.Visible = false;
            }
            else if (GlobalVar.level == "3")
            {
                btnAdmin.Visible = false;
                btnOpen.Enabled = false;
            }

            //新增班級            
            ListGrade.Add("101");
            ListGrade.Add("102");
            ListGrade.Add("103");

            foreach (string item in ListGrade)
            {
                cboxGrade.Items.Add(item);
            }
            cboxGrade.SelectedIndex = 0;
            grade = ListGrade[0];

            //新增店家            
            ListRestaurant.Add("米寶便當");
            ListRestaurant.Add("極品拉麵");

            foreach (string item in ListRestaurant)
            {
                cboxRestaurant.Items.Add(item);
            }
            cboxRestaurant.SelectedIndex = 0;
            restaurant = ListRestaurant[0];

            //新增餐廳1的菜單
            ListItems1.Add("黃金排骨便當");
            ListItems1.Add("正宗池上便當");
            ListItems1.Add("紅豆養生便當");
            ListItems1.Add("糙米高纖便當");
            ListItems1.Add("懷舊排骨便當");

            foreach (string item in ListItems1)
            {
                lboxItems.Items.Add(item);
            }

            //新增餐廳2的菜單
            ListItems2.Add("豚骨拉麵");
            ListItems2.Add("叉燒拉麵");
            ListItems2.Add("味增拉麵");
            ListItems2.Add("牛肉拉麵");
            ListItems2.Add("海鮮拉麵");

            //新增價格
            int[] pr1 = { 85, 90, 95, 100, 105 };
            ListPrice1.AddRange(pr1);

            int[] pr2 = { 85, 90, 75, 100, 85 };
            ListPrice2.AddRange(pr2);

            count = 1;
            txtCount.Text = count.ToString();
        }

        private void cboxGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVar.grade = ListGrade[cboxGrade.SelectedIndex];
        }

        private void cboxRestaurant_SelectedIndexChanged(object sender, EventArgs e)
        {
            lboxItems.Items.Clear();

            if (cboxRestaurant.SelectedIndex == 0)
            {
                foreach (string item in ListItems1)
                {
                    lboxItems.Items.Add(item);
                }

            }
            else if (cboxRestaurant.SelectedIndex == 1)
            {
                foreach (string item in ListItems2)
                {
                    lboxItems.Items.Add(item);
                }
            }

            restaurant = ListRestaurant[cboxRestaurant.SelectedIndex];
        }

        private void lboxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxRestaurant.SelectedIndex == 0)
            {
                item1 = ListItems1[lboxItems.SelectedIndex];
                unitprice1 = ListPrice1[lboxItems.SelectedIndex];
                lblUnitPrice.Text = unitprice1.ToString();
                lblTotalPrice.Text = (unitprice1 * count).ToString();

                pictureBox1.Image = imageList1.Images[lboxItems.SelectedIndex];

            }
            else if (cboxRestaurant.SelectedIndex == 1)
            {
                item2 = ListItems2[lboxItems.SelectedIndex];
                unitprice2 = ListPrice2[lboxItems.SelectedIndex];
                lblUnitPrice.Text = unitprice2.ToString();
                lblTotalPrice.Text = (unitprice2 * count).ToString();

                pictureBox1.Image = imageList2.Images[lboxItems.SelectedIndex];
            }

        }

        private void txtCount_TextChanged(object sender, EventArgs e)
        {
            if (txtCount.Text != "")
            {
                bool num = System.Int32.TryParse(txtCount.Text, out count);

                if ((num == true) && (count >= 1))
                {

                }
                else
                {
                    MessageBox.Show("請重新輸入數量");
                    count = 1;
                    txtCount.Text = count.ToString();
                }

                if (cboxRestaurant.SelectedIndex == 0)
                {
                    lblTotalPrice.Text = (unitprice1 * count).ToString();
                }
                else if (cboxRestaurant.SelectedIndex == 1)
                {
                    lblTotalPrice.Text = (unitprice2 * count).ToString();
                }
            }
            else
            {
                count = 1;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GlobalVar.OrdererList1.Add(txtOrderer.Text);

            if (lboxItems.SelectedIndex > -1)
            {
                if (cboxRestaurant.SelectedIndex == 0)
                {
                    //   orderList1
                    ArrayList ArrayData1 = new ArrayList();
                    //ArrayData1.Add(grade);
                    //ArrayData1.Add(txtOrderer.Text);
                    //ArrayData1.Add(restaurant);
                    ArrayData1.Add(item1);                    
                    ArrayData1.Add(count);
                    ArrayData1.Add(unitprice1);
                    //ArrayData1.Add(lblTotalPrice.Text);
                    //ArrayData1.Add(lblTime.Text);
                    GlobalVar.orderList1.Add(ArrayData1);

                    //   AmountList1
                    ArrayList ArrayData3 = new ArrayList();
                    //ArrayData3.Add(grade);
                    //ArrayData3.Add(txtOrderer.Text);
                    ArrayData3.Add(restaurant);
                    ArrayData3.Add(item1);
                    //ArrayData3.Add(unitprice1);
                    ArrayData3.Add(count);
                    //ArrayData3.Add(lblTotalPrice.Text);
                    //ArrayData3.Add(lblTime.Text);
                    GlobalVar.AmountList1.Add(ArrayData3);

                    ArrayList ArrayDataSave1 = new ArrayList();
                    ArrayDataSave1.Add(grade);
                    ArrayDataSave1.Add(txtOrderer.Text);
                    ArrayDataSave1.Add(restaurant);
                    ArrayDataSave1.Add(item1);
                    ArrayDataSave1.Add(unitprice1);
                    ArrayDataSave1.Add(count);
                    ArrayDataSave1.Add(lblTotalPrice.Text);
                    ArrayDataSave1.Add(lblTime.Text);
                    GlobalVar.Save1.Add(ArrayDataSave1);

                }
                else if (cboxRestaurant.SelectedIndex == 1) 
                {
                    //   orderList2
                    ArrayList ArrayData2 = new ArrayList();
                    //ArrayData2.Add(grade);
                    //ArrayData2.Add(txtOrderer.Text);
                    //ArrayData2.Add(restaurant);
                    ArrayData2.Add(item2);                    
                    ArrayData2.Add(count);
                    ArrayData2.Add(unitprice2);
                    //ArrayData2.Add(lblTotalPrice.Text);
                    //ArrayData2.Add(lblTime.Text);
                    GlobalVar.orderList2.Add(ArrayData2);

                    //   AmountList2
                    ArrayList ArrayData4 = new ArrayList();
                    //ArrayData4.Add(grade);
                    //ArrayData4.Add(txtOrderer.Text);
                    ArrayData4.Add(restaurant);
                    ArrayData4.Add(item2);
                    //ArrayData4.Add(unitprice2);
                    ArrayData4.Add(count);
                    //ArrayData4.Add(lblTotalPrice.Text);
                    //ArrayData4.Add(lblTime.Text);
                    GlobalVar.AmountList2.Add(ArrayData4);

                    ArrayList ArrayDataSave2 = new ArrayList();
                    ArrayDataSave2.Add(grade);
                    ArrayDataSave2.Add(txtOrderer.Text);
                    ArrayDataSave2.Add(restaurant);
                    ArrayDataSave2.Add(item1);
                    ArrayDataSave2.Add(unitprice1);
                    ArrayDataSave2.Add(count);
                    ArrayDataSave2.Add(lblTotalPrice.Text);
                    ArrayDataSave2.Add(lblTime.Text);
                    GlobalVar.Save1.Add(ArrayDataSave2);
                }

                MessageBox.Show("已加入訂購單");

            }
            else
            {
                MessageBox.Show("請選擇品項");
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }
    }
}
