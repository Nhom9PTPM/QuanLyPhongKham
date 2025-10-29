using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_NguoiDung.Models
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

        public virtual DonThuoc MaDonThuocNavigation { get; set; } = null!;
        public virtual Thuoc MaThuocNavigation { get; set; } = null!;
    }
}
