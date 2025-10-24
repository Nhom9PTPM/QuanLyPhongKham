using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        public int? MaBenhNhan { get; set; }
        public int? MaNguoiThu { get; set; }
        public DateTime NgayLap { get; set; }
        public double TongTien { get; set; }
        public string? PhuongThucThanhToan { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }
    }
}
