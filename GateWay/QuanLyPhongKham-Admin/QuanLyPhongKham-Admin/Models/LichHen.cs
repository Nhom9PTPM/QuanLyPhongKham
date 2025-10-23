using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_Admin.Models
{
    public class LichHen
    {
        [Key]
        public int MaLichHen { get; set; }

        [ForeignKey("BenhNhan")]
        public int MaBenhNhan { get; set; }
        public BenhNhan? BenhNhan { get; set; }

        [ForeignKey("BacSi")]
        public int? MaBacSi { get; set; }
        public BacSi? BacSi { get; set; }

        public DateTime NgayHen { get; set; }
        public string? GioHen { get; set; }
        public string? TrangThai { get; set; } = "DaDat";
        public string? GhiChu { get; set; }
        public string? NguoiTao { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public bool DaXoa { get; set; } = false;

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
