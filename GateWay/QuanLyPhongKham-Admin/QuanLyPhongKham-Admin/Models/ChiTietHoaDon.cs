using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        [Key]
        [Column("MaChiTietHoaDon")]
        public int MaChiTietHoaDon { get; set; }

        [Column("MaHoaDon")]
        public int MaHoaDon { get; set; }

        [Column("MaDichVu")]
        public int? MaDichVu { get; set; }

        [Column("MaThuoc")]
        public int? MaThuoc { get; set; }

        [Column("SoLuong")]
        public int? SoLuong { get; set; }

        [Column("DonGia")]
        public double? DonGia { get; set; }

        [Column("ThanhTien")]
        public double? ThanhTien { get; set; }

      

        [ForeignKey(nameof(MaHoaDon))]
        public HoaDon? HoaDon { get; set; }

        [ForeignKey(nameof(MaThuoc))]
        public Thuoc? Thuoc { get; set; }
    }
}
