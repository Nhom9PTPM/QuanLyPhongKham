using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class ThongBao
    {
        [Key]
        public int MaThongBao { get; set; }

        [ForeignKey("TaiKhoan")]
        public int? MaTaiKhoan { get; set; }
        public TaiKhoan? TaiKhoan { get; set; }

        public string TieuDe { get; set; } = "";
        public string? NoiDung { get; set; }
        public bool DaDoc { get; set; } = false;
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
    }
}
