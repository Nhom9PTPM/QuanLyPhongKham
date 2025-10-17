using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace QuanLyPhongKham_NguoiDung.Models
{
    public class LichHen
    {
        [Key]
        public int MaLichHen { get; set; }

        [ForeignKey("BenhNhan")]
        public int MaBenhNhan { get; set; }

        public DateTime NgayHen { get; set; }
        public string? GioHen { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }

        public virtual BenhNhan? BenhNhan { get; set; }
    }
}
