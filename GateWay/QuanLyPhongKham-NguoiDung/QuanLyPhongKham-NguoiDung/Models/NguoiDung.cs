using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_NguoiDung.Models
{
    public class NguoiDung
    {
        [Key]
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; } = "";
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? LoaiNguoiDung { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public bool DaXoa { get; set; } = false;

        public BacSi? BacSi { get; set; }
    }
}
