using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class LichSuHoatDong
    {
        [Key]
        public int MaLichSu { get; set; }
        public int? MaTaiKhoan { get; set; }
        public string HanhDong { get; set; } = "";
        public DateTime DongThoiGian { get; set; } = DateTime.UtcNow;
        public string? DiaChiIP { get; set; }
        public string? ChiTiet { get; set; }
    }
}
