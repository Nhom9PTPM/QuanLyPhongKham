using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class NhaCungCap
    {
        [Key]
        public int MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; } = "";
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? NguoiLienHe { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public bool DaXoa { get; set; } = false;

        public ICollection<Thuoc>? Thuocs { get; set; }
    }
}
