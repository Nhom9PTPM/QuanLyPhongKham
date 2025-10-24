using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("BacSi")]
    public class BacSi
    {
        [Key]
        public int MaBacSi { get; set; }

        public int? MaNguoiDung { get; set; }

        public string? ChuyenKhoa { get; set; }
        public string? BangCap { get; set; }
        public string? KinhNghiem { get; set; }
        public string? SoPhong { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
