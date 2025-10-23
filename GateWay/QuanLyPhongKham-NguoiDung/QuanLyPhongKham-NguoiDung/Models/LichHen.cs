using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuanLyPhongKham_NguoiDung.Models
{
    public class LichHen
    {
        [Key]
        public int MaLichHen { get; set; }
        public int? MaBenhNhan { get; set; }
        public int? MaBacSi { get; set; }
        public DateTime NgayHen { get; set; }
        public string? LyDoKham { get; set; }
        public string? TrangThai { get; set; } = "Chờ xác nhận";

    }
}
