using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("HoSoBenhAn")]
    public class HoSoBenhAn
    {
        [Key]
        [Column("MaHoSo")]
        public int MaHoSo { get; set; }

        [Column("MaBenhNhan")]
        public int MaBenhNhan { get; set; }

        [Column("TomTatBenhLy")]
        public string? TomTatBenhLy { get; set; }

        [Column("ChanDoanChinh")]
        public string? ChanDoanChinh { get; set; }

        [Column("LichSuBenhLy")]
        public string? LichSuBenhLy { get; set; }

        [Column("NgayLap")]
        public DateTime NgayLap { get; set; }

        [Column("NguoiLap")]
        public string? NguoiLap { get; set; }

        [Column("DaXoa")]
        public bool DaXoa { get; set; }

        [ForeignKey(nameof(MaBenhNhan))]
        public BenhNhan? BenhNhan { get; set; }

        // Quan hệ 1-n: HoSoBenhAn -> KhamBenh
        public List<KhamBenh>? DanhSachKham { get; set; }

        // Quan hệ 1-n: HoSoBenhAn -> TapTin
        public List<TapTin>? TapTin { get; set; }
    }
}
