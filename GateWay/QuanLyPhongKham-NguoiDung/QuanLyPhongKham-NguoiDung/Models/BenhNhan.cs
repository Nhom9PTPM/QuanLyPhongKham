using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_NguoiDung.Models
{
    public class BenhNhan
    {
        [Key]
        public int MaBenhNhan { get; set; }
        public string HoTen { get; set; } = "";
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? CMTND_NV { get; set; }
        public DateTime NgayTao { get; set; }
        public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();

    }
}
