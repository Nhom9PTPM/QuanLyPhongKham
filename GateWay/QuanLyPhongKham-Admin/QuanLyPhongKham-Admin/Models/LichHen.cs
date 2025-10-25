using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace QuanLyPhongKham_Admin.Models
{
    [Table("LichHen")]
    public partial class LichHen
    {
        public int MaLichHen { get; set; }
        public int? MaBenhNhan { get; set; }
        public int? MaBacSi { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? GioHen { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }
        public string? NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public bool DaXoa { get; set; }

        public virtual BenhNhan MaBenhNhanNavigation { get; set; }

        public virtual BacSi? MaBacSiNavigation { get; set; }

    }
}
