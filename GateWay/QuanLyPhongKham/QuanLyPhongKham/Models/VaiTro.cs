using System;
using System.Collections.Generic;

namespace QuanLyPhongKham.Models
{
    public partial class VaiTro
    {
        public VaiTro()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public int MaVaiTro { get; set; }
        public string TenVaiTro { get; set; } = null!;
        public string? MoTa { get; set; }
        public DateTime NgayTao { get; set; }
        public bool DaXoa { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
