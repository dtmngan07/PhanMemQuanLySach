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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection();
        DataSet ds = new DataSet("tblNhanVien");
        SqlDataAdapter daNhanVien;

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True";
            Hienthidatagrid();

            //command Thêm 
            String sThemNV = @"insert into NhanVien values(@manhanvien,@tennhanvien,@sdt,@diachi,@email)";
            SqlCommand cmThemNV = new SqlCommand(sThemNV, conn);
            cmThemNV.Parameters.Add("@manhanvien", SqlDbType.NVarChar, 50, "MaNhanVien");
            cmThemNV.Parameters.Add("@tennhanvien", SqlDbType.NVarChar, 50, "TenNhanVien");
            cmThemNV.Parameters.Add("@sdt", SqlDbType.NChar, 10, "SDT");
            cmThemNV.Parameters.Add("@diachi", SqlDbType.NVarChar, 50, "DiaChi");
            cmThemNV.Parameters.Add("@email", SqlDbType.NVarChar, 50, "E-mail");
            daNhanVien.InsertCommand = cmThemNV;

            // command sửa
            String sSuaNV = @"update NhanVien set TenNhanVien=@tennhanvien, SDT=@sdt, DiaChi=@diachi, [E-mail]=@email where MaNhanVien=@manhanvien";
            SqlCommand cmSuaNV = new SqlCommand(sSuaNV, conn);
            cmSuaNV.Parameters.Add("@manhanvien", SqlDbType.NVarChar, 50, "MaNhanVien");
            cmSuaNV.Parameters.Add("@tennhanvien", SqlDbType.NVarChar, 50, "TenNhanVien");
            cmSuaNV.Parameters.Add("@sdt", SqlDbType.NChar, 10, "SDT");
            cmSuaNV.Parameters.Add("@diachi", SqlDbType.NVarChar, 50, "DiaChi");
            cmSuaNV.Parameters.Add("@email", SqlDbType.NVarChar, 50, "E-mail");
            daNhanVien.UpdateCommand = cmSuaNV;

            //command xóa
            String sXoaNV = @"delete from NhanVien where MaNhanVien=@manhanvien";
            SqlCommand cmXoaNV = new SqlCommand(sXoaNV, conn);
            cmXoaNV.Parameters.Add("@manhanvien", SqlDbType.NVarChar, 50, "MaNhanVien");
            daNhanVien.DeleteCommand = cmXoaNV;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void Hienthidatagrid()
        {
            String sQueryTG = @"select * from NHANVIEN";
            daNhanVien = new SqlDataAdapter(sQueryTG, conn);
            daNhanVien.Fill(ds);
            dgnhanvien.DataSource = ds.Tables[0];
            dgnhanvien.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dgnhanvien.Columns["MaNhanVien"].Width = 100;
            dgnhanvien.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            dgnhanvien.Columns["TenNhanVien"].Width = 250;
            dgnhanvien.Columns["SDT"].HeaderText = "Số điện thoại";
            dgnhanvien.Columns["SDT"].Width = 130;
            dgnhanvien.Columns["E-mail"].HeaderText = "Email";
            dgnhanvien.Columns["E-mail"].Width = 150;
            dgnhanvien.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgnhanvien.Columns["DiaChi"].Width = 300;
        }

       

        private void btnThem_Click(object sender, EventArgs e)
        {
            conn.Open();
            String hd = @"(Select Convert(NVarChar(50),MaNhanVien) from NHANVIEN where Convert(NVarChar(50),MaNhanVien)='" + txtMaNV.Text + "')";
            SqlCommand thd = new SqlCommand(hd, conn);
            String mhd = (String)thd.ExecuteScalar();
            conn.Close();
            if (txtMaNV.Text == mhd)
            {
                MessageBox.Show("Mã đã có sẵn, vui lòng nhập mã khác", "Cảnh báo");
                txtMaNV.Focus();
            }

            else  
            if (txtMaNV.Text == "" || txtTenNV.Text == "" || txtEmailNV.Text == "" || txtDiaChiNV.Text == "" || txtSoDTNV.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ dữ liệu!!!", "Thông báo");
            }
            else
            {
                DataRow row = ds.Tables[0].NewRow();
                row["MaNhanVien"] = txtMaNV.Text;
                row["TenNhanVien"] = txtTenNV.Text;
                row["SDT"] = txtSoDTNV.Text;
                row["E-mail"] = txtEmailNV.Text;
                row["DiaChi"] = txtDiaChiNV.Text;
                ds.Tables[0].Rows.Add(row);
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtSoDTNV.Clear();
            txtEmailNV.Clear();
            txtDiaChiNV.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgnhanvien.SelectedRows[0];
            dgnhanvien.BeginEdit(true);
            dr.Cells["MaNhanVien"].Value = txtMaNV.Text;
            dr.Cells["TenNhanVien"].Value = txtTenNV.Text;
            dr.Cells["SDT"].Value = txtSoDTNV.Text;
            dr.Cells["E-mail"].Value = txtEmailNV.Text;
            dr.Cells["DiaChi"].Value = txtDiaChiNV.Text;
            dgnhanvien.EndEdit();
            MessageBox.Show("Đã cập nhập thành công", "Thông báo");
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgnhanvien.SelectedRows[0];
            DialogResult tl;
            tl = MessageBox.Show("Bạn chắc chắn Xóa", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (tl == DialogResult.OK)
            {
                dgnhanvien.Rows.Remove(dr);
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void dgnhanvien_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgnhanvien.SelectedRows[0];
            txtMaNV.Text = dr.Cells["MaNhanVien"].Value.ToString();
            txtTenNV.Text = dr.Cells["TenNhanVien"].Value.ToString();
            txtSoDTNV.Text = dr.Cells["SDT"].Value.ToString();
            txtEmailNV.Text = dr.Cells["E-mail"].Value.ToString();
           txtDiaChiNV.Text = dr.Cells["DiaChi"].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            daNhanVien.Update(ds);
            MessageBox.Show("Đã lưu!", "Thông báo");
            dgnhanvien.Refresh();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables[0].RejectChanges();
        }
    }
}
