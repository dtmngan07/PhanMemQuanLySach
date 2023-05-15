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
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection();
        DataSet ds = new DataSet("KhachHang");
        SqlDataAdapter daKhachHang;

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True";
            Hienthidatagrid();


            //command Thêm Khách hàng
            String sThemKH = @"insert into KhachHang values(@makhachhang,@tenkhachhang,@sdt)";
            SqlCommand cmThemKH = new SqlCommand(sThemKH, conn);
            cmThemKH.Parameters.Add("@makhachhang", SqlDbType.NVarChar, 50, "MaKhachHang");
            cmThemKH.Parameters.Add("@tenkhachhang", SqlDbType.NVarChar, 50, "TenKhachHang");
            cmThemKH.Parameters.Add("@sdt", SqlDbType.NChar, 10, "SDT");
            daKhachHang.InsertCommand = cmThemKH;

            // command sửa Khách hàng
            String sSuaKH = @"update KhachHang set TenKhachHang=@tenkhachhang, SDT=@sdt where MaKhachHang=@makhachhang";
            SqlCommand cmSuaKH = new SqlCommand(sSuaKH, conn);
            cmSuaKH.Parameters.Add("@makhachhang", SqlDbType.NVarChar, 50, "MaKhachHang");
            cmSuaKH.Parameters.Add("@tenkhachhang", SqlDbType.NVarChar, 50, "TenKhachHang");
            cmSuaKH.Parameters.Add("@sdt", SqlDbType.NChar, 10, "SDT");
            daKhachHang.UpdateCommand = cmSuaKH;

            //command xóa Khách hàng
            String sXoaKH = @"delete from KhachHang where MaKhachHang=@makhachhang";
            SqlCommand cmXoaKH = new SqlCommand(sXoaKH, conn);
            cmXoaKH.Parameters.Add("@makhachhang", SqlDbType.NVarChar, 50, "MaKhachHang");
            daKhachHang.DeleteCommand = cmXoaKH;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        private void Hienthidatagrid()
        {
            String sQueryTG = @"select * from KHACHHANG";
            daKhachHang = new SqlDataAdapter(sQueryTG, conn);
            daKhachHang.Fill(ds);
            dgKhachHang.DataSource = ds.Tables[0];
            dgKhachHang.Columns["MaKhachHang"].HeaderText = "Mã khách hàng";
            dgKhachHang.Columns["MaKhachHang"].Width = 100;
            dgKhachHang.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
            dgKhachHang.Columns["TenKhachHang"].Width = 250;
            dgKhachHang.Columns["SDT"].HeaderText = "Số điện thoại";
            dgKhachHang.Columns["SDT"].Width = 130;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void btnThem_Click(object sender, EventArgs e)
        {
            conn.Open();
            String hd = @"(Select Convert(NVarChar(50),MaKhachHang) from KHACHHANG where Convert(NVarChar(50),MaKhachHang)='" + txtMaKH.Text + "')";
            SqlCommand thd = new SqlCommand(hd, conn);
            String mhd = (String)thd.ExecuteScalar();
            conn.Close();
            if (txtMaKH.Text == mhd)
            {
                MessageBox.Show("Mã đã có sẵn, vui lòng nhập mã khác", "Cảnh báo");
                txtMaKH.Focus();
            }

            else if (txtMaKH.Text == "" || txtTenKH.Text == "" || txtSoDTKH.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ dữ liệu!!!", "Thông báo");
            }
            else
            {
                DataRow row = ds.Tables[0].NewRow();
                row["MaKhachHang"] = txtMaKH.Text;
                row["TenKhachHang"] = txtTenKH.Text;
                row["SDT"] = txtSoDTKH.Text;
                ds.Tables[0].Rows.Add(row);
            }
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSoDTKH.Clear();
            txtMaKH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgKhachHang.SelectedRows[0];
            dgKhachHang.BeginEdit(true);
            dr.Cells["MaKhachHang"].Value = txtMaKH.Text;
            dr.Cells["TenKhachHang"].Value = txtTenKH.Text;
            dr.Cells["SDT"].Value = txtSoDTKH.Text;
            dgKhachHang.EndEdit();
            MessageBox.Show("Đã cập nhập thành công", "Thông báo");
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgKhachHang.SelectedRows[0];
            DialogResult tl;
            tl = MessageBox.Show("Bạn chắc chắn Xóa", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (tl == DialogResult.OK)
            {
                dgKhachHang.Rows.Remove(dr);
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
            daKhachHang.Update(ds);
            MessageBox.Show("Đã lưu!", "Thông báo");
            dgKhachHang.Refresh();
        }

        private void dgKhachHang_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgKhachHang.SelectedRows[0];
            txtMaKH.Text = dr.Cells["MaKhachHang"].Value.ToString();
            txtTenKH.Text = dr.Cells["TenKhachHang"].Value.ToString();
            txtSoDTKH.Text = dr.Cells["SDT"].Value.ToString();
        }

       
    }
}
