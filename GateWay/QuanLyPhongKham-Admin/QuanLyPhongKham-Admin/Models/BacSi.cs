using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_Admin.Models
{
    public class BacSi
    {
        [Key] 
        public int MaBacSi { get; set; }
        public string HoTen { get; set; } = "";
        public string? ChuyenKhoa { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? TrinhDo { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
    }
}
