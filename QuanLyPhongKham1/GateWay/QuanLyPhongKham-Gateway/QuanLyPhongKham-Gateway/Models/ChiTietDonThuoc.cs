using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonThuocService.Models
{
    public class ChiTietDonThuoc
    {
        [Key]
        public int MaChiTiet { get; set; }

        [ForeignKey("DonThuoc")]
        public int MaDonThuoc { get; set; }

        public int MaThuoc { get; set; }
        public int SoLuong { get; set; }

        [MaxLength(500)]
        public string? CachDung { get; set; }

        public decimal DonGia { get; set; }

        // Navigation
        public DonThuoc? DonThuoc { get; set; }
    }
}
