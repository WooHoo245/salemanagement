using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FruitsHouse
{
    public partial class Main : Form
    {
        public static object Functions { get; private set; }
        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Function.Connect();
        }
        public Main()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void BunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

        }

        private void BunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            ClientList m = new ClientList();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
           frmMatHang m = new frmMatHang();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Login m = new Login();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Class.Function.Disconnect();
            Application.Exit();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            frmStaff m = new frmStaff();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Invoice m = new Invoice();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            frmBillDetail m = new frmBillDetail();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            frmReport m = new frmReport();
            this.Hide();
            m.ShowDialog();
            this.ShowDialog();
        }
    }
}
