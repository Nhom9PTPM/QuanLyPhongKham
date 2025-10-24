using System;
using System.Collections.Generic;

namespace QuanLyPhongKham.Models
{
    public partial class BacSi
    {
        public BacSi()
        {
            LichHens = new HashSet<LichHen>();
            KhamBenhs = new HashSet<KhamBenh>();
            DonThuocs = new HashSet<DonThuoc>();
        }

        public int MaBacSi { get; set; }
        public int? MaNguoiDung { get; set; }
        public string? ChuyenKhoa { get; set; }
        public string? BangCap { get; set; }
        public string? KinhNghiem { get; set; }
        public string? SoPhong { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual NguoiDung? MaNguoiDungNavigation { get; set; }

        public virtual ICollection<LichHen> LichHens { get; set; }
        public virtual ICollection<KhamBenh> KhamBenhs { get; set; }
        public virtual ICollection<DonThuoc> DonThuocs { get; set; }
    }
}
