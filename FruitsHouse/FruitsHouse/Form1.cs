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

namespace FruitsHouse
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void txtUserEnter(object sender, EventArgs e)
        {
            if(txtUsername.Text.Equals(@"Username or Email"))
            {
                txtUsername.Text = "";
            }
        }

        private void txtxUserLeave(object sender, EventArgs e)
        {
            if (txtUsername.Text.Equals(""))
            {
                txtUsername.Text = @"Username or Email";
            }
        }

        private void txtPassEnter(object sender, EventArgs e)
        {
            if(txtPassword.Text.Equals("Password"))
            {
                txtPassword.Text = "";
            }
        }

        private void txtPassLeave(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(""))
            {
                txtPassword.Text = "Password";
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection (@"Data Source=ACER;Initial Catalog=Freshfruit;Integrated Security=True");
            {
                conn.Open();
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string sql = "select * from AUTH where Username ='" + username + "'and  Password ='" + password + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dta = cmd.ExecuteReader();
                if (dta.Read() == true)
                {
                    Main m = new Main();
                    this.Hide();
                    m.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Thử lại lần nữa nào, check carefully nha!", "Bạn ơi! Bạn ơi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
    }
}
