using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    [Table("BenhNhan")]
    public partial class BenhNhan
    {
        public BenhNhan()
        {
            LichHens = new HashSet<LichHen>();
            HoSoBenhAns = new HashSet<HoSoBenhAn>();
            KhamBenhs = new HashSet<KhamBenh>();
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaBenhNhan { get; set; }
        public string HoTen { get; set; } = null!;
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? CMTND_NV { get; set; }
        public DateTime NgayTao { get; set; }
        public bool DaXoa { get; set; }

        public virtual ICollection<LichHen> LichHens { get; set; }
        public virtual ICollection<HoSoBenhAn> HoSoBenhAns { get; set; }
        public virtual ICollection<KhamBenh> KhamBenhs { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
