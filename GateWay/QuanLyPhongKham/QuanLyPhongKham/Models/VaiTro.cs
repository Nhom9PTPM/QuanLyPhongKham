using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class VaiTro
    {
        [Key]
        public int MaVaiTro { get; set; }
        public string TenVaiTro { get; set; } = "";
        public string? MoTa { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public bool DaXoa { get; set; } = false;

        public ICollection<TaiKhoan>? TaiKhoans { get; set; }
    }
}
