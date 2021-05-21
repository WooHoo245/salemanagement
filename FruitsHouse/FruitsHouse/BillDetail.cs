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
    public partial class frmBillDetail : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter dap;
        DataSet ds;
        public frmBillDetail()
        {
            InitializeComponent();
        }

        private void FrmBillDetail_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source = ACER; Initial Catalog = Freshfruit; Integrated Security = True";
            LoadDuLieu("Select * from CHITIETHD");
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(false);
            // TODO: This line of code loads data into the 'freshfruitDataSet.CHITIETHD' table. You can move, or remove it, as needed.
            this.cHITIETHDTableAdapter.Fill(this.freshfruitDataSet.CHITIETHD);

        }
        private void HienChiTiet(Boolean hien)
        {
            txtMaSP.Enabled = hien;
            txtMaHD.Enabled = hien;
            txtSoLuong.Enabled = hien;
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


        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (con.State != ConnectionState.Open)
                con.Open();
            if (txtMaSP.Text.Trim() == "")
            {
                errChiTiet.SetError(txtMaSP, "Bạn không thể để trống mã sản phẩm!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }

            if (txtSoLuong.Text.Trim() == "")
            { errChiTiet.SetError(txtSoLuong, "Bạn không thể để trống số lượng!"); return; }
            else { errChiTiet.Clear(); }

            if (btnThem.Enabled == true)
            {
                if (txtMaHD.Text.Trim() == "")
                { errChiTiet.SetError(txtMaHD, "Bạn không thể để trống mã Hoá Đơn!"); return; }
                else
                {
                    sql = "Select Count(*) From CHITIETHD Where MaHD ='" + txtMaHD.Text + "'";
                    cmd = new SqlCommand(sql, con);
                    int val = (int)cmd.ExecuteScalar();
                    if (val > 0)
                    {
                        errChiTiet.SetError(txtMaSP, "Mã hoá đơn trùng trong cơ sở dữ liệu");
                        return;
                    }
                    errChiTiet.Clear();
                }

            }

            sql = "INSERT INTO CHITIETHD(MaHD,MaSP,SoLuong)VALUES (";
            sql += "N'" + txtMaHD.Text + "',N'" + txtMaSP.Text + "',N'" + txtSoLuong.Text + "')";

            if (btnSua.Enabled == true)
            {
                sql = "Update CHITIETHD SET ";
                sql += "MaSP = N'" + txtMaSP.Text + "',";
                sql += "SoLuong = N'" + txtSoLuong.Text + "' ";
                sql += "Where MaHD = N'" + txtMaHD.Text + "'";
            }

            if (btnXoa.Enabled == true)
            {
                sql = "Delete From CHITIETHD Where MaHD =N'" + txtMaHD.Text + "'";
            }
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            sql = "Select * from CHITIETHD";
            LoadDuLieu(sql);
            con.Close();
            HienChiTiet(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "TÌM KIẾM CTHĐ";
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            String sql = "SELECT * FROM CHITIETHD";
            String dk = "";
            if (txtTKMaHD.Text.Trim() != "")
            {
                dk += " MaHD like '%" + txtTKMaHD.Text + "%'";
            }
            if (txtTKMaSP.Text.Trim() != "" && dk != "")
            {
                dk += " AND MaSP like N'%" + txtTKMaSP.Text + "%'";
            }
            if (txtTKMaHD.Text.Trim() != "" && dk == "")
            {
                dk += " MaHD like N'%" + txtTKMaHD.Text + "%'";
            }
            if (dk != "")
            {
                sql += " WHERE" + dk;
            }
            LoadDuLieu(sql);
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "THÊM CTHĐ";
            XoaTrangChiTiet();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            HienChiTiet(true);
        }
        private void XoaTrangChiTiet()
        {
            txtMaSP.Text = "";
            txtMaHD.Text = "";
            txtSoLuong.Text = "";
           
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "CẬP NHẬT CTHĐ";
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
                txtMaSP.Text = dgvKetQua[1, e.RowIndex].Value.ToString();
                txtSoLuong.Text = dgvKetQua[2, e.RowIndex].Value.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa mã "+txtMaHD.Text+"CTHĐ này không? Nếu có ấn nút Có, không thì ấn nút Hủy", "Xóa CTHD", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTieuDe.Text = "XÓA CTHĐ";
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                HienChiTiet(true);
            }
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
