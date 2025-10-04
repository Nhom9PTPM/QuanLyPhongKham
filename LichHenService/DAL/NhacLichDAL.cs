using System.Data.SqlClient;
using LichHenService.Models;


namespace LichHenService.DAL
{
    public class NhacLichDAL
    {
        private readonly string _connectionString;

        public NhacLichDAL(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection") ?? "";
        }

        public List<NhacLich> GetAllWithDetail(DateTime? from = null, DateTime? to = null, int? maBenhNhan = null)
        {
            var list = new List<NhacLich>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var sql = @"
                SELECT nl.maNhacLich, nl.maLichHen, nl.ngayGioGui, nl.hinhThuc, nl.ghiChu,
                       bn.hoTen AS tenBenhNhan, bn.soDienThoai, bn.email,
                       lh.ngayHen, lh.gioHen
                FROM NhacLich nl
                JOIN LichHen lh ON nl.maLichHen = lh.maLichHen
                JOIN BenhNhan bn ON lh.maBenhNhan = bn.maBenhNhan
                WHERE 1=1";

            if (from.HasValue) sql += " AND nl.ngayGioGui >= @from";
            if (to.HasValue) sql += " AND nl.ngayGioGui <= @to";
            if (maBenhNhan.HasValue) sql += " AND bn.maBenhNhan = @maBenhNhan";

            sql += " ORDER BY nl.ngayGioGui DESC";

            var cmd = new SqlCommand(sql, conn);

            if (from.HasValue) cmd.Parameters.AddWithValue("@from", from.Value);
            if (to.HasValue) cmd.Parameters.AddWithValue("@to", to.Value);
            if (maBenhNhan.HasValue) cmd.Parameters.AddWithValue("@maBenhNhan", maBenhNhan.Value);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new NhacLich
                {
                    maNhacLich = (int)reader["maNhacLich"],
                    maLichHen = (int)reader["maLichHen"],
                    ngayGioGui = (DateTime)reader["ngayGioGui"],
                    hinhThuc = reader["hinhThuc"].ToString(),
                    ghiChu = reader["ghiChu"].ToString(),
                    tenBenhNhan = reader["tenBenhNhan"].ToString(),
                    soDienThoai = reader["soDienThoai"].ToString(),
                    email = reader["email"].ToString(),
                    ngayHen = (DateTime)reader["ngayHen"],
                    gioHen = (TimeSpan)reader["gioHen"]
                });
            }
            return list;
        }

        // Thêm nhắc lịch
        public void Insert(NhacLich nhac)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var sql = @"INSERT INTO NhacLich(maLichHen, ngayGioGui, hinhThuc, ghiChu) 
                        VALUES(@maLichHen, @ngayGioGui, @hinhThuc, @ghiChu)";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@maLichHen", nhac.maLichHen);
            cmd.Parameters.AddWithValue("@ngayGioGui", nhac.ngayGioGui);
            cmd.Parameters.AddWithValue("@hinhThuc", nhac.hinhThuc ?? "SMS");
            cmd.Parameters.AddWithValue("@ghiChu", nhac.ghiChu ?? "Nhắc lịch tự động");
            cmd.ExecuteNonQuery();
        }
    }
}
