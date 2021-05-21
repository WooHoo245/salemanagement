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
    public partial class Invoice : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter dap;
        DataSet ds;
        public Invoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source = ACER; Initial Catalog = Freshfruit; Integrated Security = True";
            LoadDuLieu("Select * from HOADON");
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(false);
            // TODO: This line of code loads data into the 'freshfruitDataSet.HOADON' table. You can move, or remove it, as needed.
            this.hOADONTableAdapter.Fill(this.freshfruitDataSet.HOADON);

        }
        private void HienChiTiet(Boolean hien)
        {
            txtMaHD.Enabled = hien;
            txtMaKH.Enabled = hien;
            txtMaNV.Enabled = hien;
            dtpNgayLapHD.Enabled = hien;
            dtpNgayGH.Enabled = hien;
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


        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "TÌM KIẾM HOÁ ĐƠN";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            String sql = "SELECT * FROM HOADON";
            String dk = "";
            if (txtTKMaHD.Text.Trim() != "")
            {
                dk += " MaHD like '%" + txtTKMaHD.Text + "%'";
            }
            if (txtTKMaKH.Text.Trim() != "" && dk != "")
            {
                dk += " AND MaKH like N'%" + txtTKMaKH.Text + "%'";
            }
            if (txtTKMaKH.Text.Trim() != "" && dk == "")
            {
                dk += " MaKH like N'%" + txtTKMaKH.Text + "%'";
            }
            if (dk != "")
            {
                sql += " WHERE" + dk;
            }
            LoadDuLieu(sql);
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "THÊM HOÁ ĐƠN";
            XoaTrangChiTiet();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
        }
        private void XoaTrangChiTiet()
        {
            txtMaHD.Text = "";
            txtMaKH.Text = "";
            txtMaNV.Text = "";
            dtpNgayLapHD.Value = DateTime.Now;
            dtpNgayGH.Value = DateTime.Now;
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "CẬP NHẬT HOÁ ĐƠN";
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
                txtMaHD.Text = dgvKetQua[0, e.RowIndex].Value.ToString();
                txtMaKH.Text = dgvKetQua[1, e.RowIndex].Value.ToString();
                txtMaNV.Text = dgvKetQua[2, e.RowIndex].Value.ToString();
                dtpNgayLapHD.Value = (DateTime)dgvKetQua[3, e.RowIndex].Value;
                dtpNgayGH.Value = (DateTime)dgvKetQua[4, e.RowIndex].Value;              
            }
            catch (Exception)
            {
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa Hoá Đơn"+txtMaHD.Text+"này không? Nếu có ấn nút Có, không thì ấn nút Hủy", "Xóa hoá đơn", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTieuDe.Text = "XÓA HOÁ ĐƠN";
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
            if (txtMaKH.Text.Trim() == "")
            {
                errChiTiet.SetError(txtMaKH, "Bạn không thể để trống mã KH!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }

            if (txtMaNV.Text.Trim() == "")
            { errChiTiet.SetError(txtMaNV, "Bạn không thể để trống mã NV!"); return; }
            else { errChiTiet.Clear(); }

            if (btnThem.Enabled == true)
            {
                if (txtMaHD.Text.Trim() == "")
                { errChiTiet.SetError(txtMaHD, "Bạn không để trống mã HD này!"); return; }
                else
                {
                    sql = "Select Count(*) From HOADON Where MaHD ='" + txtMaHD.Text + "'";
                    cmd = new SqlCommand(sql, con);
                    int val = (int)cmd.ExecuteScalar();
                    if (val > 0)
                    {
                        errChiTiet.SetError(txtMaHD, "Mã HD trùng trong cơ sở dữ liệu");
                        return;
                    }
                    errChiTiet.Clear();
                }

            }

            sql = "INSERT INTO HOADON(MaHD,MaKH,MaNV,NgayLapHD,NgayGH)VALUES (";
            sql += "N'" + txtMaHD.Text + "',N'" + txtMaKH.Text + "',N'" + txtMaNV.Text + "','" + dtpNgayLapHD.Value.Date + "','" + dtpNgayGH.Value.Date + "')";

            if (btnSua.Enabled == true)
            {
                sql = "Update HOADON SET";
                sql += "MaKH = N'" + txtMaKH.Text + "',";
                sql += "MaNV = N'" + txtMaNV.Text + "',";
                sql += "NgayLapHD = '" + dtpNgayLapHD.Value.Date + "',";
                sql += "NgayGH = '" + dtpNgayGH.Value.Date + "',";
                sql += "Where MaHD = N'" + txtMaHD.Text + "'";
            }

            if (btnXoa.Enabled == true)
            {
                sql = "Delete From HOADON Where MaHD =N'" + txtMaHD.Text + "'";
            }
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "Select * from HOADON";
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

        private void TxtMaHD_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
