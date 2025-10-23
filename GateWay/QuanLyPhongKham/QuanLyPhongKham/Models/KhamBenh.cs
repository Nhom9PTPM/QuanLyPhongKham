using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class KhamBenh
    {
        [Key]
        public int MaKham { get; set; }

        [ForeignKey("HoSoBenhAn")]
        public int? MaHoSo { get; set; }
        public HoSoBenhAn? HoSoBenhAn { get; set; }

        [ForeignKey("BenhNhan")]
        public int MaBenhNhan { get; set; }
        public BenhNhan? BenhNhan { get; set; }

        [ForeignKey("BacSi")]
        public int? MaBacSi { get; set; }
        public BacSi? BacSi { get; set; }

        public DateTime NgayKham { get; set; }
        public string? ChanDoan { get; set; }
        public string? ChiDinhXN { get; set; }
        public string? ChiDinhCLS { get; set; }
        public string? GhiChu { get; set; }
        public string? NguoiThucHien { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public bool DaXoa { get; set; } = false;

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
