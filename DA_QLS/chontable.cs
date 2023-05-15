using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA_QLS
{
    public partial class frmChon : Form
    {
        public frmChon()
        {
            InitializeComponent();
        }

        private void frmChon_Load(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void btnSach_Click(object sender, EventArgs e)
        {
            SACH fs = new SACH();
            this.Hide();
            fs.ShowDialog();
            this.Show();
            
        }

        private void btnTheLoai_Click(object sender, EventArgs e)
        {
            frmTheLoai tl = new frmTheLoai();
           
            this.Hide();
            tl.ShowDialog();
            this.Show();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHang fs = new frmKhachHang();
            this.Hide();
            fs.ShowDialog();
            this.Show();
        }

        private void btnTacGia_Click(object sender, EventArgs e)
        {
            tacgia fs = new tacgia();
            this.Hide();
            fs.ShowDialog();
            this.Show();
        }

        private void btnBanSach_Click(object sender, EventArgs e)
        {
            frmBanSach fs = new frmBanSach();
            this.Hide();
            fs.ShowDialog();
            this.Show();
        }

        private void btnNXB_Click(object sender, EventArgs e)
        {
            nxb fs = new nxb();
            this.Hide();
            fs.ShowDialog();
            this.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien fs =new  frmNhanVien();
            this.Hide();
            fs.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult tl;
            tl = MessageBox.Show("Bạn chắc chắn muốn thoát", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tl == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}
