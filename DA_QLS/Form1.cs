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


namespace DA_QLS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True");
            
            try
            {
                conn.Open();
                String tk = txttk.Text;
                String mk = txtpass.Text;
                String sql = @"select * from NGUOIDUNG where TenDangNhap='" + tk + "' and Password='" + mk + "' ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dtr = cmd.ExecuteReader();

                if(dtr.Read()==true)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng nhập Thất bại");
                }
            }catch(Exception  )
            
            {
                MessageBox.Show("Lỗi kết nối");
            }   
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult tl;
            tl = MessageBox.Show("Bạn chắc chắn muốn thoát", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(tl==DialogResult.OK)
            {
                frmChon f1 = new frmChon();
                f1.Close();
                Application.Exit();
            }
        }
    }
}
