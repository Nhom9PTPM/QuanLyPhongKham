using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int MaChiTietHoaDon { get; set; }

        [ForeignKey("HoaDon")]
        public int MaHoaDon { get; set; }
        public HoaDon? HoaDon { get; set; }

        public string? MaDichVu { get; set; }

        [ForeignKey("Thuoc")]
        public int? MaThuoc { get; set; }
        public Thuoc? Thuoc { get; set; }

        public int? SoLuong { get; set; }
        public double? DonGia { get; set; }

        [NotMapped]
        public double ThanhTien => (SoLuong ?? 0) * (DonGia ?? 0);

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
