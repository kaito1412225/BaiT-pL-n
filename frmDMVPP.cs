using QLyVPP1.Class;
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

namespace QLyVPP1
{
    public partial class frmDMVPP : Form
    {
        DataTable tbldmvp;

        public frmDMVPP()
        {
            InitializeComponent();
        }

        private void picAnh_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbldmvp.Rows.Count ==0)
            {
                MessageBox.Show("khong con du lieu", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtmavpp.Text == "")
            {
                MessageBox.Show("khong co du lieu ", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }
            if (MessageBox.Show("ban co muon xoa ko?", "thong bao", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)== DialogResult.OK)
            {
                sql = "Delete tblDMVPP where MaVPP = N'" + txtmavpp.Text + "'";
                Functions.RunSql(sql);
                Load_DataGridView();
                ResetValues();
            }


        }

        private void frmDMVPP_Load(object sender, EventArgs e)
        {
            txtmavpp.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            Load_DataGridView();

            ResetValues();
                
        }
        private void ResetValues ()
        {
            txtmavpp.Text = "";
            txttenvpp.Text = "";
            txtmacl.Text = "";
            txtmadv.Text = "";
            txtmaloai.Text = "";
            txtmamau.Text = "";
            txtmacd.Text = "";
            txtmanuocsx.Text = "";
            txtsoluong.Text = "0";
            txtgiaban.Text = "0";
            txtgianhap.Text = "0";
            txtsoluong.Enabled = false;
            txtgiaban.Enabled = false;
            txtgianhap.Enabled = false;
            txtanh.Text = "";
            picAnh.Image = null;

                
        }
       

        private void Load_DataGridView ()
        {
            string sql;
            sql = "SELECT MaVPP ,TenVPP , MaCL,MaDV, MaMau, MaCD, MaNuocSX, SoLuong, Anh , DonGN, DonGB  FROM tblDMVPP";
            tbldmvp = Functions.GetDataToTable(sql);
            DataGridView_DMVPP.DataSource = tbldmvp;
            DataGridView_DMVPP.Columns[0].HeaderText = " Mã VPP";
            DataGridView_DMVPP.Columns[1].HeaderText = "Tên VPP";
            DataGridView_DMVPP.Columns[2].HeaderText = "Chất Liệu";
            DataGridView_DMVPP.Columns[3].HeaderText = "Đơn Vị Tính";
            DataGridView_DMVPP.Columns[4].HeaderText = "Màu";
            DataGridView_DMVPP.Columns[5].HeaderText = "Công Dụng";
            DataGridView_DMVPP.Columns[6].HeaderText = "Nước Sản Xuất";
            DataGridView_DMVPP.Columns[7].HeaderText = "Số Lượng";
            DataGridView_DMVPP.Columns[8].HeaderText = "Đơn Giá Nhập";
            DataGridView_DMVPP.Columns[9].HeaderText = "Đơn Giá Bán";
            DataGridView_DMVPP.Columns[0].Width = 80;
            DataGridView_DMVPP.Columns[1].Width = 140;
            DataGridView_DMVPP.Columns[2].Width = 80;
            DataGridView_DMVPP.Columns[3].Width = 80;
            DataGridView_DMVPP.Columns[4].Width = 80;
            DataGridView_DMVPP.Columns[5].Width = 80;
            DataGridView_DMVPP.Columns[6].Width = 80;
            DataGridView_DMVPP.Columns[7].Width = 80;
            DataGridView_DMVPP.Columns[8].Width = 100;
            DataGridView_DMVPP.Columns[9].Width = 100;
            DataGridView_DMVPP.AllowUserToDeleteRows = false;
            DataGridView_DMVPP.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void DataGridView_Click(object sender, DataGridViewCellEventArgs e)
        {
            
            if (btnThem.Enabled == false )
            {
                MessageBox.Show("Đang ở chế độ thêm mới ", "Thông Báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmavpp.Focus();
                return;
            }
           if (tbldmvp .Rows.Count==0)
            {
                MessageBox.Show("không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            txtmavpp.Text = DataGridView_DMVPP.CurrentRow.Cells["MaVPP"].Value.ToString();
            txttenvpp.Text = DataGridView_DMVPP.CurrentRow.Cells["TenVPP"].Value.ToString();
            txtmacl.Text = DataGridView_DMVPP.CurrentRow.Cells["MaCL"].Value.ToString();
            txtmaloai.Text = DataGridView_DMVPP.CurrentRow.Cells["MaL"].Value.ToString();
            txtmadv.Text= DataGridView_DMVPP.CurrentRow.Cells["MaDV"].Value.ToString();
            txtmamau.Text = DataGridView_DMVPP.CurrentRow.Cells["MaMau"].Value.ToString();
            txtmacd.Text = DataGridView_DMVPP.CurrentRow.Cells["MaCD"].Value.ToString();
            txtmanuocsx.Text = DataGridView_DMVPP.CurrentRow.Cells["MaNuocSX"].Value.ToString();
            txtgiaban.Text = DataGridView_DMVPP.CurrentRow.Cells["DonGB"].Value.ToString();
            txtgianhap.Text = DataGridView_DMVPP.CurrentRow.Cells["DonGN"].Value.ToString();
            txtsoluong .Text = DataGridView_DMVPP.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtanh.Text =  Functions.GetFielValues("Select Anh From tbl DMVPP Where MaVPP = N'" + txtmavpp.Text + "'");
            picAnh.Image = Image.FromFile(txtanh.Text);
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
            btnBoQua.Enabled = true; 
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtmavpp.Enabled = true;
            txtmavpp.Focus();
           
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtmavpp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã vpp", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                txtmavpp.Focus();
                return;
            }
            if (txttenvpp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên vpp ", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                txttenvpp.Focus();
                return;
            }
            if (txtmacl.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmacl.Focus();
                return;
            }
            if (txtmadv.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã đơn vị tính ", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                txtmadv.Focus();
                return;
            }
            if (txtmaloai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã loại ", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                txtmaloai.Focus();
                return;
            }
            if (txtmamau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã màu ", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                txtmamau.Focus();
                return;
            }
            if (txtmanuocsx.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nước sản xuất ", "Thông báo", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                txtmanuocsx.Focus();
                return;
            }
            if (txtanh.Text.Trim().Length == 0)
            {
                MessageBox.Show("bạn phải nhập ảnh minh hoa cho hang ", "thông báo ", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtanh.Focus();
                return;
            }
            sql = "select MaLoai From tblTheLoai Where MaLoai= N'" + txtmaloai.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Loại Hàng này đã có, ban hay nhập mặt hàng khác", "thong báo",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmaloai.Focus();
                txtmaloai.Text = "";
                return;
            }
            sql = "select MaVPP From tblDMVPP Where MaVPP = N '" + txtmavpp.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("văn phong phẩm này đã tồn tại , bạn hay nhập mã vpp khác", "thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmavpp.Focus();
                txtmavpp.Text = "";
                return;
            }
            sql = " INSERT INTO tblDMVPP (MaVPP,TeenVPP, MaL,MaDV,MaCL,MaMau,MaCD,MaNuocSX,SoLuong,Anh, DonGB,DonGN) Values ('"
              + txtmavpp.Text.Trim() + "', N'" + txttenvpp.Text.Trim() + "',N'" + txtmacl.Text.Trim() + "',N'" +
              txtmaloai.Text.Trim() + "',N'" + txtmadv.Text.Trim() + "',N'" + txtmamau.Text.Trim() + "',N'" +
              txtmacd.Text.Trim() + "',N'" + txtmanuocsx.Text.Trim() + "',N'" + txtsoluong.Text.Trim()+ "',N'" +
              txtanh.Text + "',N'" + txtgiaban.Text + "',N'"
              + txtgianhap .Text + "')";
            Functions.RunSql(sql);
            Load_DataGridView();
            ResetValues();
            btnxoa.Enabled = true;
            btnThem.Enabled = true;
            btnsua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtmavpp.Enabled = false;
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbldmvp.Rows.Count== 0)
            {
                MessageBox.Show("khong con du lieu", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (txtmavpp.Text =="")
            {
                MessageBox.Show("ban chua chon ban ghi nao ", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (txttenvpp.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ten van phong pham", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            if (txtmaloai.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ma loai", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (txtmadv.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ma don vi tinh", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (txtmacl.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ma chat lieu", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (txtmamau.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ma mau", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (txtmacd.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ma cong dung", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (txtmanuocsx.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap ma nuoc san xuat", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (txtsoluong .Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap so luong ", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (txtanh.Text.Trim().Length == 0)
            {
                MessageBox.Show("ban phai nhap anh minh hoa", "thong bao",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtanh.Focus();
                return;

            }

            sql = "UPDATE tblDMVPP SET TenVPP = N'" + txttenvpp.Text.Trim().ToString() + "',MaCL = N'" + txtmacl.Text.Trim().ToString()
                + "', MaL = N'" + txtmaloai.Text.Trim().ToString() + "', MaDV = N'" + txtmadv.Text.Trim().ToString() + "',MaNuocSX=N" +
                txtmanuocsx.Text.Trim().ToString() + "',Anh= '" + txtanh.Text + "'WHere MaVPP = N'" + txtmavpp.Text + "'";
            Functions.RunSql(sql);
            Load_DataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
                
                }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnxoa.Enabled = true;
            btnsua.Enabled = true;
            btnLuu.Enabled = false;
            txtmavpp.Enabled = false;
        
        }

        private void txtmavpp_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnopen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|Gif(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.InitialDirectory = "D:\\";
            dlgOpen.FilterIndex = 2; 
            dlgOpen.Title = "Chon hinh anh de hien thi";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtanh.Text = dlgOpen.FileName;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtmavpp.Text=="") && (txttenvpp.Text == "") && (txtmamau.Text=="")
                && (txtmaloai.Text =="") && (txtmadv.Text=="")&& (txtmacl.Text=="")&& (txtmacd.Text=="")
                && (txtmanuocsx .Text==""))
            {
                MessageBox.Show("Hay Nhap mot dieu kien tim kiem!!", "yeu cau...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "Select * From tblDNVPP Where 1=1";
            if (txtmavpp.Text!= "")
                sql = sql + " AND MaVPP Like N'%" + txtmavpp.Text + "%'";
            if (txttenvpp.Text != "")
                sql = sql + " AND TenVPP Like N'%" + txttenvpp.Text + "%'";
            if (txtmaloai.Text != "")
                sql = sql + " AND MaLoai Like N'%" + txtmaloai.Text + "%'";
            if (txtmadv.Text != "")
                sql = sql + " AND MaDV like N'%" + txtmadv.Text + "%'";
            if (txtmacl.Text != "")
                sql = sql + " AND MaCL like N'%" + txtmacl.Text + "%'";
            if (txtmamau.Text != "")
                sql = sql + " AND MaMau like N'%" + txtmamau.Text + "%'";
            if (txtmacd.Text != "")
                sql = sql + " AND MaCD like N'%" + txtmacd.Text + "%'";
            if (txtmanuocsx.Text != "")
                sql = sql + " AND MaCL like N'%" + txtmanuocsx.Text + "%'";
            tbldmvp = Functions.GetDataToTable(sql);
            if (tbldmvp.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Có " + tbldmvp.Rows.Count + " bản ghi thỏa mãn điều kiện!!!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DataGridView_DMVPP.DataSource = tbldmvp;
            ResetValues();
        }

        private void btnHienThiDanhSach_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaVPP, TenVPP, MaLoai, MaDV, MaCL,MaMau, MaCD, MaNuocSX,SoLuong,Anh,DonGN,DonGB FROM tblDMVPP";
               tbldmvp = Functions.GetDataToTable(sql);
           DataGridView_DMVPP.DataSource = tbldmvp;
        }

        private void txtsoluong_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
