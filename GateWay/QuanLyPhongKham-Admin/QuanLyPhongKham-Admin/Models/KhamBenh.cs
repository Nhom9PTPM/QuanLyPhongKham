using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_Admin.Models
{
    public class KhamBenh
    {
        [Key]
        public int MaKham { get; set; }

        public int? MaHoSo { get; set; }
        public int MaBenhNhan { get; set; }
        public int? MaBacSi { get; set; }
        public DateTime NgayKham { get; set; }

        public string? ChanDoan { get; set; }     // ✅ Cho phép null
        public string? ChiDinhXN { get; set; }    // ✅ Cho phép null
        public string? ChiDinhCLS { get; set; }   // ✅ Cho phép null
        public string? GhiChu { get; set; }       // ✅ Cho phép null
        public string? NguoiThucHien { get; set; } // ✅ Cho phép null

        public DateTime NgayTao { get; set; }
        public bool DaXoa { get; set; }
    }
}
