using System.Data.SqlClient;
using LichHenService.Models;

namespace LichHenService.DAL
{
    public class LichHenDAL
    {
        private readonly string _connectionString;
        public LichHenDAL(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<object> GetAllWithDetails()
        {
            var list = new List<object>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = @"SELECT lh.maLichHen, lh.ngayHen, lh.gioHen, lh.trangThai,
                               bn.hoTen AS tenBenhNhan, bs.hoTen AS tenBacSi, bs.chuyenKhoa
                        FROM LichHen lh
                        JOIN BenhNhan bn ON lh.maBenhNhan = bn.maBenhNhan
                        JOIN BacSi bs ON lh.maBacSi = bs.maBacSi";
            var cmd = new SqlCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new
                {
                    maLichHen = (int)reader["maLichHen"],
                    ngayHen = (DateTime)reader["ngayHen"],
                    gioHen = (TimeSpan)reader["gioHen"],
                    trangThai = reader["trangThai"].ToString(),
                    tenBenhNhan = reader["tenBenhNhan"].ToString(),
                    tenBacSi = reader["tenBacSi"].ToString(),
                    chuyenKhoa = reader["chuyenKhoa"].ToString()
                });
            }
            return list;
        }

        public void Insert(LichHen lh)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = @"INSERT INTO LichHen(maBenhNhan, maBacSi, ngayHen, gioHen, trangThai) 
                        VALUES(@maBenhNhan, @maBacSi, @ngayHen, @gioHen, @trangThai)";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@maBenhNhan", lh.maBenhNhan);
            cmd.Parameters.AddWithValue("@maBacSi", lh.maBacSi);
            cmd.Parameters.AddWithValue("@ngayHen", lh.ngayHen);
            cmd.Parameters.AddWithValue("@gioHen", lh.gioHen);
            cmd.Parameters.AddWithValue("@trangThai", lh.trangThai ?? "Chờ khám");
            cmd.ExecuteNonQuery();
        }

        public void Update(LichHen lh)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = @"UPDATE LichHen SET maBenhNhan=@maBenhNhan, maBacSi=@maBacSi, 
                        ngayHen=@ngayHen, gioHen=@gioHen, trangThai=@trangThai
                        WHERE maLichHen=@maLichHen";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@maLichHen", lh.maLichHen);
            cmd.Parameters.AddWithValue("@maBenhNhan", lh.maBenhNhan);
            cmd.Parameters.AddWithValue("@maBacSi", lh.maBacSi);
            cmd.Parameters.AddWithValue("@ngayHen", lh.ngayHen);
            cmd.Parameters.AddWithValue("@gioHen", lh.gioHen);
            cmd.Parameters.AddWithValue("@trangThai", lh.trangThai);
            cmd.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = "DELETE FROM LichHen WHERE maLichHen=@id";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
