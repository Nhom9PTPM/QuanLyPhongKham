using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    public class LichHen
    {
        [Key]
        public int MaLichHen { get; set; }
        public int? MaBenhNhan { get; set; }
        public DateTime? NgayHen { get; set; }
        public string? GioHen { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }
        public DateTime? NgayTao { get; set; } = DateTime.UtcNow;

        [ForeignKey("MaBenhNhan")]
        public BenhNhan? BenhNhan { get; set; }
    }
}
