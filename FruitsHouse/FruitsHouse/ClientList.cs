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
    public partial class ClientList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter dap;
        DataSet ds;

        public ClientList()
        {
            InitializeComponent();
        }

        private void ClientList_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=ACER;Initial Catalog=Freshfruit;Integrated Security=True";
            LoadDuLieu("Select * from KHACHHANG");
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(false);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "TÌM KIẾM KHÁCH HÀNG";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            String sql = "SELECT * FROM KHACHHANG";
            String dk = "";
            if (txtTKMaKH.Text.Trim() != "")
            {
                dk += " MaKH like '%" + txtTKMaKH.Text + "%'";
            }
            if (txtTKTenKH.Text.Trim() != "" && dk != "")
            {
                dk += " AND TenKH like N'%" + txtTKTenKH.Text + "%'";
            }
            if (txtTKTenKH.Text.Trim() != "" && dk == "")
            {
                dk += " TenKH like N'%" + txtTKTenKH.Text + "%'";
            }
            if (dk != "")
            {
                sql += " WHERE" + dk;
            }
            LoadDuLieu(sql);
        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void TxtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DgvDulieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            try
            {
                txtMaKH.Text = dgvDulieu[0, e.RowIndex].Value.ToString();
                txtTenKH.Text = dgvDulieu[1, e.RowIndex].Value.ToString();
                dtbNgayDH.Value = (DateTime)dgvDulieu[2, e.RowIndex].Value;
                txtSDT.Text = dgvDulieu[4, e.RowIndex].Value.ToString();
                txtDiaChi.Text = dgvDulieu[5, e.RowIndex].Value.ToString();
                txtThanhPho.Text = dgvDulieu[6, e.RowIndex].Value.ToString();
                txtGhichu.Text = dgvDulieu[6, e.RowIndex].Value.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void LoadDuLieu(String sql)
        {
            ds = new DataSet();
            dap = new SqlDataAdapter(sql, con);
            dap.Fill(ds);
            dgvDulieu.DataSource = ds.Tables[0];
        }
        private void HienChiTiet(Boolean hien)
        {
            txtMaKH.Enabled = hien;
            txtTenKH.Enabled = hien;
            dtbNgayDH.Enabled = hien;
            txtSDT.Enabled = hien;
            txtDiaChi.Enabled = hien;
            txtGhichu.Enabled = hien;
            txtThanhPho.Enabled = hien;
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "THÊM KHÁCH HÀNG";
            XoaTrangChiTiet();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
        }
        private void XoaTrangChiTiet()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            dtbNgayDH.Value = DateTime.Now;
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "CẬP NHẬT KHÁCH HÀNG";
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa khách hàng" + txtMaKH.Text + "này không? Nếu có ấn nút Có, không thì ấn nút Hủy", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTitle.Text = "XÓA KHÁCH HÀNG";
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                HienChiTiet(true);
            }

        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (con.State != ConnectionState.Open)
                con.Open();
            if (txtTenKH.Text.Trim() == "")
            {
                errorChiTiet.SetError(txtTenKH, "Bạn không thể để trống tên khách hàng!");
                return;
            }
            else
            {
                errorChiTiet.Clear();
            }

            if (txtSDT.Text.Trim() == "")
            { errorChiTiet.SetError(txtSDT, "Bạn không thể để trống SĐT!"); return; }
            else { errorChiTiet.Clear(); }

            if (txtDiaChi.Text.Trim() == "")
              { errorChiTiet.SetError(txtDiaChi, "Bạn không thể để trống địa chỉ!"); return; }
            else { errorChiTiet.Clear(); }

            if (txtThanhPho.Text.Trim() == "")
            { errorChiTiet.SetError(txtThanhPho, "Bạn không thể để trống thành phố!"); return; }
            else { errorChiTiet.Clear(); }

            if (btnThem.Enabled == true)
            {
                if (txtMaKH.Text.Trim() == "")
                { errorChiTiet.SetError(txtMaKH, "Bạn không thể để trống mã sản phẩm trường này!"); return; }
                else
                {
                    sql = "Select Count(*) From KHACHHANG Where MaKH ='" + txtMaKH.Text + "'";
                    cmd = new SqlCommand(sql, con);
                    int val = (int)cmd.ExecuteScalar();
                    if (val > 0)
                    {
                        errorChiTiet.SetError(txtMaKH, "Mã KH trùng trong cơ sở dữ liệu");
                        return;
                    }
                    errorChiTiet.Clear();
                }

            }

            sql = "INSERT INTO KHACHHANG(MaKH,TenKH,NgayDH,DTKH,DiaChi,ThanhPho,GhiChu)VALUES (";
            sql += "N'" + txtMaKH.Text + "',N'" + txtTenKH.Text + "','" + dtbNgayDH.Value.Date + "',N'" + txtSDT.Text + "',N'" + txtDiaChi.Text + "',N'" + txtThanhPho + "',N'" + txtGhichu.Text + "')";

            if (btnSua.Enabled == true)
            {
                sql = "Update KHACHHANG SET ";
                sql += "TenKH = N'" + txtTenKH.Text + "',";
                sql += "NgayDH = '" + dtbNgayDH.Value.Date + "',";
                sql += "DTKH = N'" + txtSDT.Text + "',";
                sql += "DiaChi = N'" + txtDiaChi.Text + "',";
                sql += "ThanhPho = N'" + txtThanhPho.Text + "',";
                sql += "GhiChu = N'" + txtGhichu.Text + "' ";
                sql += "Where MaKH = N'" + txtMaKH.Text + "'";
            }

            if (btnXoa.Enabled == true)
            {
                sql = "Delete From KHACHHANG Where MaKH =N'" + txtMaKH.Text + "'";
            }
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "Select * from KHACHHANG";
            LoadDuLieu(sql);
            con.Close();
            HienChiTiet(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void BtnHuy_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            XoaTrangChiTiet();
            HienChiTiet(false);
        }
    
        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Main m = new Main();
            this.Hide();
            m.ShowDialog();
            this.Show();
        }
    }

    
}
