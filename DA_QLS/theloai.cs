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
    public partial class frmTheLoai : Form
    {
        public frmTheLoai()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection();
        DataSet ds = new DataSet("Theloai");
        SqlDataAdapter daTheLoai;

        private void frmTheLoai_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True";
            Hienthidatagrid();

            //command Thêm thể loại;
            String sThemTL = @"insert into THELOAI values(@Matheloai, @Tentheloai)";
            SqlCommand cmThemtl = new SqlCommand(sThemTL, conn);
            cmThemtl.Parameters.Add("@Matheloai", SqlDbType.NVarChar, 50, "MaTheLoai");
            cmThemtl.Parameters.Add("@Tentheloai", SqlDbType.NVarChar, 50, "TenTheLoai");
            daTheLoai.InsertCommand = cmThemtl;

            // command sửa thể loại
            String sSuaTL = @"update THELOAI set TenTheLoai=@Tentheloai  where MaTheLoai=@Matheloai ";
            SqlCommand cmSuatl = new SqlCommand(sSuaTL, conn);
            cmSuatl.Parameters.Add("@Matheloai", SqlDbType.NVarChar, 50, "MaTheLoai");
            cmSuatl.Parameters.Add("@Tentheloai", SqlDbType.NVarChar, 50, "TenTheLoai");
            daTheLoai.UpdateCommand = cmSuatl;

            //command xóa thể loại
            String sXoaTL = @"delete from THELOAI where MaTheLoai=@Matheloai";
            SqlCommand cmxoatl = new SqlCommand(sXoaTL, conn);
            cmxoatl.Parameters.Add("@Matheloai", SqlDbType.NVarChar, 50, "MaTheLoai");
            daTheLoai.DeleteCommand = cmxoatl;

            btnLuu.Enabled=false;
            btnHuy.Enabled = false;
        }
        private void Hienthidatagrid()
        {
            String sQueryTheLloai = @"select * from THELOAI";
            daTheLoai = new SqlDataAdapter(sQueryTheLloai, conn);

            daTheLoai.Fill(ds);
            dgTL.DataSource = ds.Tables[0];
            dgTL.Columns["MaTheLoai"].HeaderText = "Mã Thể Loại";
            dgTL.Columns["MaTheLoai"].Width = 120;
            dgTL.Columns["TenTheLoai"].HeaderText = "Tên Thể Loại";
            dgTL.Columns["TenTheLoai"].Width = 680;
        }

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void btnThem_Click(object sender, EventArgs e)
        {
            conn.Open();
            String hd = @"(Select Convert(NVarChar(50),MaTheLoai) from THELOAI where Convert(NVarChar(50),MaTheLoai)='" + txtMaTL.Text + "')";
            SqlCommand thd = new SqlCommand(hd, conn);
            String mhd = (String)thd.ExecuteScalar();
            conn.Close();
            if (txtMaTL.Text == mhd)
            {
                MessageBox.Show("Mã đã có sẵn, vui lòng nhập mã khác", "Cảnh báo");
                txtMaTL.Focus();
            }

            else

            if (txtMaTL.Text=="" || txtTenTL.Text=="")
            {
                MessageBox.Show("Bạn chưa nhập đủ dữ liệu!!!","Thông báo");
                txtMaTL.Focus();
            }
          
             else
                  {
                DataRow row = ds.Tables[0].NewRow();
                row["MaTheLoai"] = txtMaTL.Text;
                row["TenTheLoai"] = txtTenTL.Text;
                ds.Tables[0].Rows.Add(row);

                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
                }
            txtMaTL.Clear();
            txtTenTL.Clear();
            txtMaTL.Focus();

        }

        private void dgTL_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgTL.SelectedRows[0];
            txtMaTL.Text = dr.Cells["MaTheLoai"].Value.ToString();
            txtTenTL.Text = dr.Cells["TenTheLoai"].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr=dgTL.SelectedRows[0];
            dgTL.BeginEdit(true);
            dr.Cells["MaTheLoai"].Value = txtMaTL.Text;
            dr.Cells["TenTheLoai"].Value=txtTenTL.Text;
            dgTL.EndEdit();
            MessageBox.Show("Đã cập nhập thành công", "Thông báo");
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgTL.SelectedRows[0];
            DialogResult tl;
            tl = MessageBox.Show("Bạn chắc chắn Xóa", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (tl == DialogResult.OK)
            {
                dgTL.Rows.Remove(dr);
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables[0].RejectChanges();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daTheLoai.Update(ds);
                MessageBox.Show("Đã lưu!", "Thông báo");
                dgTL.Refresh();
            }catch
            {
                return;
            }
            
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }
    }
}
