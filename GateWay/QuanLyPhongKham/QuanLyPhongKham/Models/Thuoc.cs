using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    [Table("Thuoc")]
    public partial class Thuoc
    {
        public Thuoc()
        {
            TonKhoThuocs = new HashSet<TonKhoThuoc>();
            ChiTietDonThuocs = new HashSet<ChiTietDonThuoc>();
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; } = null!;
        public int? MaNhaCungCap { get; set; }
        public string? DonViTinh { get; set; }
        public string? MoTa { get; set; }
        public double? Gia { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual NhaCungCap? MaNhaCungCapNavigation { get; set; }

        public virtual ICollection<TonKhoThuoc> TonKhoThuocs { get; set; }
        public virtual ICollection<ChiTietDonThuoc> ChiTietDonThuocs { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
