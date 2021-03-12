using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTH2_WindowsForm_QLSinhVien
{
    class Dataprovider
    {
        private String connstr = @"Data Source=.\;Initial Catalog=QLSINHVIEN;Integrated Security=True";
        private static Dataprovider intance;

        public static Dataprovider Intance
        {
            get
            {
                if(intance == null)
                {
                    intance = new Dataprovider();
                }
                return intance;
            }
            set { intance = value; }
        }
        public DataTable ExcuteQuery(String query)
        {
            DataTable data = new DataTable();
            using(SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(comm);
                adapter.Fill(data);
                conn.Close();
            }
            return data;
        }
        public void ExcuteNonQuery(String Query)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(Query, conn);
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }
        public void themsv(sinhvien sv)
        {
            //@masv nvarchar(10),@tensv nvarchar(100),@ngaysinh datetime,
            //@gioitinh bit, @diachi nvarchar(100),@malop varchar(100),@hinh nvarchar(100)
            String query = "exec dbo.addsv";
            query += " N'" + sv.Masv+"'";
            query += ",N'" + sv.Hoten+"'";
            query += ",'" + sv.Ngaysinh+"'";
            query += "," + sv.Gioitinh;
            query += ",N'" + sv.Diachi + "'";
            query += ",N'" + sv.Lop + "'";
            query += ",'" + sv.Hinh + "'";

            ExcuteNonQuery(query);
        }
        public void suasv(sinhvien sv)
        {
            //@masv nvarchar(10),@tensv nvarchar(100),@ngaysinh datetime,
            //@gioitinh bit, @diachi nvarchar(100),@malop varchar(100),@hinh nvarchar(100)
            String query = "exec dbo.modifysv";
            query += " N'" + sv.Masv + "'";
            query += ",N'" + sv.Hoten + "'";
            query += ",'" + sv.Ngaysinh + "'";
            query += "," + sv.Gioitinh;
            query += ",N'" + sv.Diachi + "'";
            query += ",N'" + sv.Lop + "'";
            query += ",'" + sv.Hinh + "'";

            ExcuteNonQuery(query);
        }
        public void xoa_sv(string masv)
        {
            ExcuteNonQuery("delete from dbo.SINHVIEN where MaSV = '"+masv+"'");
        }
    }
}
