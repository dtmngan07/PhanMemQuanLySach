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
    public partial class SACH : Form
    {
        public SACH()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection();
        DataSet ds = new DataSet("tblSach");
        SqlDataAdapter dasach;
        SqlDataAdapter daTheloai;
        SqlDataAdapter daNXB;
        SqlDataAdapter daTacgia;


        private void txtback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SACH_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True";
            Hienthidatagrid();

            string sQuerytl = @"select * from THELOAI";
            daTheloai = new SqlDataAdapter(sQuerytl, conn);
            daTheloai.Fill(ds, "tblTheloai");
            cbtl.DataSource = ds.Tables["tblTheloai"];
            cbtl.DisplayMember = "TenTheLoai";
            cbtl.ValueMember = "MaTheLoai";

            string sQuerytg = @"select * from TACGIA";
            daTacgia = new SqlDataAdapter(sQuerytg, conn);
            daTacgia.Fill(ds, "tblTacGia");
            cbtg.DataSource = ds.Tables["tblTacGia"];
            cbtg.DisplayMember = "TenTacGia";
            cbtg.ValueMember = "MaTacGia";

            string sQuerynxb = @"select * from NXB";
            daNXB = new SqlDataAdapter(sQuerynxb, conn);
            daNXB.Fill(ds, "tblNXB");
            cbnxb.DataSource = ds.Tables["tblNXB"];
            cbnxb.DisplayMember = "TenNXB";
            cbnxb.ValueMember = "MaNXB";


            //command Thêm thể loại;
            String sThemS = @"insert into SACH values(@masach, @tensach,@matacgia,@matheloai,@manxb,@namxb,@lantaiban,@gianhap,@giaban,@slton)";
            SqlCommand cmThems = new SqlCommand(sThemS, conn);
            cmThems.Parameters.Add("@masach", SqlDbType.NVarChar, 50, "MaSach");
            cmThems.Parameters.Add("@tensach", SqlDbType.NVarChar, 50, "TenSach");
            cmThems.Parameters.Add("@matacgia", SqlDbType.NVarChar, 50, "MaTacGia");
            cmThems.Parameters.Add("@matheloai", SqlDbType.NVarChar, 50, "MaTheLoai");
            cmThems.Parameters.Add("@manxb", SqlDbType.NVarChar, 50, "MaNXB");
            cmThems.Parameters.Add("@namxb", SqlDbType.Date, 50, "NamXB");
            cmThems.Parameters.Add("@lantaiban", SqlDbType.Int, 50, "LanTaiBan");
            cmThems.Parameters.Add("@gianhap", SqlDbType.Float, 50, "GiaNhap");
            cmThems.Parameters.Add("@giaban", SqlDbType.Float, 50, "GiaBan");
            cmThems.Parameters.Add("@slton", SqlDbType.Int, 50, "SoLuongTon");
            dasach.InsertCommand = cmThems;

            // command sửa thể loại
            String sSuaS = @"update SACH set TenSach=@tensach, MaTacGia=@matacgia, MaTheLoai=@matheloai, MaNXB=@manxb, 
NamXB=@namxb,LanTaiBan=@lantaiban, GiaNhap=@gianhap, GiaBan=@giaban, SoLuongTon=@slton  where MaSach=@masach";
            SqlCommand cmSuas = new SqlCommand(sSuaS, conn);
            cmSuas.Parameters.Add("@masach", SqlDbType.NVarChar, 50, "MaSach");
            cmSuas.Parameters.Add("@tensach", SqlDbType.NVarChar, 50, "TenSach");
            cmSuas.Parameters.Add("@matacgia", SqlDbType.NVarChar, 50, "MaTacGia");
            cmSuas.Parameters.Add("@matheloai", SqlDbType.NVarChar, 50, "MaTheLoai");
            cmSuas.Parameters.Add("@manxb", SqlDbType.NVarChar, 50, "MaNXB");
            cmSuas.Parameters.Add("@namxb", SqlDbType.Date, 50, "NamXB");
            cmSuas.Parameters.Add("@lantaiban", SqlDbType.Int, 50, "LanTaiBan");
            cmSuas.Parameters.Add("@gianhap", SqlDbType.Float, 50, "GiaNhap");
            cmSuas.Parameters.Add("@giaban", SqlDbType.Float, 50, "GiaBan");
            cmSuas.Parameters.Add("@slton", SqlDbType.Int, 50, "SoLuongTon");
            dasach.UpdateCommand = cmSuas;

            //command xóa thể loại
            String sXoaS = @"delete from SACH where MaSach=@masach";
            SqlCommand cmxoas = new SqlCommand(sXoaS, conn);
            cmxoas.Parameters.Add("@masach", SqlDbType.NVarChar, 50, "MaSach");
            dasach.DeleteCommand = cmxoas;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        private void Hienthidatagrid()
        {
            String sQuerySach = @"Select n.*, a.TenTheLoai, b.TenTacGia, c.TenNXB From Sach n,  TheLoai a, TacGia b, NXB c Where n.MaTheLoai=a.MaTheLoai and n.MaTacGia=b.MaTacGia and n.MaNXB=c.MaNXB";
            dasach = new SqlDataAdapter(sQuerySach, conn);
            dasach.Fill(ds);
            dgsach.DataSource = ds.Tables[0];
            dgsach.Columns["MaSach"].HeaderText = "Mã Sách";
            dgsach.Columns["MaSach"].Width = 80;
            dgsach.Columns["TenSach"].HeaderText = "Tên Sách";
            dgsach.Columns["TenSach"].Width = 80;

            dgsach.Columns["TenTacGia"].HeaderText = "Tên tác giả";
            dgsach.Columns["TenTacGia"].Width = 80;
            dgsach.Columns["TenNXB"].HeaderText = "Tên Nhà xuất bản";
            dgsach.Columns["TenNXB"].Width = 80;
            dgsach.Columns["TenTheLoai"].HeaderText = "Tên Thể loại";
            dgsach.Columns["TenTheLoai"].Width = 80;

            dgsach.Columns["MaTacGia"].Visible = false;

            dgsach.Columns["MaTheLoai"].Visible = false;

            dgsach.Columns["MaNXB"].Visible = false;

            dgsach.Columns["NamXB"].HeaderText = "Năm Xuất bản";
            dgsach.Columns["NamXB"].Width = 120;
            dgsach.Columns["LanTaiBan"].HeaderText = "Lần tái bản";
            dgsach.Columns["LanTaiBan"].Width = 60;
            dgsach.Columns["GiaNhap"].HeaderText = "Giá nhập";
            dgsach.Columns["GiaNhap"].Width = 60;
            dgsach.Columns["GiaBan"].HeaderText = "Giá Bán";
            dgsach.Columns["GiaBan"].Width = 60;
            dgsach.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
            dgsach.Columns["SoLuongTon"].Width = 80;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgsach.SelectedRows[0];
            DialogResult tl;
            tl = MessageBox.Show("Bạn chắc chắn Xóa", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (tl == DialogResult.OK)
            {
                dgsach.Rows.Remove(dr);
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                dasach.Update(ds);
                MessageBox.Show("Đã lưu!", "Thông báo");
                dgsach.Refresh();
            }
            catch
            {
                return;
            }
        }

        private void dgsach_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgsach.SelectedRows[0];
            txtMaSach.Text = dr.Cells["MaSach"].Value.ToString();
            txtTenSach.Text = dr.Cells["TenSach"].Value.ToString();
            cbnxb.SelectedValue = dr.Cells["MaNXB"].Value.ToString();
            cbtg.SelectedValue = dr.Cells["MaTacGia"].Value.ToString();
            cbtl.SelectedValue = dr.Cells["MaTheLoai"].Value.ToString();
            dtNamXB.Text = dr.Cells["NamXB"].Value.ToString();
            txtLanTaiBan.Text = dr.Cells["LanTaiBan"].Value.ToString();
            txtGiaBan.Text = dr.Cells["GiaBan"].Value.ToString();
            txtGiaNhap.Text = dr.Cells["GiaNhap"].Value.ToString();
            txtSoLuong.Text = dr.Cells["SoLuongTon"].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgsach.SelectedRows[0];
            dgsach.BeginEdit(true);
            dr.Cells["MaSach"].Value = txtMaSach.Text;
            dr.Cells["TenSach"].Value = txtTenSach.Text;
            dr.Cells["MaNXB"].Value = cbnxb.SelectedValue;
            dr.Cells["MaTacGia"].Value = cbtg.SelectedValue;
            dr.Cells["MaTheLoai"].Value = cbtl.SelectedValue;
            dr.Cells["NamXB"].Value = dtNamXB.Text;
            dr.Cells["LanTaiBan"].Value = txtLanTaiBan.Text;
            dr.Cells["GiaBan"].Value = txtGiaBan.Text;
            dr.Cells["GiaNhap"].Value = txtGiaNhap.Text;
            dr.Cells["SoLuongTon"].Value = txtSoLuong.Text;
            dr.Cells["TenNXB"].Value = cbnxb.Text;
            dr.Cells["TenTacGia"].Value = cbtg.Text;
            dr.Cells["TenTheLoai"].Value = cbtl.Text;


            dgsach.EndEdit();



            MessageBox.Show("Đã cập nhập thành công", "Thông báo");
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

            ds.Tables[0].RejectChanges();
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            try
            {
                dasach.Update(ds);
                MessageBox.Show("Đã lưu!", "Thông báo");
                dgsach.Refresh();
            }
            catch
            {
                return;
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            conn.Open();
            String hd = @"(Select Convert(NVarChar(50),MaSach) from SACH where Convert(NVarChar(50),MaSach)='" + txtMaSach.Text + "')";
            SqlCommand thd = new SqlCommand(hd, conn);
            String mhd = (String)thd.ExecuteScalar();

            conn.Close();
            if (txtMaSach.Text== mhd)
            {
                MessageBox.Show("Mã đã có sẵn, vui lòng nhập mã khác", "Cảnh báo");
                txtMaSach.Focus();
            }
            else if (txtMaSach.Text == "" || txtTenSach.Text == "" || txtLanTaiBan.Text == "" || txtGiaBan.Text == "" || txtGiaNhap.Text == "" || txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ dữ liệu", "Cảnh báo");
            }
            else
            {
                DataRow row = ds.Tables[0].NewRow();
                row["MaSach"] = txtMaSach.Text;
                row["TenSach"] = txtTenSach.Text;
                row["MaNXB"] = cbnxb.SelectedValue;
                row["MaTacGia"] = cbtg.SelectedValue;
                row["MaTheLoai"] = cbtl.SelectedValue;
                row["TenNXB"] = cbnxb.Text;
                row["TenTacGia"] = cbtg.Text;
                row["TenTheLoai"] = cbtl.Text;
                row["NamXB"] = dtNamXB.Text;

                try
                {
                    row["LanTaiBan"] = txtLanTaiBan.Text;
                    row["GiaBan"] = txtGiaBan.Text;
                    row["GiaNhap"] = txtGiaNhap.Text;
                    row["SoLuongTon"] = txtSoLuong.Text;
                }catch(Exception )
                {
                    MessageBox.Show("Dữ liệu bân nhập phải là số", "Cảnh báo");
                    return;
                }

                ds.Tables[0].Rows.Add(row);

                dgsach.Columns["TenNXB"].HeaderText = "Tên NXB";
                dgsach.Columns["TenNXB"].Width = 100;
                dgsach.Columns["TenTacGia"].HeaderText = "Tên tác giả";
                dgsach.Columns["TenTacGia"].Width = 100;
                dgsach.Columns["TenTheLoai"].HeaderText = "Tên thể loại";
                dgsach.Columns["TenTheLoai"].Width = 100;

                btnLuu.Enabled = true;
                btnHuy.Enabled = true;

            }
            
            txtMaSach.Clear();
            txtTenSach.Clear();
            txtLanTaiBan.Clear();
            txtGiaBan.Clear();
            txtGiaNhap.Clear();
            txtSoLuong.Clear();
            txtMaSach.Focus();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            conn.Open();
            String sql = @"Select n.*, a.TenTheLoai, b.TenTacGia, c.TenNXB From Sach n, TheLoai a, TacGia b, NXB c Where n.MaTheLoai = a.MaTheLoai and n.MaTacGia = b.MaTacGia and n.MaNXB = c.MaNXB and n.MaSach=@MaSach";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Masach", txttimkiem.Text);
            SqlDataAdapter ten = new SqlDataAdapter(cmd);
            DataTable ht = new DataTable();
            ten.Fill(ht);
            if (ht.Rows.Count == 0)
            {
                MessageBox.Show("Không có sách cần tìm", "Thông báo");
            }
            else
            {
                dgsach.DataSource = ht;
                dgsach.Columns["TenNXB"].HeaderText = "Tên NXB";
                dgsach.Columns["TenNXB"].Width = 100;
                dgsach.Columns["TenTacGia"].HeaderText = "Tên tác giả";
                dgsach.Columns["TenTacGia"].Width = 100;
                dgsach.Columns["TenTheLoai"].HeaderText = "Tên thể loại";
                dgsach.Columns["TenTheLoai"].Width = 100;

            }
            conn.Close();
            return;
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            String sQuerySach = @"Select n.*, a.TenTheLoai, b.TenTacGia, c.TenNXB From Sach n,  TheLoai a, TacGia b, NXB c Where n.MaTheLoai=a.MaTheLoai and n.MaTacGia=b.MaTacGia and n.MaNXB=c.MaNXB";
            dasach = new SqlDataAdapter(sQuerySach, conn);
            dgsach.DataSource = ds.Tables[0];
            dgsach.Columns["TenNXB"].HeaderText = "Tên NXB";
            dgsach.Columns["TenNXB"].Width = 100;
            dgsach.Columns["TenTacGia"].HeaderText = "Tên tác giả";
            dgsach.Columns["TenTacGia"].Width = 100;
            dgsach.Columns["TenTheLoai"].HeaderText = "Tên thể loại";
            dgsach.Columns["TenTheLoai"].Width = 100;
        }

        
    }
}
