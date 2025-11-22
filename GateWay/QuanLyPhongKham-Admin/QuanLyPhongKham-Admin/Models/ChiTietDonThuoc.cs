using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("ChiTietDonThuoc")]
    public partial class ChiTietDonThuoc
    {
        public int MaChiTietDon { get; set; }
        public int MaDonThuoc { get; set; }
        public int MaThuoc { get; set; }
        public int SoLuong { get; set; }
        public string? CachDung { get; set; }
        public double? DonGia { get; set; }

        [ForeignKey("MaDonThuoc")]
        public virtual DonThuoc? MaDonThuocNavigation { get; set; }

        [ForeignKey("MaThuoc")]
        public virtual Thuoc? MaThuocNavigation { get; set; }

    }
}
