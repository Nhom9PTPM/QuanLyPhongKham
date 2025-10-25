using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonThuocService.Models
{
    public class DonThuoc
    {
        [Key]
        public int MaDonThuoc { get; set; }
        public int MaBenhNhan { get; set; }
        public int MaBacSi { get; set; }

        [MaxLength(1000)]
        public string? GhiChu { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
        public string? NguoiTao { get; set; }
        public bool DaXoa { get; set; } = false;

        // Navigation
        public ICollection<ChiTietDonThuoc>? ChiTietDonThuoc { get; set; }
    }
}
