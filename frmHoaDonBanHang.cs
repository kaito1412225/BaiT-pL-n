using QLyVPP1.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QLyVPP1
{
    public partial class frmHoaDonBanHang : Form
    {
        DataTable tblCTHDB;
        public frmHoaDonBanHang()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmHoaDonBanHang_Load(object sender, EventArgs e)
        {
            btnthemHd.Enabled = true;
            btnLuu.Enabled = false;
            btnhuy.Enabled = false;
            
            txtsoHDB.ReadOnly = true;
            txttennv.ReadOnly = true;
            txttenvpp.ReadOnly = true;
            txttenkh.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtdienthoai.ReadOnly = true;
            txtthanhtien.ReadOnly = true;
            txtdongia.ReadOnly = true;
            txttongtienhoadon.ReadOnly = true;
            txtgiamgia.ReadOnly = true;
            txtgiamgia.Text = "0";
            txttongtienhoadon.Text = "0";

            

            Functions.FillCombo("SELECT MaVPP, TenVPP FROM tblDMVPP ", cboSoHDB, "MaVPP","TenVPP");
            cboSoHDB.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaNV, TenNV FROM tblNhanVien", cboManhanvien, "MaNV", "TenNV");
            cboManhanvien.SelectedIndex = -1;

            if (txtsoHDB.Text != "")
            {
                btnhuy.Enabled = true;
               

            }
            Load_DataGridViiewChiTiet();

        }
        private void Load_DataGridViiewChiTiet()
        {
            string sql;
            sql = " SELECT a.MaVPP, b.TenVPP, a.SoLuong, b.DonGB, a.GiamGia, a.ThanhTien FROM tblChiTietHoaDonBan as a ,tblDMVPP as b Where a.SoHDB = N'"
                + txtsoHDB.Text + "'AND a.MaVPP = b.MaVPP";
            tblCTHDB = Functions.GetDataToTable(sql);
            DataGridViewChiTiet.DataSource = tblCTHDB;
            DataGridViewChiTiet.Columns[0].HeaderText = "Mã VPP";
            DataGridViewChiTiet.Columns[1].HeaderText = "Tên VPP";
            DataGridViewChiTiet.Columns[2].HeaderText = "Số Lượng";
            DataGridViewChiTiet.Columns[3].HeaderText = "Giảm Giá";
            DataGridViewChiTiet.Columns[4].HeaderText = "Đơn Giá";
            DataGridViewChiTiet.Columns[5].HeaderText = "Thành Tiền";
            DataGridViewChiTiet.Columns[0].Width = 80;
            DataGridViewChiTiet.Columns[1].Width = 100;
            DataGridViewChiTiet.Columns[2].Width = 80;
            DataGridViewChiTiet.Columns[3].Width = 90;
            DataGridViewChiTiet.Columns[4].Width = 90;
            DataGridViewChiTiet.Columns[5].Width = 90;

            DataGridViewChiTiet.AllowUserToAddRows = false;
            DataGridViewChiTiet.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        private void Load_THongtinHD()
        {
            string sql;
            sql = "SELECT NgayBan FROM tblHoaDonBan WHERE SoHDB = N'" + txtsoHDB.Text + "'";
            txtNgayBan.Text = Functions.ConvertDateTime(Functions.GetFielValues(sql));
            sql = "SELECT MaNV FROM tblHoaDonBan Where SoHDB =N'" + txtsoHDB + "'";
            cboManhanvien.Text = Functions.GetFielValues(sql);
            sql = "SELECT MaKH FROM tblKhachHang Where SoHDB =N'" + txtsoHDB + "'";
            txtmakh.Text = Functions.GetFielValues(sql);
            sql = "SELECT TongTien FROM tblHoaDonBan Where SoHDB =N'" + txtsoHDB + "'";
            txttongtienhoadon.Text = Functions.GetFielValues(sql);

        }
        private void ResetValues()
        {
            txtsoHDB.Text = "";
            txtNgayBan.Text = DateTime.Now.ToShortDateString();
            txtmakh.Text = "";
            cboManhanvien.Text = "";
            cbomavpp.Text = "";
            txtSoLuong.Text = "0";
            txtgiamgia.Text = "0";
            txtthanhtien.Text = "0";
            txtdongia.Text = "0";
           

        }
        private void btnthemHd_Click(object sender, EventArgs e)
        {
            btnhuy.Enabled = false;
            btnLuu.Enabled = true;
            
            btnthemHd.Enabled = false;
            ResetValues();
            txtsoHDB.Text = Functions.CreateKey("HDB");
            Load_DataGridViiewChiTiet();
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, slcon, tong, tongmoi;
            sql = "SELECT SoHDB FROM tblHoaDonBan Where  SoHDB =N'" + txtsoHDB.Text + "'";
            if(! Functions.CheckKey(sql))
            {
                if(txtNgayBan.Text.Length==0)
                {
                    MessageBox.Show("Ban Phai Nhap ngay ban", "thong bao",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNgayBan.Focus();
                    return;

                }
                if(txtmakh.Text.Length== 0)
                {
                    MessageBox.Show("ban phai nhap ma khach hang", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtmakh.Focus();
                    return;
                }
                if(cboManhanvien.Text.Length==0)
                {
                    MessageBox.Show("ban phai nhap ma nhan vien", "thong bao",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboManhanvien.Focus();
                    return;
                }
                if (cbomavpp.Text.Length==0)
                {
                    MessageBox.Show("Ban phai nhap ma van phong ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbomavpp.Focus();
                    return;
                }
                sql = "INSERT INTO tblHoaDonBan (SoHDB,NgayBan , MaKH, TongTien ) VALUES (N'"
                     + txtsoHDB.Text.Trim() + "','" + Functions.ConvertDateTime(txtNgayBan.Text.Trim())
                     + "',N'" + txtmakh.Text + "'N,'" + cboManhanvien.SelectedValue + "','"
                     + txttongtienhoadon.Text + ")";
                Functions.RunSql(sql);
                if(cboSoHDB.Text.Trim().Length==0)
                {
                    MessageBox.Show("ban phai nhap so hoa don", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboSoHDB.Focus();
                    return;
                }
                if(txtSoLuong.Text.Trim().Length==0)
                {
                    MessageBox.Show("ban phai nhap so luong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoLuong.Text = "";
                    txtSoLuong.Focus();
                    return;
                }
                if(txtgiamgia.Text.Trim().Length==0)
                {
                    MessageBox.Show("ban phai nhap giam gia", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtgiamgia.Focus();
                    return;
                }
                sql = "SELECT MaVPP FROM tblChiTietHoaDonBan Where MaVPP=N'" +
                    cbomavpp.SelectedValue + "'AND SoHDB = N'" + txtsoHDB.Text.Trim() + "'";
                if(Functions.CheckKey(sql))
                {
                    MessageBox.Show("Ma VPP Nay Da co, ban phai nhap ma khac", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ResetValuesHang();
                    cboSoHDB.Focus();
                    return;
                }
                sl = Convert.ToDouble(Functions.GetFielValues("SELECT SoLuong FROM tblMaVPP Where MaVPP =N'" +
                    cbomavpp.SelectedValue + "'"));
                if(Convert.ToDouble(txtSoLuong.Text) > sl)
                {
                    MessageBox.Show("so luong mat hang nay chi con" + sl, "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoLuong.Text = "";
                    txtSoLuong.Focus();
                    return;

                }
                sql = "INSERT INTO tblChiTietHoaDonBan ( SoHDB, MaVPP, SoLuong, GiamGia,ThanhTien) VALUES (N'"
                    + txtsoHDB.Text.Trim() + "',N'" + cbomavpp.SelectedValue + "','"
                    + txtSoLuong.Text + "," + txtgiamgia.Text + "," + txtthanhtien.Text + ")";
                Functions.RunSql(sql);
                Load_DataGridViiewChiTiet();
                slcon = sl - Convert.ToDouble(txtSoLuong.Text);
                sql = "UPDATE tblDMVPP SET SoLuong =" + slcon + "WHERE MaVPP=N '" + cbomavpp.SelectedValue + "'";
                Functions.RunSql(sql);
                tong = Convert.ToDouble(Functions.GetFielValues("SELECT TongTien FROM tbHoaDonBan WHERE SoHDB =N'" + txtsoHDB.Text + "'"));
                tongmoi = tong + Convert.ToDouble(txtthanhtien.Text);
                sql ="UPDATE tblHoaDonBan SET TongTien =" + tongmoi + "where SoHDB =N'"+ txtsoHDB.Text + "'";
                Functions.RunSql(sql);
                txttongtienhoadon.Text = tongmoi.ToString();
                ResetValuesHang();
                btnhuy.Enabled = true;
                btnthemHd.Enabled = true;
                

            }

        }
        private void ResetValuesHang()
        {
            cbomavpp.Text = "";
            txtSoLuong.Text = "";
            txtgiamgia.Text = "0";
            txtthanhtien.Text = "0";
        }

        private void DataGridViewChiTiet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string mavpp;
            double Thanhtien;
            if(tblCTHDB.Rows.Count == 0)
            {
                MessageBox.Show("khong co du lieu ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            if((MessageBox.Show("ban co chac chan muon xoa ko?","thong bao", MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes))
            {
                mavpp = DataGridViewChiTiet.CurrentRow.Cells["MaVPP"].Value.ToString();
                DelHang(txtsoHDB.Text, mavpp);

                Thanhtien = Convert.ToDouble(DataGridViewChiTiet.CurrentRow.Cells["ThanhTien"].Value.ToString());
                DelUpdateTongtien(txtsoHDB.Text, Thanhtien);
                Load_DataGridViiewChiTiet();


            }
            
        }
        private void DelHang (string SoHDB, String MaVPP)
        {
            double s, sl, slcon;
            string sql;
            sql = "SELECT SoLuong FROM tblChiTietHoaDonBan Where SoHDB = N'" + SoHDB + "' AND MaVPP =N'" + MaVPP + "'";
            s = Convert.ToDouble(Functions.GetFielValues(sql));
            sql = "DELETE tblChiTietHoaDonBan Where SoHDB =N'" + SoHDB + "'AND MaVPP=N'" + MaVPP + "'";
            Functions.RunSql(sql);
            sql = "SELECT SoLuong FROM tblDMVPP Where MaVPP = N'" + MaVPP + "'";
            sl = Convert.ToDouble(Functions.GetFielValues(sql));
            slcon = sl + s;
            sql = "UPDATE tblDMVPP SET SoLuong =" + slcon + "WHERE MaVPP =N'" + MaVPP + "'";
            Functions.RunSql(sql);
        }
        private void DelUpdateTongtien (string SoHDB, double ThanhTien)
        {
            double Tong, Tongmoi;
            string sql;
            sql = "SELECT TongTien FROM tblHoaDonBan WHERE SoHDB =N'" + SoHDB + "'";
            Tong = Convert.ToDouble(Functions.GetFielValues(sql));
            Tongmoi = Tong - ThanhTien;
            sql = "UPDATE tblHoaDonBan SET TongTien =" + Tongmoi + "WHERE SoHDB= N'" +
                SoHDB + "'";
            Functions.RunSql(sql);
            txttongtienhoadon.Text = Tongmoi.ToString();
            
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblCTHDB.Rows.Count == 0)
            {
                MessageBox.Show("khong con du lieu", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtsoHDB.Text == "")
            {
                MessageBox.Show("khong co du lieu ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }
            if (MessageBox.Show("ban co muon xoa ko?", "thong bao", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "Delete tblHoaDonBanHang AND tblChiTietHoaDonBan where SoHDB = N'" + txtsoHDB + "'MaVPP=N'"+ cbomavpp.Text +"'" ;
                Functions.RunSql(sql);
                Load_DataGridViiewChiTiet();
                ResetValues();
            }
        }

        private void cboManhanvien_TextChanged(object sender, EventArgs e)
        {
            string str;
            if(cboManhanvien.Text =="")
            {
                txttennv.Text = "";
                

            }
            str = "Select TenNV From tblNhanVien Where MaNV=N'" + cboManhanvien.SelectedValue + "'";
            txttennv.Text = Functions.GetFielValues(str);
           
        }

        private void cbomavpp_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cbomavpp.Text == "")
            {
                txttenvpp.Text = "";
                txtSoLuong.Text = "";
               

            }
            str = "Select TenVPP From tblDMVPP Where MaVPP=N'" + cbomavpp.SelectedValue + "'";
            txttenvpp.Text = Functions.GetFielValues(str);
            str = "Select SoLuong From tblDMVPP Where MaNV =N'" + cbomavpp.SelectedValue + "'";
            txtSoLuong.Text = Functions.GetFielValues(str);
            
        }

        private void cboMakhach_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtgiamgia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtgiamgia.Text);
            if (txtdongia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtdongia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtthanhtien.Text = tt.ToString();


        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtgiamgia_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void frmHoaDonBanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetValues();
        }

        private void btndong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btndong_Click_(object sender, EventArgs e)
        {

        }

        private void txtmakh_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
