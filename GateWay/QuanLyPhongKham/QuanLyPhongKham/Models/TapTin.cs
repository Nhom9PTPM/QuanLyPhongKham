using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class TapTin
    {
        [Key]
        public int MaTapTin { get; set; }
        public string TenTapTin { get; set; } = "";
        public string DuongDan { get; set; } = "";
        public long? KichThuoc { get; set; }
        public string? DinhDang { get; set; }

        [ForeignKey("HoSoBenhAn")]
        public int? MaHoSo { get; set; }
        public HoSoBenhAn? HoSoBenhAn { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
    }
}
