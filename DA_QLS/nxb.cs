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
    public partial class nxb : Form
    {
        public nxb()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection();
        DataSet ds = new DataSet("NXB");
        SqlDataAdapter danxb;

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nxb_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True";
            Hienthidatagrid();

            //command Thêm NXB;
            String sThemNXB = @"insert into NXB values(@manxb,@tennxb,@diachi,@email,@sdt)";
            SqlCommand cmThemnxb = new SqlCommand(sThemNXB, conn);
            cmThemnxb.Parameters.Add("@manxb", SqlDbType.NVarChar, 50, "MaNXB");
            cmThemnxb.Parameters.Add("@tennxb", SqlDbType.NVarChar, 50, "TenNXB");
            cmThemnxb.Parameters.Add("@diachi", SqlDbType.NVarChar, 50, "DiaChi");
            cmThemnxb.Parameters.Add("@email", SqlDbType.NVarChar, 50, "E-mail");
            cmThemnxb.Parameters.Add("@sdt", SqlDbType.NChar, 10, "SDT");
            
            danxb.InsertCommand = cmThemnxb;

            // command sửa NXB
            String sSuaNXB = @"update NXB set TenNXB=@tennxb, DiaChi=@diachi, [E-mail]=@email, SDT=@sdt where MaNXB=@manxb";
            SqlCommand cmSuanxb = new SqlCommand(sSuaNXB, conn);
            cmSuanxb.Parameters.Add("@manxb", SqlDbType.NVarChar, 50, "MaNXB");
            cmSuanxb.Parameters.Add("@tennxb", SqlDbType.NVarChar, 50, "TenNXB");
            cmSuanxb.Parameters.Add("@diachi", SqlDbType.NVarChar, 50, "DiaChi");
            cmSuanxb.Parameters.Add("@email", SqlDbType.NVarChar, 50, "E-mail");
            cmSuanxb.Parameters.Add("@sdt", SqlDbType.NChar, 10, "SDT");
            danxb.UpdateCommand = cmSuanxb;

            //command xóa NXB
            String sXoaNXB = @"delete from NXB where MaNXB=@manxb";
            SqlCommand cmxoanxb = new SqlCommand(sXoaNXB, conn);
            cmxoanxb.Parameters.Add("@manxb", SqlDbType.NVarChar, 50, "MaNXB");
            danxb.DeleteCommand = cmxoanxb;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        private void Hienthidatagrid()
        {
            String sQueryNXB= @"select * from NXB";
            danxb = new SqlDataAdapter(sQueryNXB, conn);
            danxb.Fill(ds);
            dgNXB.DataSource = ds.Tables[0];
            dgNXB.Columns["MaNXB"].HeaderText = "Mã Nhà Xuất Bản";
            dgNXB.Columns["MaNXB"].Width = 100;
            dgNXB.Columns["TenNXB"].HeaderText = "Tên Nhà Xuất Bản";
            dgNXB.Columns["TenNXB"].Width = 200;
            dgNXB.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgNXB.Columns["DiaChi"].Width = 300;
            dgNXB.Columns["E-mail"].HeaderText = "E-mail";
            dgNXB.Columns["E-mail"].Width = 120;
            dgNXB.Columns["SDT"].HeaderText = "Số điện thoại";
            dgNXB.Columns["SDT"].Width = 120;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            conn.Open();
            String hd = @"(Select Convert(NVarChar(50),MaNXB) from NXB where Convert(NVarChar(50),MaNXB)='" + txtMaNXB.Text + "')";
            SqlCommand thd = new SqlCommand(hd, conn);
            String mhd = (String)thd.ExecuteScalar();
            conn.Close();
            if (txtMaNXB.Text == mhd)
            {
                MessageBox.Show("Mã đã có sẵn, vui lòng nhập mã khác", "Cảnh báo");
                txtMaNXB.Focus();
            }

            else if (txtMaNXB.Text=="" || txtTenNXB.Text=="" || txtSoDTNXB.Text=="" || txtEmailNXB.Text=="" || txtDiaChiNXB.Text=="")
            {
                MessageBox.Show("Bạn chưa nhập đủ dữ liệu!!!", "Thông báo");
            }
            else
            {
                DataRow row = ds.Tables[0].NewRow();
                row["MaNXB"] = txtMaNXB.Text;
                row["TenNXB"] = txtTenNXB.Text;
                row["DiaChi"] = txtDiaChiNXB.Text;
                row["E-mail"] = txtEmailNXB.Text;
                row["SDT"] = txtSoDTNXB.Text;
                ds.Tables[0].Rows.Add(row);

                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
            }
            txtMaNXB.Clear();
            txtTenNXB.Clear();
            txtDiaChiNXB.Clear();
            txtEmailNXB.Clear();
            txtSoDTNXB.Clear();
            txtMaNXB.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgNXB.SelectedRows[0];
            DialogResult tl;
            tl = MessageBox.Show("Bạn chắc chắn Xóa", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (tl == DialogResult.OK)
            {
                dgNXB.Rows.Remove(dr);
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgNXB.SelectedRows[0];
            dgNXB.BeginEdit(true);
            dr.Cells["MaNXB"].Value = txtMaNXB.Text;
            dr.Cells["TENNXB"].Value = txtTenNXB.Text;
            dr.Cells["DiaChi"].Value = txtDiaChiNXB.Text;
            dr.Cells["E-mail"].Value = txtEmailNXB.Text;
            dr.Cells["SDT"].Value = txtSoDTNXB.Text;
            dgNXB.EndEdit();
            MessageBox.Show("Đã cập nhập thành công", "Thông báo");
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void dgNXB_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgNXB.SelectedRows[0];
            txtMaNXB.Text = dr.Cells["MaNXB"].Value.ToString();
            txtTenNXB.Text = dr.Cells["TENNXB"].Value.ToString();
            txtDiaChiNXB.Text = dr.Cells["DiaChi"].Value.ToString();
            txtEmailNXB.Text = dr.Cells["E-mail"].Value.ToString();
            txtSoDTNXB.Text = dr.Cells["SDT"].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {

                danxb.Update(ds);
                MessageBox.Show("Đã lưu!", "Thông báo");
                dgNXB.Refresh();

            }catch
            {
                return;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables[0].RejectChanges();
        }

       
    }
}
