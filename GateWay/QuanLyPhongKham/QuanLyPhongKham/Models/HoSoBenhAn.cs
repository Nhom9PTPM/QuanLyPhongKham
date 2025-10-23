using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class HoSoBenhAn
    {
        [Key]
        public int MaHoSo { get; set; }

        [ForeignKey("BenhNhan")]
        public int MaBenhNhan { get; set; }
        public BenhNhan? BenhNhan { get; set; }

        public string? TomTatBenhLy { get; set; }
        public string? ChanDoanChinh { get; set; }
        public string? LichSuBenhLy { get; set; }
        public string? TapTinDinhKem { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.UtcNow;
        public string? NguoiLap { get; set; }
        public bool DaXoa { get; set; } = false;

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
