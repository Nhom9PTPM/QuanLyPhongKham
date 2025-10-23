using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class DonThuoc
    {
        [Key]
        public int MaDonThuoc { get; set; }

        [ForeignKey("KhamBenh")]
        public int? MaKham { get; set; }
        public KhamBenh? KhamBenh { get; set; }

        [ForeignKey("BacSi")]
        public int? MaBacSi { get; set; }
        public BacSi? BacSi { get; set; }

        public DateTime NgayKe { get; set; } = DateTime.UtcNow;
        public string? GhiChu { get; set; }
        public string? NguoiKe { get; set; }
        public bool DaXoa { get; set; } = false;

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public ICollection<ChiTietDonThuoc>? ChiTietDonThuocs { get; set; }
    }
}
