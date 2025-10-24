using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_Admin.Models
{
    public class LichHen
    {
        [Key]
        public int MaLichHen { get; set; }

        public int MaBenhNhan { get; set; }
        public int? MaBacSi { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? GioHen { get; set; }
        public string? TrangThai { get; set; }     // "DaDat", "DaKham", "Huy"
        public string? GhiChu { get; set; }
        public string? NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public bool DaXoa { get; set; }
    }
}
