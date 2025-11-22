using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("DonThuoc")]
    public partial class DonThuoc
    {
        public DonThuoc()
        {
            ChiTietDonThuocs = new HashSet<ChiTietDonThuoc>();
        }

        public int MaDonThuoc { get; set; }
        public int? MaKham { get; set; }
        public int? MaBacSi { get; set; }
        public DateTime NgayKe { get; set; } = DateTime.Now;
        public string? GhiChu { get; set; }
        public string? NguoiKe { get; set; }
        public bool DaXoa { get; set; } = false;

        [ForeignKey("MaKham")]
        public virtual KhamBenh? MaKhamNavigation { get; set; }

        [ForeignKey("MaBacSi")]
        public virtual BacSi? MaBacSiNavigation { get; set; }

        public virtual ICollection<ChiTietDonThuoc> ChiTietDonThuocs { get; set; }
    }
}
