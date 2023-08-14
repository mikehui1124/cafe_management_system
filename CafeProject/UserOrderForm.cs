using CafeProject.Common;
using CafeProject.DB;
using CafeProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeProject
{
    public partial class UserOrderForm : Form
    {
        SqlConnection sqlCon = new SqlConnection(DBCommon.ConString);
        public UserOrderForm()
        {
            InitializeComponent();
            label4.Text = Global.UserInfo.Username;
            LoadItems();
        }

        //Load items from DB into itemGridView.Columns
        private void LoadItems()
        {
            sqlCon = CmnMethods.OpenConnectionString(sqlCon);
            string query = "SELECT * FROM Item";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            var dataSet = new DataSet();
            sda.Fill(dataSet);
            //2. Load all 9 fields from a row into the itemGridView
            itemGridView.DataSource = dataSet.Tables[0];
            sqlCon.Close();

            //Hide Common fields like AddedData, AddedBy...
            itemGridView.Columns[0].Visible = false;
            itemGridView.Columns[5].Visible = false;
            itemGridView.Columns[6].Visible = false;
            itemGridView.Columns[7].Visible = false;
            itemGridView.Columns[8].Visible = false;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1(); // Login page
            form1.Show();
            CmnMethods.ResetGlobal();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemsForm itemsForm = new ItemsForm();
            itemsForm.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersForm usersForm = new UsersForm();
            usersForm.Show();
        }

        private void qtyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if( !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar !='.') )
            {
                e.Handled = true;
            }

            if ( (e.KeyChar == '.') &&  ((sender as TextBox).Text.IndexOf('.') > -1) )
            {
                e.Handled = true;
            }
        }

        //1. Select an item from gridView first, before clicking the btn
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            AddToCart();
        }

        private void AddToCart()
        {
            if (qtyTextBox.Text.Trim() == "" || Convert.ToInt32(qtyTextBox.Text) == 0)
            {
                MessageBox.Show("Give Quantity.");
                qtyTextBox.Focus();
            }
            else if (itemGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an Item from the List Please");
            }
            else
            {
                DataGridViewRow row = this.itemGridView.SelectedRows[0]; //it holds the selected item.obj

                int itemId = Convert.ToInt32(row.Cells["ItemId"].Value);
                string itemCode = Convert.ToString(row.Cells["ItemCode"].Value);
                string itemName = row.Cells["ItemName"].Value.ToString();
                string category = row.Cells["Category"].Value.ToString();
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);


                int rowIndex = -1;

                var orderTotalRows = orderGridView.Rows.Count;

                if (orderTotalRows >1)
                {

                    for (int i=0; i< orderGridView.Rows.Count -1; i++ )
                    {
                        if (orderGridView.Rows[i].Cells["itemId"].Value.ToString() == itemId.ToString())
                        {
                            rowIndex = orderGridView.Rows[i].Index;
                            break; //Break this for_loop
                        }
                    }
                }

                int qty = Convert.ToInt32(qtyTextBox.Text);
                decimal amount = 0;

                if (rowIndex < 0)
                {
                   //Add new row to the List, return the index of new row and update 'rowIndex'
                    rowIndex = orderGridView.Rows.Add();
                }
                else //*when selected item is already in the orderList
                {
                    int previousQty = Convert.ToInt32(orderGridView.Rows[rowIndex].Cells["Qty"].Value); //or put (int) as a cast
                    qty += previousQty;
                }
                amount = qty * price;

                //after add a new row to orderView, populate each field of the new row
                orderGridView.Rows[rowIndex].Cells["ItemId"].Value = itemId.ToString();
                orderGridView.Rows[rowIndex].Cells["ItemCode"].Value = itemCode;
                
                orderGridView.Rows[rowIndex].Cells["ItemName"].Value = itemName; 
                orderGridView.Rows[rowIndex].Cells["Category"].Value = category;
                orderGridView.Rows[rowIndex].Cells["UnitPrice"].Value = price;
                orderGridView.Rows[rowIndex].Cells["Qty"].Value = qty;
                orderGridView.Rows[rowIndex].Cells["Amount"].Value = amount;
                //orderGridView.Rows[rowIndex].Selected = true;

                CalculateTotal();
            }
        }

        private void CalculateTotal()
        {
            decimal totalAmount = 0;
            for (int i=0; i < orderGridView.Rows.Count -1; i++)
            {
                totalAmount += Convert.ToDecimal(orderGridView.Rows[i].Cells["Amount"].Value);
            }
            labelAmount.Text = totalAmount.ToString();
        }

        private void btnPlaceThisOrder_Click(object sender, EventArgs e)
        {
            if (Global.UserInfo.UserId == 0) //check if user has logged in or not (== 0)
            {
                MessageBox.Show("Please login first");
                Form1 form = new Form1();
                form.Show();
            }
            else if (orderNoTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Give an order no.");
                orderNoTextBox.Focus();
            }
            else if (sellerTextBox.Text.Trim()== "")
            {
                MessageBox.Show("Give Seller's name.");
                sellerTextBox.Focus();
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(DBCommon.ConString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        string query = @"INSERT INTO UserOrder (OrderNo,SellerName,AddedDate,AddedBy) VALUES (@OrderNo,@SellerName,@AddedDate,@AddedBy); SELECT SCOPE_IDENTITY();";
                        SqlCommand command = new SqlCommand(query, conn, transaction);
                        command.Parameters.AddWithValue("@OrderNo", orderNoTextBox.Text.Trim());
                        command.Parameters.AddWithValue("@SellerName", sellerTextBox.Text.Trim());
                        command.Parameters.AddWithValue("@AddedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@AddedBy", Global.UserInfo.UserId); //1
                        int orderId = Convert.ToInt32(command.ExecuteScalar()); //it returns the ID (pk) of the new added row

                        if (orderId > 0)
                        {
                            //Create a list of UserOrderDetail.obj
                            List<UserOrderDetail> userOrderDetails = new List<UserOrderDetail>();
                            for (int i = 0; i< orderGridView.Rows.Count - 1; i++)
                            {
                                //Call constructor to create new obj
                                var orderDetail = new UserOrderDetail() 
                                { 
                                    UserOrderId = 0,
                                    ItemId = Convert.ToInt32(orderGridView.Rows[i].Cells["ItemId"].Value ),
                                    UnitPrice = Convert.ToDecimal(orderGridView.Rows[i].Cells["UnitPrice"].Value),
                                    Qty = Convert.ToInt32(orderGridView.Rows[i].Cells["Qty"].Value)
                                };

                                query = @"INSERT INTO UserOrderDetail (UserOrderId,ItemId,UnitPrice,Qty) VALUES (@UserOrderId,@ItemId,@UnitPrice,@Qty);";
                                command = new SqlCommand(query, conn, transaction);
                                command.Parameters.AddWithValue("@UserOrderId", orderId);
                                command.Parameters.AddWithValue("@ItemId", orderDetail.ItemId);
                                command.Parameters.AddWithValue("@UnitPrice", orderDetail.UnitPrice);
                                command.Parameters.AddWithValue("@Qty", orderDetail.Qty);
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        MessageBox.Show("The Order is Placed.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        transaction.Rollback();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

            }

        }

        private void categoryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FilterByCategory();
        }

        private void FilterByCategory()
        {
            sqlCon = CmnMethods.OpenConnectionString(sqlCon);
            string query = string.Format(@"SELECT * FROM Item WHERE UPPER(Category)= UPPER('{0}')", categoryComboBox.SelectedItem.ToString());
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder scb = new SqlCommandBuilder(sda);
            var dataSet = new DataSet();
            sda.Fill(dataSet);
            itemGridView.DataSource = dataSet.Tables[0];
            sqlCon.Close();
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            this.Hide();
            OrderViewForm orderViewForm = new OrderViewForm();
            orderViewForm.Show();
        }
    }
}
