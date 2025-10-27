using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        // ===== CÁC CỘT CƠ BẢN =====
        public int? MaBenhNhan { get; set; }
        public int? MaNguoiThu { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public double TongTien { get; set; }
        public string? PhuongThucThanhToan { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }

        // ===== CÁC QUAN HỆ (NAVIGATION PROPERTIES) =====
        // Liên kết đến bảng BenhNhan
        [ForeignKey("MaBenhNhan")]
        public BenhNhan? BenhNhan { get; set; }

        // Một hóa đơn có nhiều chi tiết hóa đơn
        public List<ChiTietHoaDon>? ChiTietHoaDon { get; set; }
    }
}
