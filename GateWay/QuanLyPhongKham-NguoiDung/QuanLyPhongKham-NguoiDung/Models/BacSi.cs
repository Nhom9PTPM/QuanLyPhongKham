using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_NguoiDung.Models
{
    public class BacSi
    {
        [Key]
        public int MaBacSi { get; set; }

        [ForeignKey("NguoiDung")]
        public int? MaNguoiDung { get; set; }
        public NguoiDung? NguoiDung { get; set; }

        public string? ChuyenKhoa { get; set; }
        public string? BangCap { get; set; }
        public string? KinhNghiem { get; set; }
        public string? SoPhong { get; set; }
        public bool TrangThai { get; set; } = true;
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
    }
}
