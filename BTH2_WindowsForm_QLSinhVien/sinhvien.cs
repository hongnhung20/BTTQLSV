using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH2_WindowsForm_QLSinhVien
{
    class sinhvien
    {
        private String masv, hoten, gioitinh, lop, diachi,hinh;
        private String ngaysinh;

        public sinhvien()
        {
        }
        public sinhvien(string masv, string hoten, string gioitinh, string lop, string diachi, string ngaysinh, string hinh)
        {
            this.masv = masv;
            this.hoten = hoten;
            this.gioitinh = gioitinh;
            this.lop = lop;
            this.diachi = diachi;
            this.ngaysinh = ngaysinh;
            this.hinh = hinh;
        }
        public string Masv { get => masv; set => masv = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public string Lop { get => lop; set => lop = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public string Hinh { get => hinh; set => hinh = value; }
    }
}
