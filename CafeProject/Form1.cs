using CafeProject.Common;
using CafeProject.DB;
using System.Data;
using System.Data.SqlClient;

namespace CafeProject
{
    public partial class Form1 : Form
    {
        //create new SqlConnection.obj to the CMSDB at 'ConString'
        SqlConnection sqlCon = new SqlConnection(DBCommon.ConString); //ConString is a static field that can be called directly
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textUsername.Text.Trim()== "" || textPassword.Text.Trim()=="")
            {
                MessageBox.Show("Enter username & password.");
            }
            else 
            {
                sqlCon = CmnMethods.OpenConnectionString(sqlCon);
                string query = string.Format(@"SELECT * FROM UserInfo WHERE Username='{0}' AND UserPassword='{1}'", textUsername.Text.Trim(), textPassword.Text.Trim() );
                SqlDataAdapter sda = new(query, sqlCon);
                
                DataTable datatable = new();
                sda.Fill(datatable);
                if (datatable.Rows.Count > 0)
                {
                    UserOrderForm userOrderForm = new UserOrderForm();
                    userOrderForm.Show();
                    this.Hide();

                    CmnMethods.GetUserInfo(datatable);

                  //  UserOrderForm userOrder = new UserOrderForm();
                  //  userOrder.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username & password.");
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (textUsername.Text.Trim() =="" || textPassword.Text.Trim() =="")
            {
                MessageBox.Show("Fill in username and password.");
            }
            else
            {
                sqlCon = CmnMethods.OpenConnectionString(sqlCon);
                string query = @"INSERT INTO UserInfo (Username, UserPassword, Contact, AddedDate, AddedBy) VALUES (@Username, @UserPassword, @Contact, @AddedDate, @AddedBy)";
                SqlCommand command = new SqlCommand(query, sqlCon);
                command.Parameters.AddWithValue("@Username", textUsername.Text.Trim());
                command.Parameters.AddWithValue("@UserPassword", textPassword.Text.Trim());
                command.Parameters.AddWithValue("@Contact", "");
                command.Parameters.AddWithValue("@AddedDate", DateTime.Now);
                command.Parameters.AddWithValue("@AddedBy", Global.UserInfo.UserId); // the login user ID
                command.ExecuteNonQuery();
                MessageBox.Show("Signup Successfully. Now Please login");
                sqlCon.Close(); 
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {
            //create new Userinfo.obj (static class)
            Global.UserInfo = new Models.UserInfo();
            Global.UserInfo.Username = "Guest";

            //create new UserOrderForm.obj (normal class, not static)
            UserOrderForm userOrderForm = new UserOrderForm();
            userOrderForm.Show();   
            this.Hide();
        }
    }
}