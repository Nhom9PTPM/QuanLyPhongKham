using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("HoSoBenhAn")]
    public partial class HoSoBenhAn
    {
        public HoSoBenhAn()
        {
            KhamBenhs = new HashSet<KhamBenh>();
            TapTins = new HashSet<TapTin>();
        }

        public int MaHoSo { get; set; }
        public int MaBenhNhan { get; set; }
        public string? TomTatBenhLy { get; set; }
        public string? ChanDoanChinh { get; set; }
        public string? LichSuBenhLy { get; set; }
        public string? TapTinDinhKem { get; set; }
        public DateTime NgayLap { get; set; }
        public string? NguoiLap { get; set; }
        public bool DaXoa { get; set; }

        public virtual BenhNhan MaBenhNhanNavigation { get; set; } = null!;
        public virtual ICollection<KhamBenh> KhamBenhs { get; set; }
        public virtual ICollection<TapTin> TapTins { get; set; }
    }
}
