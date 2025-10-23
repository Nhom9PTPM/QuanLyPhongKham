using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        [ForeignKey("BenhNhan")]
        public int? MaBenhNhan { get; set; }
        public BenhNhan? BenhNhan { get; set; }

        public int? MaNguoiThu { get; set; } 
        public DateTime NgayLap { get; set; } = DateTime.UtcNow;
        public double TongTien { get; set; } = 0;
        public string? PhuongThucThanhToan { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }

    }
}
