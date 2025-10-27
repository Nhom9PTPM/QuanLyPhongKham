using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        [Key]
        public int MaChiTietHoaDon { get; set; }

        public int MaHoaDon { get; set; }
        public string? MaDichVu { get; set; }
        public int? MaThuoc { get; set; }
        public int? SoLuong { get; set; }
        public double? DonGia { get; set; }

        [ForeignKey("MaHoaDon")]
        public HoaDon? HoaDon { get; set; }
    }
}
