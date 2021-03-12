using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTH2_WindowsForm_QLSinhVien
{
    public partial class Form1 : Form
    {
        String imagename = "anhhocsinh.jpg";
        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
        }

        private void txtTimkiem_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            loadthongtin();
            dgDSSV.ClearSelection();
        }
        public void setunable()
        {
            txtHoten.Enabled = false;
            txtMaSV.Enabled = false;
            txtDiachi.Enabled = false;
            cbLop.Enabled = false;
            dtpNgaysinh.Enabled = false;
            btnChonhinh.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        public void setenable()
        {
            txtHoten.Enabled = true;
            txtMaSV.Enabled = true;
            txtDiachi.Enabled = true;
            cbLop.Enabled = true;
            dtpNgaysinh.Enabled = true;
            btnChonhinh.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }
        public void loadthongtin()
        {
            
            cbLop.DataSource = Dataprovider.Intance.ExcuteQuery("select * from dbo.LOP");
            cbLop.DisplayMember = "MaLop";
            cbLop.ValueMember = "MaLop";
            dgDSSV.DataSource = Dataprovider.Intance.ExcuteQuery("select MaSV,HoTen as 'Họ và Tên',NgaySinh as N'Ngày sinh',case GioiTinh " +
                "when 0 then N'Nữ' when 1 then N'Nam'end as N'Giới tính',DiaChi as N'Địa chỉ',MaLop as N'Mã lớp' from dbo.SINHVIEN");
            dgDSSV.ClearSelection();
        }

        private void dgDSSV_SelectionChanged(object sender, EventArgs e)
        {
            setunable();
            try
            {
                txtMaSV.Text = dgDSSV.SelectedCells[0].Value.ToString() ;
                txtHoten.Text = dgDSSV.SelectedCells[1].Value.ToString();
                txtDiachi.Text = dgDSSV.SelectedCells[4].Value.ToString();
                cbLop.Text = dgDSSV.SelectedCells[5].Value.ToString();
                if(dgDSSV.SelectedCells[3].Value.ToString()=="Nam")
                {
                    rdNam.Checked = true;
                }
                else
                {
                    rdNu.Checked = true;
                }

            
                dtpNgaysinh.Value = DateTime.Parse(dgDSSV.SelectedCells[2].Value.ToString());
                DataTable anhhocsinh = Dataprovider.Intance.ExcuteQuery("select dbo.GetImageSV('"+txtMaSV.Text+"')");
                            String Hinh = anhhocsinh.Rows[0].Field<String>(0);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; 
                            pictureBox1.Image = Image.FromFile("../../Resources/anhhocsinh/"+Hinh);
                }
            catch { }
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            dgDSSV.ClearSelection();
            setenable();
            txtMaSV.Text = "";
            txtHoten.Text = "";
            txtDiachi.Text = "";
            cbLop.SelectedItem = "";
            rdNam.Checked = true;
            dtpNgaysinh.Value = DateTime.Now;
            pictureBox1.Image = Image.FromFile("../../Resources/anhhocsinh/anhhocsinh.jpg");
            pictureBox1.Update();
            btnChonhinh.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult resultclose = MessageBox.Show(this, "Xác nhận đóng", "Thoát", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
            if(resultclose == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if(txtTimkiem.Text.Length == 0)
            {
                MessageBox.Show("Chưa nhập mã sinh viên !");
            }
            else
            {
                dgDSSV.ClearSelection();
                try
                {
                    dgDSSV.DataSource = null;
                    dgDSSV.DataSource = Dataprovider.Intance.ExcuteQuery("select MaSV,HoTen as 'Họ và Tên',NgaySinh as N'Ngày sinh',case GioiTinh " +
                    "when 0 then N'Nữ' when 1 then N'Nam'end as N'Giới tính',DiaChi as N'Địa chỉ',MaLop as N'Mã lớp' from dbo.SINHVIEN where MaSV = '" +txtTimkiem.Text+"'");
                    if(dgDSSV.Rows.Count == 1)
                    {
                        MessageBox.Show("Mã sinh viên Không tồn tại ");
                        loadthongtin();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Mã sinh viên Không tồn tại ");
                }
            }
            
           
        }

        private void txtTimkiem_MouseDown(object sender, MouseEventArgs e)
        { 
        }

        private void btnChonhinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                imagename = dialog.SafeFileName;
                pictureBox1.Image = Image.FromFile(path);
                //using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                //{
                    
                //}
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,"Đã hủy","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
           
            loadthongtin();
            dgDSSV.ClearSelection();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            setenable();
            txtMaSV.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //batdau:
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            sinhvien sv = new sinhvien();
            sv.Masv = txtMaSV.Text;
            DataTable dt_test = new DataTable();
            String query = "select * from dbo.SINHVIEN where MaSV = '"+sv.Masv+"'";
            dt_test = Dataprovider.Intance.ExcuteQuery(query);
            int tontai = dt_test.Rows.Count;
            
            sv.Hoten = txtHoten.Text;
            sv.Lop = cbLop.SelectedValue.ToString();
            if(rdNam.Checked == true)
            {
                sv.Gioitinh = 1+"";
            }
            else
            {
                sv.Gioitinh = 0 + "";
            }
            sv.Diachi = txtDiachi.Text;
            sv.Ngaysinh = dtpNgaysinh.Value.ToShortDateString();
            sv.Hinh = imagename;
            if(tontai > 0 && txtMaSV.Enabled == true)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại");
                txtMaSV.Text = "";
                txtMaSV.Select();
            } else if(tontai == 0)
            {
               
                Dataprovider.Intance.themsv(sv);
                MessageBox.Show("Thêm thành công !");
                loadthongtin();
                dgDSSV.ClearSelection();
            }
            else
            {
                Dataprovider.Intance.suasv(sv);
                MessageBox.Show("Sửa thành công !");
                loadthongtin();
                dgDSSV.ClearSelection();
            }

            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Dataprovider.Intance.xoa_sv(txtMaSV.Text);
            dgDSSV.ClearSelection();
            setenable();
            txtMaSV.Text = "";
            txtHoten.Text = "";
            txtDiachi.Text = "";
            cbLop.SelectedItem = "";
            rdNam.Checked = true;
            dtpNgaysinh.Value = DateTime.Now;
            pictureBox1.Image = Image.FromFile("../../Resources/anhhocsinh/anhhocsinh.jpg");
            pictureBox1.Update();
            btnChonhinh.Enabled = true;
            loadthongtin();
            dgDSSV.ClearSelection();
        }
    }
}
