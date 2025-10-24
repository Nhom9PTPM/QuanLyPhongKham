using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("ChiTietDonThuoc")]
    public class ChiTietDonThuoc
    {
        [Key]
        [Column("MaChiTietDon")]
        public int MaChiTietDon { get; set; }

        [Column("MaDonThuoc")]
        public int MaDonThuoc { get; set; }

        [Column("MaThuoc")]
        public int MaThuoc { get; set; }

        [Column("SoLuong")]
        public int SoLuong { get; set; }

        [Column("CachDung")]
        public string? CachDung { get; set; }

        [Column("DonGia")]
        public double? DonGia { get; set; }

        [ForeignKey(nameof(MaDonThuoc))]
        public DonThuoc? DonThuoc { get; set; }
    }
}
