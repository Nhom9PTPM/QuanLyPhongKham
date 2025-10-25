using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    [Table("KhamBenh")]
    public partial class KhamBenh
    {
        public KhamBenh()
        {
            DonThuocs = new HashSet<DonThuoc>();
        }

        public int MaKham { get; set; }
        public int? MaHoSo { get; set; }
        public int MaBenhNhan { get; set; }
        public int? MaBacSi { get; set; }
        public DateTime NgayKham { get; set; }
        public string? ChanDoan { get; set; }
        public string? ChiDinhXN { get; set; }
        public string? ChiDinhCLS { get; set; }
        public string? GhiChu { get; set; }
        public string? NguoiThucHien { get; set; }
        public DateTime NgayTao { get; set; }
        public bool DaXoa { get; set; }

        public virtual HoSoBenhAn? MaHoSoNavigation { get; set; }
        public virtual BenhNhan MaBenhNhanNavigation { get; set; } = null!;
        public virtual BacSi? MaBacSiNavigation { get; set; }

        public virtual ICollection<DonThuoc> DonThuocs { get; set; }
    }
}
