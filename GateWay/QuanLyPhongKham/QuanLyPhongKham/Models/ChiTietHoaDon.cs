using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        public int MaChiTietHoaDon { get; set; }
        public int MaHoaDon { get; set; }
        public string? MaDichVu { get; set; }
        public int? MaThuoc { get; set; }
        public int? SoLuong { get; set; }
        public double? DonGia { get; set; }

        public virtual HoaDon MaHoaDonNavigation { get; set; } = null!;
        public virtual Thuoc? MaThuocNavigation { get; set; }
    }
}
