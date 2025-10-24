using System;
using System.Collections.Generic;

namespace QuanLyPhongKham.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            Thuocs = new HashSet<Thuoc>();
        }

        public int MaNhaCungCap { get; set; }
        public string TenNhaCungCap { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? NguoiLienHe { get; set; }
        public string? GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
        public bool DaXoa { get; set; }

        public virtual ICollection<Thuoc> Thuocs { get; set; }
    }
}
