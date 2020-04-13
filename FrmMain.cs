using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using QLyVPP1.Class;

namespace QLyVPP1
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();         
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Class.Functions.Connect();
            Application.Exit();
        }

        private void mnuDMVPP_Click(object sender, EventArgs e)
        {
            frmDMVPP frm = new frmDMVPP();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void mnuChatLieu_Click(object sender, EventArgs e)
        {
           
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
           
        }

        private void menuChiTietHoaDonBan_Click(object sender, EventArgs e)
        {

        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            frmHoaDonBanHang frm = new frmHoaDonBanHang();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void mnuHoaDonNhap_Click(object sender, EventArgs e)
        {
            FrmHoadonnhaphang frm = new FrmHoadonnhaphang();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }
}
