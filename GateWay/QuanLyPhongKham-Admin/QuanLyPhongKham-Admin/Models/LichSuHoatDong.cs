using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("LichSuHoatDong")]
    public partial class LichSuHoatDong
    {
        [Key]
        public int MaLichSu { get; set; }
        public int? MaTaiKhoan { get; set; }
        public string HanhDong { get; set; } = null!;
        public DateTime DongThoiGian { get; set; }
        public string? DiaChiIP { get; set; }
        public string? ChiTiet { get; set; }
    }
}
