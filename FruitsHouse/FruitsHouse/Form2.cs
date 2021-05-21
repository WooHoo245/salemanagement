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
    public partial class frmStaff : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter dap;
        DataSet ds;
        public frmStaff()
        {
            InitializeComponent();
        }

        private void FrmStaff_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=ACER;Initial Catalog=Freshfruit;Integrated Security=True";
            LoadDuLieu("Select * from NHANVIEN");
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(false);
            // TODO: This line of code loads data into the 'freshfruitDataSet.NHANVIEN' table. You can move, or remove it, as needed.
            this.nHANVIENTableAdapter.Fill(this.freshfruitDataSet.NHANVIEN);

        }
        private void HienChiTiet(Boolean hien)
        {
            txtMaNV.Enabled = hien;
            txtTenNV.Enabled = hien;
            dtpNgaySinh.Enabled = hien;
            dtpNgayNV.Enabled = hien;
            txtHoLot.Enabled = hien;
            txtPhai.Enabled = hien;
            txtDiaChi.Enabled = hien;
            txtSDT.Enabled = hien;
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }
        private void LoadDuLieu(String sql)
        {
            ds = new DataSet();
            dap = new SqlDataAdapter(sql, con);
            dap.Fill(ds);
            dgvKetQua.DataSource = ds.Tables[0];
        }


        private void TxtMaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "TÌM KIẾM NHÂN VIÊN";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            String sql = "SELECT * FROM NHANVIEN";
            String dk = "";
            if (txtTKMaNV.Text.Trim() != "")
            {
                dk += " MaNV like '%" + txtTKMaNV.Text + "%'";
            }
            if (txtTKTenNV.Text.Trim() != "" && dk != "")
            {
                dk += " AND TenNV like N'%" + txtTKTenNV.Text + "%'";
            }
            if (txtTKTenNV.Text.Trim() != "" && dk == "")
            {
                dk += " TenNV like N'%" + txtTKTenNV.Text + "%'";
            }
            if (dk != "")
            {
                sql += " WHERE" + dk;
            }
            LoadDuLieu(sql);
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "THÊM NHÂN VIÊN";
            XoaTrangChiTiet();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
        }
        private void XoaTrangChiTiet()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            dtpNgayNV.Value = DateTime.Now;
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "CẬP NHẬT NHÂN VIÊN";
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
        }

        private void DgvKetQua_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            try
            {
                txtMaNV.Text = dgvKetQua[0, e.RowIndex].Value.ToString();
                txtTenNV.Text = dgvKetQua[1, e.RowIndex].Value.ToString();
                txtHoLot.Text = dgvKetQua[2, e.RowIndex].Value.ToString();
                txtPhai.Text = dgvKetQua[3, e.RowIndex].Value.ToString();
                txtDiaChi.Text = dgvKetQua[4, e.RowIndex].Value.ToString();
                txtSDT.Text = dgvKetQua[5, e.RowIndex].Value.ToString();
                dtpNgaySinh.Value = (DateTime)dgvKetQua[6, e.RowIndex].Value;
                dtpNgayNV.Value = (DateTime)dgvKetQua[7, e.RowIndex].Value;
            }
            catch (Exception)
            {
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa nhân viên"+txtMaNV.Text+"không? Nếu có ấn nút Có, không thì ấn nút Hủy", "Xóa Nhân viên", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTieuDe.Text = "XÓA NHÂN VIÊN";
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
            if (txtTenNV.Text.Trim() == "")
            {
                errChiTiet.SetError(txtTenNV, "Bạn không thể để trống tên NV!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }

            if (txtHoLot.Text.Trim() == "")
            { errChiTiet.SetError(txtHoLot, "Bạn không thể để trống họ lót NV!"); return; }
            else { errChiTiet.Clear(); }

            if (txtDiaChi.Text.Trim() == "")
            { errChiTiet.SetError(txtDiaChi, "Bạn không thể để trống địa chỉ!"); return; }
            else { errChiTiet.Clear(); }

            if (txtSDT.Text.Trim() == "")
            { errChiTiet.SetError(txtSDT, "Bạn không thể để trống SĐT!"); return; }
            else { errChiTiet.Clear(); }

            if (btnThem.Enabled == true)
            {
                if (txtMaNV.Text.Trim() == "")
                { errChiTiet.SetError(txtMaNV, "Bạn không thể để trống mã nhân viên!"); return; }
                else
                {
                    sql = "Select Count(*) From NHANVIEN Where MaNV ='" + txtMaNV.Text + "'";
                    cmd = new SqlCommand(sql, con);
                    int val = (int)cmd.ExecuteScalar();
                    if (val > 0)
                    {
                        errChiTiet.SetError(txtMaNV, "Mã NV trùng trong cơ sở dữ liệu");
                        return;
                    }
                    errChiTiet.Clear();
                }

            }

            sql = "INSERT INTO NHANVIEN(MaNV,TenNV,HoLot,Phai,DiaChi,DTNV,NgaySinh,NgayNV) VALUES (";
            sql += "N'" + txtMaNV.Text + "',N'" + txtTenNV.Text + "',N'" + txtHoLot.Text + "',N'" + txtPhai.Text + "',N'" + txtDiaChi.Text + "','" + txtSDT.Text + "','" + dtpNgaySinh.Value.Date + "','" + dtpNgayNV.Value.Date + "')";

            if (btnSua.Enabled == true)
            {
                sql = "Update NHANVIEN SET ";
                sql += "TenNV = N'" + txtTenNV.Text + "',";
                sql += "HoLot = N'" + txtHoLot.Text + "',";
                sql += "Phai = '" + txtPhai.Text + "',";
                sql += "DiaChi = N'" + txtDiaChi.Text + "',";
                sql += "DTNV = '" + txtSDT.Text + "',";
                sql += "NgaySinh = '" + dtpNgaySinh.Value.Date + "',";
                sql += "NgayNV = '" + dtpNgayNV.Value.Date + "',";
                sql += "Where MaNV = N'" + txtMaNV.Text + "'";
            }
            if (btnXoa.Enabled == true)
            {
                sql = "Delete From NHANVIEN Where MaNV = N'" + txtMaNV.Text + "'";
            }

            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "Select * from NHANVIEN";
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
