using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_Admin.Models
{
    public class HoSoBenhAn
    {
        [Key] // 🔑 Khóa chính
        public int MaHoSo { get; set; }

        public int MaBenhNhan { get; set; }         // FK → BenhNhan
        public string TomTatBenhLy { get; set; }
        public string ChanDoanChinh { get; set; }
        public string LichSuBenhLy { get; set; }
        public string TapTinDinhKem { get; set; }
        public DateTime NgayLap { get; set; }
        public string NguoiLap { get; set; }
        public bool DaXoa { get; set; }
    }
}
