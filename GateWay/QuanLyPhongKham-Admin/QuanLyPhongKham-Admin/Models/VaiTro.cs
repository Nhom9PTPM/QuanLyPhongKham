using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("VaiTro")]
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
