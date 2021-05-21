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
    public partial class frmProduct : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter dap;
        DataSet ds;
        public frmProduct()
        {
            InitializeComponent();
        }
        private void frmProduct_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=ACER;Initial Catalog=Freshfruit;Integrated Security=True";
            con.Open();
            LoadDuLieu("Select * from SANPHAM");
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(false);
        }
        private void LoadDuLieu(String sql)
        {
            ds = new DataSet();
            dap = new SqlDataAdapter(sql, con);
            dap.Fill(ds);
            dgvKetQua.DataSource = ds.Tables[0];
            dgvKetQua.Refresh();
        }

        private void HienChiTiet(Boolean hien)
        {
            txtMaSP.Enabled = hien;
            txtTenSP.Enabled = hien;
            dtpNgayHH.Enabled = hien;
            dtpNgayNH.Enabled = hien;
            txtDonVi.Enabled = hien;
            txtDonGia.Enabled = hien;
            txtGhiChu.Enabled = hien;
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }
        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtMaSP_TextChanged(object sender, EventArgs e)
        {

        }

        private void DgvKetQua_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            try
            {
                txtMaSP.Text = dgvKetQua[0, e.RowIndex].Value.ToString();
                txtTenSP.Text = dgvKetQua[1, e.RowIndex].Value.ToString();
                dtpNgayNH.Value = (DateTime)dgvKetQua[2, e.RowIndex].Value;
                dtpNgayHH.Value = (DateTime)dgvKetQua[3, e.RowIndex].Value;
                txtDonVi.Text = dgvKetQua[4, e.RowIndex].Value.ToString();
                txtDonGia.Text = dgvKetQua[5, e.RowIndex].Value.ToString();
                txtGhiChu.Text = dgvKetQua[6, e.RowIndex].Value.ToString();
            }
            catch (Exception)
            {
            }
        }
        

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "TÌM KIẾM MẶT HÀNG";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            String sql = "SELECT * FROM SANPHAM";
            String dk = "";
            if (txtTKMaSP.Text.Trim() != "")
            {
                dk += " MaSP like '%" + txtTKMaSP.Text + "%'";
            }
            if (txtTKTenSP.Text.Trim() != "" && dk != "")
            {
                dk += " AND TenSP like N'%" + txtTKTenSP.Text + "%'";
            }
            if (txtTKTenSP.Text.Trim() != "" && dk == "")
            {
                dk += " TenSP like N'%" + txtTKTenSP.Text + "%'";
            }
            if (dk != "")
            {
                sql += " WHERE" + dk;
            }
            LoadDuLieu(sql);
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "THÊM MẶT HÀNG";
            XoaTrangChiTiet();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
        }
        private void XoaTrangChiTiet()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            dtpNgayNH.Value = DateTime.Now;
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "CẬP NHẬT MẶT HÀNG";
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa mã hàng " + txtMaSP.Text + " không? Nếu có ấn nút Có, không thì ấn nút Hủy", "Xóa sản phẩm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTieuDe.Text = "XÓA MẶT HÀNG";
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
            if (txtTenSP.Text.Trim() == "")
            {
                errChiTiet.SetError(txtTenSP, "Bạn không để trống tên sản phẩm!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }
            if (dtpNgayNH.Value > DateTime.Now)
            {
                errChiTiet.SetError(dtpNgayNH, "Ngày sản xuất không hợp lệ!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }
            if (dtpNgayHH.Value < dtpNgayNH.Value)
            { errChiTiet.SetError(dtpNgayHH, "Ngày hết hạn nhỏ hơn ngày sản xuất!"); return; }
            else { errChiTiet.Clear(); }

            if (txtDonVi.Text.Trim() == "")
                { errChiTiet.SetError(txtDonVi, "Bạn không để trống đơn vị!"); return; }
            else { errChiTiet.Clear(); }

            if (txtDonGia.Text.Trim() == "")
                { errChiTiet.SetError(txtDonGia, "Bạn không để trống đơn giá!"); return; }
            else { errChiTiet.Clear(); }

                if (txtMaSP.Text.Trim() == "")
                { errChiTiet.SetError(txtMaSP, "Bạn không để trống mã sản phẩm trường này!"); return; }
                else
                { errChiTiet.Clear(); }

                sql = "Select Count(*) From SANPHAM Where MaSP ='" + txtMaSP.Text + "'";
                cmd = new SqlCommand(sql, con);
                int val = (int)cmd.ExecuteScalar();
                if (val > 0)
                {
                    errChiTiet.SetError(txtMaSP, "Mã sản phẩm trùng trong cơ sở dữ liệu");
                    return;
                }
                errChiTiet.Clear();

                sql = "INSERT INTO SANPHAM(MaSP,TenSP,NgayNH,NgayHH,DonVTinh,DonGia,GhiChu) VALUES (";
                sql += "N'" + txtMaSP.Text + "',N'" + txtTenSP.Text + "','" + dtpNgayNH.Value.Date + "','" + dtpNgayHH.Value.Date + "',N'" + txtDonVi.Text + "',N'" + txtDonGia.Text + "',N'" + txtGhiChu.Text + "')";
            if (btnSua.Enabled == true)
            {
                sql = "Update SANPHAM SET ";
                sql += "TenSP = N'" + txtTenSP.Text + "',";
                sql += "NgayNH = '" + dtpNgayNH.Value.Date + "',";
                sql += "NgayHH = '" + dtpNgayHH.Value.Date + "',";
                sql += "DonVTinh = N'" + txtDonVi.Text + "',";
                sql += "DonGia = '" + txtDonGia.Text + "',";
                sql += "GhiChu = N'" + txtGhiChu.Text + "' ";
                sql += "Where MaSP = N'" + txtMaSP.Text + "'";
            }
            if (btnXoa.Enabled == true)
            {
                sql = "Delete From SANPHAM Where MaSP =N'" + txtMaSP.Text + "'";
            }
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "Select * from SANPHAM";
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
