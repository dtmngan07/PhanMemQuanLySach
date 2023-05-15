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
    public partial class tacgia : Form
    {
        public tacgia()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection();
        DataSet ds = new DataSet("TacGia");
        SqlDataAdapter daTG;

        private void btnTroLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tacgia_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True";
            Hienthidatagrid();

            //command Thêm tác giả;
            String sThemTG = @"insert into TACGIA values(@MaTacgia, @TenTacgia, @Namsinh, @sdt,@email,@diaChi)";
            SqlCommand cmThemtg = new SqlCommand(sThemTG, conn);
            cmThemtg.Parameters.Add("@MaTacgia", SqlDbType.NVarChar, 50, "MaTacGia");
            cmThemtg.Parameters.Add("@TenTacgia", SqlDbType.NVarChar, 50, "TenTacGia");
            cmThemtg.Parameters.Add("@Namsinh", SqlDbType.Date, 50, "NamSinh");
            cmThemtg.Parameters.Add("@sdt", SqlDbType.NChar, 10, "SDT");
            cmThemtg.Parameters.Add("@email", SqlDbType.NVarChar, 50, "E-mail");
            cmThemtg.Parameters.Add("@diaChi", SqlDbType.NVarChar, 50, "DiaChi");
            daTG.InsertCommand = cmThemtg;

            // command sửa tác giả
            String sSuaTG = @"update TACGIA set TenTacGia=@TenTacgia, NamSinh=@Namsinh, SDT=@sdt, [E-mail]=@email, DiaChi=@diaChi where MaTacGia=@MaTacgia";
            SqlCommand cmSuatg = new SqlCommand(sSuaTG, conn);
            cmSuatg.Parameters.Add("@MaTacgia", SqlDbType.NVarChar, 50, "MaTacGia");
            cmSuatg.Parameters.Add("@TenTacgia", SqlDbType.NVarChar, 50, "TenTacGia");
            cmSuatg.Parameters.Add("@Namsinh", SqlDbType.Date, 50, "NamSinh");
            cmSuatg.Parameters.Add("@sdt", SqlDbType.NChar, 10, "SDT");
            cmSuatg.Parameters.Add("@email", SqlDbType.NVarChar, 50, "E-mail");
            cmSuatg.Parameters.Add("@diaChi", SqlDbType.NVarChar, 50, "DiaChi");
            daTG.UpdateCommand = cmSuatg;

            //command xóa tác giả
            String sXoaTG = @"delete from TACGIA where MaTacGia=@MaTacgia";
            SqlCommand cmxoatg = new SqlCommand(sXoaTG, conn);
            cmxoatg.Parameters.Add("@MaTacgia", SqlDbType.NVarChar, 50, "MaTacGia");
            daTG.DeleteCommand = cmxoatg;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }  
            //Hiển thị datagridview
        private void Hienthidatagrid()
        {
            String  sQueryTG = @"select * from TACGIA";
            daTG = new SqlDataAdapter(sQueryTG, conn);
            daTG.Fill(ds);
            dgtacgia.DataSource=ds.Tables[0];
            dgtacgia.Columns["MaTacGia"].HeaderText = "Mã tác giả";
            dgtacgia.Columns["MaTacGia"].Width = 100;
            dgtacgia.Columns["TenTacGia"].HeaderText = "Tên tác giả";
            dgtacgia.Columns["TenTacGia"].Width = 250;
            dgtacgia.Columns["NamSinh"].HeaderText = "Năm sinh";
            dgtacgia.Columns["NamSinh"].Width = 80;
            dgtacgia.Columns["SDT"].HeaderText = "Số điện thoại";
            dgtacgia.Columns["SDT"].Width = 130;
            dgtacgia.Columns["E-mail"].HeaderText = "Email";
            dgtacgia.Columns["E-mail"].Width = 150;
            dgtacgia.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgtacgia.Columns["DiaChi"].Width = 300;
        }

       

        private void btnThem_Click(object sender, EventArgs e)
        {
            conn.Open();
            String hd = @"(Select Convert(NVarChar(50),MaTacGia) from TACGIA where Convert(NVarChar(50),MaTacGia)='" + txtMaTG.Text + "')";
            SqlCommand thd = new SqlCommand(hd, conn);
            String mhd = (String)thd.ExecuteScalar();
            conn.Close();
            if (txtMaTG.Text == mhd)
            {
                MessageBox.Show("Mã đã có sẵn, vui lòng nhập mã khác", "Cảnh báo");
                txtMaTG.Focus();
            }

            else if (txtMaTG.Text=="" || txtTenTG.Text == "" || txtEmailTG.Text == "" || txtDiacChiTG.Text == "" || txtSoDTTG.Text == "" )
            {
                MessageBox.Show("Bạn chưa nhập đủ dữ liệu!!!", "Thông báo");
            }
            else
            {
                DataRow row = ds.Tables[0].NewRow();
                row["MaTacGia"] = txtMaTG.Text;
                row["TenTacGia"] = txtTenTG.Text;
                row["NamSinh"] = dtNamSinh.Value.ToString();
                row["SDT"] = txtSoDTTG.Text;
                row["E-mail"] = txtEmailTG.Text;
                row["DiaChi"] = txtDiacChiTG.Text;
                ds.Tables[0].Rows.Add(row);

                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
            }
            txtMaTG.Clear();
            txtTenTG.Clear();
            txtSoDTTG.Clear();
            txtEmailTG.Clear();
            txtDiacChiTG.Clear();
            txtMaTG.Focus();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgtacgia.SelectedRows[0];
            DialogResult tl;
            tl = MessageBox.Show("Bạn chắc chắn Xóa", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (tl == DialogResult.OK)
            {
                dgtacgia.Rows.Remove(dr);
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgtacgia.SelectedRows[0];
            dgtacgia.BeginEdit(true);
            dr.Cells["MaTacGia"].Value = txtMaTG.Text;
            dr.Cells["TenTacGia"].Value = txtTenTG.Text;
            dr.Cells["NamSinh"].Value = dtNamSinh.Value.ToString();
            dr.Cells["SDT"].Value = txtSoDTTG.Text;
            dr.Cells["E-mail"].Value = txtEmailTG.Text;
            dr.Cells["DiaChi"].Value = txtDiacChiTG.Text;
            dgtacgia.EndEdit();
            MessageBox.Show("Đã cập nhập thành công", "Thông báo");
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void dgtacgia_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgtacgia.SelectedRows[0];
            txtMaTG.Text = dr.Cells["MaTacGia"].Value.ToString();
            txtTenTG.Text = dr.Cells["TenTacGia"].Value.ToString();
            dtNamSinh.Text= dr.Cells["NamSinh"].Value.ToString();
            txtSoDTTG.Text = dr.Cells["SDT"].Value.ToString();
            txtEmailTG.Text = dr.Cells["E-mail"].Value.ToString();
            txtDiacChiTG.Text = dr.Cells["DiaChi"].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daTG.Update(ds);
                MessageBox.Show("Đã lưu!", "Thông báo");
                dgtacgia.Refresh();

            }
            catch
            {
                return;
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables[0].RejectChanges();
        }
    }
}
