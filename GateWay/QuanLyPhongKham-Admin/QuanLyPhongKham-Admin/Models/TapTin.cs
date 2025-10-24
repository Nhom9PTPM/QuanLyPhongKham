using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham_Admin.Models
{
    public class TapTin
    {
        [Key] // 🔑 Khóa chính
        public int MaTapTin { get; set; }

        public string TenTapTin { get; set; }
        public string DuongDan { get; set; }
        public long? KichThuoc { get; set; }
        public string DinhDang { get; set; }
        public int? MaHoSo { get; set; }             // FK → HoSoBenhAn
        public DateTime NgayTao { get; set; }
    }
}
