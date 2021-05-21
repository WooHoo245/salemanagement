using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FruitsHouse
{
    class Function
    {
        public static SqlConnection Con;
        public static void Connect()
        {
            Con = new SqlConnection();
            Con.ConnectionString = @"Data Source=ACER;Initial Catalog=Freshfruit;Integrated Security=True";
            Con.Open();
            if (Con.State == ConnectionState.Open)
                MessageBox.Show("Kết nối thành công");
            else MessageBox.Show("Không thể kết nối với dữ liệu");
        }
        public static void Disconnect()
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();   	
                Con.Dispose(); 	
                Con = null;
            }
        }
    }
}
