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
    public partial class frmBanSach : Form
    {
        public frmBanSach()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection();
        DataSet ds = new DataSet("tblBanSach");
        SqlDataAdapter daBanSach;
        SqlDataAdapter daNhanVien;
        SqlDataAdapter daKhachHang;
        SqlDataAdapter daSach;

        private void txtback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   

        private void frmBanSach_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLSACH;Integrated Security=True";
            Hienthidatagrid();


            string sQuerynv = @"select * from NHANVIEN";
            daNhanVien = new SqlDataAdapter(sQuerynv, conn);
            daNhanVien.Fill(ds, "tblNhanVien");
            cbMaNV.DataSource = ds.Tables["tblNhanVien"];
            cbMaNV.DisplayMember = "TenNhanVien";
            cbMaNV.ValueMember = "MaNhanVien";

            string sQuerykh = @"select * from KHACHHANG";
            daKhachHang = new SqlDataAdapter(sQuerykh, conn);
            daKhachHang.Fill(ds, "tblKhachHang");
            cbMaKH.DataSource = ds.Tables["tblKhachHang"];
            cbMaKH.DisplayMember = "TenKhachHang";
            cbMaKH.ValueMember = "MaKhachHang";

            string sQuerys = @"select * from SACH";
            daSach = new SqlDataAdapter(sQuerys, conn);
            daSach.Fill(ds, "tblSach");
            cbMaSach.DataSource = ds.Tables["tblSach"];
            cbMaSach.DisplayMember = "TenSach";
            cbMaSach.ValueMember = "MaSach";


            //Báo cáo số lượng sách bán được
            hienthisoluongban();
           

            //command Thêm 
            String sThemBS = @"insert into BanSach values(@mahoadon,@masach,@makhachhang,@manhanvien,@soluongban,@thoigian,@thanhtien)";
            SqlCommand cmThemBS = new SqlCommand(sThemBS, conn);
            cmThemBS.Parameters.Add("@mahoadon", SqlDbType.NVarChar, 50, "MaHoaDon");
            cmThemBS.Parameters.Add("@masach", SqlDbType.NVarChar, 50, "MaSach");
            cmThemBS.Parameters.Add("@makhachhang", SqlDbType.NVarChar, 50, "MaKhachHang");
            cmThemBS.Parameters.Add("@manhanvien", SqlDbType.NVarChar, 50, "MaNhanVien");
            cmThemBS.Parameters.Add("@soluongban", SqlDbType.Int, 50, "SoLuongBan");
            cmThemBS.Parameters.Add("@thoigian", SqlDbType.DateTime, 50, "ThoiGian");
            cmThemBS.Parameters.Add("@thanhtien", SqlDbType.Float, 50, "ThanhTien");
            daBanSach.InsertCommand = cmThemBS;

            // command sửa
            String sSuaBS = @"update BanSach set MaHoaDon=@mahoadon, MaKhachHang=@makhachhang, MaNhanVien=@manhanvien, SoLuongBan=@soluongban, 
                                    ThoiGian=@thoigian, ThanhTien=@thanhtien where MaHoaDon=@mahoadon";
            SqlCommand cmSuaBS = new SqlCommand(sSuaBS, conn);
            cmSuaBS.Parameters.Add("@mahoadon", SqlDbType.NVarChar, 50, "MaHoaDon");
            cmSuaBS.Parameters.Add("@masach", SqlDbType.NVarChar, 50, "MaSach");
            cmSuaBS.Parameters.Add("@makhachhang", SqlDbType.NVarChar, 50, "MaKhachHang");
            cmSuaBS.Parameters.Add("@manhanvien", SqlDbType.NVarChar, 50, "MaNhanVien");
            cmSuaBS.Parameters.Add("@soluongban", SqlDbType.Int, 50, "SoLuongBan");
            cmSuaBS.Parameters.Add("@thoigian", SqlDbType.DateTime, 50, "ThoiGian");
            cmSuaBS.Parameters.Add("@thanhtien", SqlDbType.Float, 50, "ThanhTien");
            daBanSach.UpdateCommand = cmSuaBS;

            //updatesl
            String suasl = @"update SACH set SoLuongTon=@soluongton where MaSach=@masach";
            SqlCommand cmsuas = new SqlCommand(suasl, conn);
            cmsuas.Parameters.Add("@masach", SqlDbType.NVarChar, 50, "MaSach");
            cmsuas.Parameters.Add("@soluongton", SqlDbType.Int, 10, "SoLuongTon");
            daSach.UpdateCommand = cmsuas;
            


            //command xóa 
            String sXoaBS = @"delete from BanSach where MaHoaDon=@mahoadon";
            SqlCommand cmXoaBS = new SqlCommand(sXoaBS, conn);
            cmXoaBS.Parameters.Add("@mahoadon", SqlDbType.NVarChar, 50, "MaHoaDon");
            daBanSach.DeleteCommand = cmXoaBS;

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            txtThanhTien.Enabled = false;


        }
        private void ktsl()
        {
            foreach (DataRow r in ds.Tables["tblSach"].Rows)
            {
                if (r["MaSach"] == cbMaSach.SelectedValue)
                {
                    int so2 = Int32.Parse(r["SoLuongTon"].ToString()) - Int32.Parse(txtSoLuongBan.Text);
                    if (so2 <= 10)
                    {
                        MessageBox.Show("Số lượng sách sắp hết, hãy nhập thêm vào  kho", "Thông báo");
                    }
                }
            }
        }
        private void Hienthidatagrid()
        {
            String sQueryBanSach = "Select n.*, a.TenKhachHang, b.TenSach, c.TenNhanVien From BanSach n, KhachHang a, Sach b, NhanVien c Where n.MaKhachHang = a.MaKhachHang and n.MaSach = b.MaSach and n.MaNhanVien = c.MaNhanVien";
            daBanSach = new SqlDataAdapter(sQueryBanSach, conn);
            daBanSach.Fill(ds);
            dgBansach.DataSource = ds.Tables[0];
            dgBansach.Columns["MaHoaDon"].HeaderText = "Mã Hoá Đơn";
             dgBansach.Columns["MaHoaDon"].Width = 100;

            dgBansach.Columns["TenSach"].HeaderText = "Tên Sách";
            dgBansach.Columns["TenSach"].Width = 100;

            dgBansach.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
            dgBansach.Columns["TenKhachHang"].Width = 100;

            dgBansach.Columns["TenNhanVien"].HeaderText = "Tên Nhân Viên";
            dgBansach.Columns["TenNhanVien"].Width = 100;

            dgBansach.Columns["ThoiGian"].HeaderText = "Thời Gian";
            dgBansach.Columns["ThoiGian"].Width = 150;

            dgBansach.Columns["SoLuongBan"].HeaderText = "Số Lượng Bán";
            dgBansach.Columns["SoLuongBan"].Width = 100;

            dgBansach.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            dgBansach.Columns["ThanhTien"].Width = 100;

            dgBansach.Columns["MaKhachHang"].Visible = false;
            dgBansach.Columns["MaNhanVien"].Visible = false;
            dgBansach.Columns["MaSach"].Visible = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables[0].RejectChanges();
        }

        private void hienthisoluongban()
        {
            int so = 0;
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                so += Int32.Parse(r["SoLuongBan"].ToString());


            }
            lblban.Text = so.ToString();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            daBanSach.Update(ds);
            daSach.Update(ds, "tblSach");
            MessageBox.Show("Đã lưu!", "Thông báo");
            dgBansach.Refresh();

            //Báo cáo số lượng sách bán được 
            hienthisoluongban();
            ktsl();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgBansach.SelectedRows[0];
            DialogResult tl;
            tl = MessageBox.Show("Bạn chắc chắn Xóa", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (tl == DialogResult.OK)
            {
                dgBansach.Rows.Remove(dr);
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            conn.Open();
            String hd = @"(Select Convert(NVarChar(50),MaHoaDon) from BANSACH where Convert(NVarChar(50),MaHoaDon)='" + txtMaHD.Text + "')";
            SqlCommand thd = new SqlCommand(hd, conn);
            String mhd = (String)thd.ExecuteScalar();
            conn.Close();
            if(txtMaHD.Text==mhd)
            {
                MessageBox.Show("Mã đã có sẵn, vui lòng nhập mã khác", "Cảnh báo");
                txtMaHD.Focus();
            }
        
              else if (txtMaHD.Text == "" || txtSoLuongBan.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ dữ liệu", "Cảnh báo");
            }
            else
            {
                DataRow row = ds.Tables[0].NewRow();
                row["MaHoaDon"] = txtMaHD.Text;
                row["MaSach"] = cbMaSach.SelectedValue;
                row["TenSach"] = cbMaSach.Text;
                row["MaNhanVien"] = cbMaNV.SelectedValue;
                row["TenNhanVien"] = cbMaNV.Text;
                row["MaKhachHang"] = cbMaKH.SelectedValue;
                row["TenKhachHang"] = cbMaKH.Text;
                row["ThoiGian"] = dtNgayBan.Text;
                try
                {
                    row["SoLuongBan"] = txtSoLuongBan.Text;
                }catch
                {
                    MessageBox.Show("Dữ liệu bân nhập phải là số", "Cảnh báo");
                    txtSoLuongBan.Focus();
                    return; 
                }

                

                //Tính tiền tự động
                foreach (DataRow r in ds.Tables["tblSach"].Rows)
                {
                    if (r["MaSach"] == cbMaSach.SelectedValue)
                    {
                        int so1 = Int32.Parse(r["GiaBan"].ToString()) * Int32.Parse(txtSoLuongBan.Text);
                        txtThanhTien.Text = so1.ToString();
                    }
                }
                row["ThanhTien"] = txtThanhTien.Text;
               
               
                

                ds.Tables[0].Rows.Add(row);

                dgBansach.Columns["TenSach"].HeaderText = "Tên Sách";
                dgBansach.Columns["TenSach"].Width = 100;

                dgBansach.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
                dgBansach.Columns["TenKhachHang"].Width = 100;

                dgBansach.Columns["TenNhanVien"].HeaderText = "Tên Nhân Viên";
                dgBansach.Columns["TenNhanVien"].Width = 100;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
            }

            foreach (DataRow r in ds.Tables["tblSach"].Rows)
            { 
                if(r["MaSach"]==cbMaSach.SelectedValue)
                {
                    int so = Int32.Parse(r["SoLuongTon"].ToString()) - Int32.Parse(txtSoLuongBan.Text);
                    r["SoLuongTon"] = so.ToString();
                }
            }


            txtMaHD.Clear();
            txtSoLuongBan.Clear();
            txtThanhTien.Clear();
            txtMaHD.Focus();
                
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgBansach.SelectedRows[0];
            dgBansach.BeginEdit(true);
            dr.Cells["MaHoaDon"].Value = txtMaHD.Text;
            dr.Cells["MaSach"].Value = cbMaSach.SelectedValue; 
            dr.Cells["MaNhanVien"].Value = cbMaNV.SelectedValue;
            dr.Cells["MaKhachHang"].Value = cbMaKH.SelectedValue;
            dr.Cells["ThoiGian"].Value = dtNgayBan.Text;
            dr.Cells["SoLuongBan"].Value = txtSoLuongBan.Text;
            foreach (DataRow r in ds.Tables["tblSach"].Rows)
            {
                if (r["MaSach"] == cbMaSach.SelectedValue)
                {
                    int so1 = Int32.Parse(r["GiaBan"].ToString()) * Int32.Parse(txtSoLuongBan.Text);
                    txtThanhTien.Text = so1.ToString();
                }
            }
            dr.Cells["ThanhTien"].Value = txtThanhTien.Text;
            dr.Cells["TenSach"].Value = cbMaSach.Text;
            dr.Cells["TenNhanVien"].Value = cbMaNV.Text;
            dr.Cells["TenKhachHang"].Value = cbMaKH.Text;
            dgBansach.EndEdit();
            

            MessageBox.Show("Đã cập nhập thành công", "Thông báo");

            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void dgBansach_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgBansach.SelectedRows[0];
            txtMaHD.Text = dr.Cells["MaHoaDon"].Value.ToString();
            cbMaSach.SelectedValue = dr.Cells["MaSach"].Value.ToString();
            cbMaNV.SelectedValue = dr.Cells["MaNhanVien"].Value.ToString();
            cbMaKH.SelectedValue = dr.Cells["MaKhachHang"].Value.ToString();
            dtNgayBan.Text = dr.Cells["ThoiGian"].Value.ToString();
            txtSoLuongBan.Text = dr.Cells["SoLuongBan"].Value.ToString();
            txtThanhTien.Text = dr.Cells["ThanhTien"].Value.ToString();
        }

       
    }
}
