using System.Data.SqlClient;
using BenhNhanService.Models;


namespace BenhNhanService.DAL
{
    public class BenhNhanDAL
    {
        private readonly string _connectionString;
        public BenhNhanDAL(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<BenhNhan> GetAll()
        {
            var list = new List<BenhNhan>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM BenhNhan", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new BenhNhan
                {
                    maBenhNhan = (int)reader["maBenhNhan"],
                    hoTen = reader["hoTen"].ToString(),
                    ngaySinh = reader["ngaySinh"] as DateTime?,
                    gioiTinh = reader["gioiTinh"].ToString(),
                    diaChi = reader["diaChi"].ToString(),
                    soDienThoai = reader["soDienThoai"].ToString(),
                    email = reader["email"].ToString(),
                    tienSuBenh = reader["tienSuBenh"].ToString()
                });
            }
            return list;
        }

        public void Insert(BenhNhan bn)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = @"INSERT INTO BenhNhan(hoTen, ngaySinh, gioiTinh, diaChi, soDienThoai, email, tienSuBenh) 
                        VALUES(@hoTen, @ngaySinh, @gioiTinh, @diaChi, @soDienThoai, @email, @tienSuBenh)";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@hoTen", bn.hoTen);
            cmd.Parameters.AddWithValue("@ngaySinh", (object)bn.ngaySinh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@gioiTinh", (object)bn.gioiTinh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@diaChi", (object)bn.diaChi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@soDienThoai", (object)bn.soDienThoai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object)bn.email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tienSuBenh", (object)bn.tienSuBenh ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Update(BenhNhan bn)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = @"UPDATE BenhNhan SET hoTen=@hoTen, ngaySinh=@ngaySinh, gioiTinh=@gioiTinh,
                        diaChi=@diaChi, soDienThoai=@soDienThoai, email=@email, tienSuBenh=@tienSuBenh
                        WHERE maBenhNhan=@maBenhNhan";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@maBenhNhan", bn.maBenhNhan);
            cmd.Parameters.AddWithValue("@hoTen", bn.hoTen);
            cmd.Parameters.AddWithValue("@ngaySinh", (object)bn.ngaySinh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@gioiTinh", (object)bn.gioiTinh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@diaChi", (object)bn.diaChi ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@soDienThoai", (object)bn.soDienThoai ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object)bn.email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tienSuBenh", (object)bn.tienSuBenh ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = "DELETE FROM BenhNhan WHERE maBenhNhan=@id";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

    }
}
