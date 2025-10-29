using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_NguoiDung.Models
{
    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
            BacSis = new HashSet<BacSi>();
        }

        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; } = null!;
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? LoaiNguoiDung { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
        public bool DaXoa { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
        public virtual ICollection<BacSi> BacSis { get; set; }
    }
}
