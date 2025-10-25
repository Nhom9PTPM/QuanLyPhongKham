using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("TapTin")]
    public partial class TapTin
    {
        public int MaTapTin { get; set; }
        public string TenTapTin { get; set; } = null!;
        public string DuongDan { get; set; } = null!;
        public long? KichThuoc { get; set; }
        public string? DinhDang { get; set; }
        public int? MaHoSo { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual HoSoBenhAn? MaHoSoNavigation { get; set; }
    }
}
