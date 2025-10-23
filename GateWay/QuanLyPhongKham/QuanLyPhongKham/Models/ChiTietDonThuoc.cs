using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class ChiTietDonThuoc
    {
        [Key]
        public int MaChiTietDon { get; set; }

        [ForeignKey("DonThuoc")]
        public int MaDonThuoc { get; set; }
        public DonThuoc? DonThuoc { get; set; }

        [ForeignKey("Thuoc")]
        public int MaThuoc { get; set; }
        public Thuoc? Thuoc { get; set; }

        public int SoLuong { get; set; } = 1;
        public string? CachDung { get; set; }
        public double? DonGia { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
