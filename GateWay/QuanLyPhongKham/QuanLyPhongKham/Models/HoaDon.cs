using System;
using System.Collections.Generic;

namespace QuanLyPhongKham.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public int MaHoaDon { get; set; }
        public int? MaBenhNhan { get; set; }
        public int? MaNguoiThu { get; set; }
        public DateTime NgayLap { get; set; }
        public double TongTien { get; set; }
        public string? PhuongThucThanhToan { get; set; }
        public string? TrangThai { get; set; }
        public string? GhiChu { get; set; }

        public virtual BenhNhan? MaBenhNhanNavigation { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
