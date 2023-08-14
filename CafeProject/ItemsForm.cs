using CafeProject.Common;
using CafeProject.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeProject
{
    public partial class ItemsForm : Form
    {
        SqlConnection sqlCon = new SqlConnection(DBCommon.ConString);
        public ItemsForm()
        {
            InitializeComponent();
            usernameLabel.Text = Global.UserInfo.Username;
            LoadItems();
        }

        private void LoadItems()
        {
            sqlCon = CmnMethods.OpenConnectionString(sqlCon);
            string query = "SELECT * FROM Item";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            var dataSet = new DataSet();
            sda.Fill(dataSet);
            itemGridView.DataSource = dataSet.Tables[0];
            sqlCon.Close();

            //Hide common Columns like AddedDate, AddedBy, UpdatedDate, UpdatedBy
            itemGridView.Columns[0].Visible = false;
            itemGridView.Columns[5].Visible = false;
            itemGridView.Columns[6].Visible = false;
            itemGridView.Columns[7].Visible = false;
            itemGridView.Columns[8].Visible = false;


        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new();
            form1.Show();
            CmnMethods.ResetGlobal();

        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserOrderForm userOrder = new UserOrderForm();
            userOrder.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersForm usersForm = new UsersForm();
            usersForm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (itemCodeTextBox.Text.Trim()=="" || itemNameTextBox.Text.Trim()=="" || categoryComboBox.Text.Trim()=="" || categoryComboBox.Text.Trim() =="Category" ||itemPriceTextBox.Text.Trim()== "")
            {
                MessageBox.Show("Fill all fields.");
            }
            else
            {
                sqlCon = CmnMethods.OpenConnectionString(sqlCon);
                string query = @"INSERT INTO Item (ItemCode, ItemName, Category, Price, AddedDate, AddedBy) VALUES (@ItemCode, @ItemName, @Category, @Price, @AddedDate, @AddedBy )";
                SqlCommand commd = new SqlCommand(query, sqlCon);

                commd.Parameters.AddWithValue("@ItemCode", itemCodeTextBox.Text.Trim());
                commd.Parameters.AddWithValue("@ItemName", itemNameTextBox.Text.Trim());
                commd.Parameters.AddWithValue("@Category", categoryComboBox.Text.Trim());
                commd.Parameters.AddWithValue("@Price", itemPriceTextBox.Text.Trim());
                commd.Parameters.AddWithValue("@AddedDate", DateTime.Now);
                commd.Parameters.AddWithValue("@AddedBy", Global.UserInfo.UserId); //Login user ID
                commd.ExecuteNonQuery();
                MessageBox.Show("Item Created successfully.");

                sqlCon.Close();//close connection to DB
                LoadItems();   //load all items from DB
                Reset();       //reset & empty textboxes 

            }
        }

        private void Reset()
        {
            itemIdTextBox.Text = Convert.ToString(0);
            itemCodeTextBox.Text = "";
            itemNameTextBox.Text = "";
            categoryComboBox.Text = "Category";
            itemPriceTextBox.Text = "";

        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (itemIdTextBox.Text.Trim() == "" || Convert.ToInt32(itemIdTextBox.Text) == 0)
            {
                MessageBox.Show("Select an item."); 
            }
            else if (itemCodeTextBox.Text.Trim() == "" || itemNameTextBox.Text.Trim() == "" || categoryComboBox.Text.Trim() == "" || categoryComboBox.Text.Trim() == "Category" || itemPriceTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Fill in all fields.");
            }
            else
            {
                sqlCon = CmnMethods.OpenConnectionString(sqlCon);
                string query = @"UPDATE Item SET ItemCode = @ItemCode, ItemName =@ItemName, Category= @Category, Price = @Price WHERE ItemId = @ItemId";
                SqlCommand command = new SqlCommand(query, sqlCon);

                command.Parameters.AddWithValue("@ItemId", itemIdTextBox.Text.Trim());
                command.Parameters.AddWithValue("@ItemCode", itemCodeTextBox.Text.Trim());
                command.Parameters.AddWithValue("@ItemName", itemNameTextBox.Text.Trim());
                command.Parameters.AddWithValue("@Category", categoryComboBox.Text.Trim());
                command.Parameters.AddWithValue("@Price", itemPriceTextBox.Text.Trim());
                command.Parameters.AddWithValue("@AddedDate", DateTime.Now);
                command.Parameters.AddWithValue("@AddedBy", Global.UserInfo.UserId);
                command.ExecuteNonQuery();
                MessageBox.Show("Item Updated successfully.");
                sqlCon.Close();
                LoadItems();
                Reset();
            }
        }

        //2. pass the selected item.obj into each textboxes to represent each field of the item 
        private void itemGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            itemIdTextBox.Text = itemGridView.Rows[e.RowIndex].Cells["ItemId"].Value.ToString();
            itemCodeTextBox.Text = itemGridView.Rows[e.RowIndex].Cells["ItemCode"].Value.ToString();
            itemNameTextBox.Text = itemGridView.Rows[e.RowIndex].Cells["ItemName"].Value.ToString();
            categoryComboBox.Text = itemGridView.Rows[e.RowIndex].Cells["Category"].Value.ToString();
            itemPriceTextBox.Text = itemGridView.Rows[e.RowIndex].Cells["Price"].Value.ToString();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (itemIdTextBox.Text.Trim() =="" || Convert.ToInt32(itemIdTextBox.Text) ==0)
            {
                MessageBox.Show("Select an item");
            }
            else
            {
                //1. start connection with DB
                sqlCon = CmnMethods.OpenConnectionString(sqlCon);
                string query = string.Format(@"DELETE FROM Item WHERE ItemId={0}", Convert.ToInt32(itemIdTextBox.Text));
                SqlCommand command = new SqlCommand(query, sqlCon);
                command.ExecuteNonQuery();
                MessageBox.Show("Item Deleted Successfully");

                //3. when delete is done, end connection with DB
                sqlCon.Close();
                //4. Reload all items from DB, when edit is done.
                LoadItems();
                Reset();
            }

        }
    }
}
