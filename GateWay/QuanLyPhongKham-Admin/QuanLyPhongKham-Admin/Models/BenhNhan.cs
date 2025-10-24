using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("BenhNhan")]
    public class BenhNhan
    {
        [Key]
        [Column("MaBenhNhan")]
        public int MaBenhNhan { get; set; }

        [Column("HoTen")]
        public string HoTen { get; set; } = string.Empty;

        [Column("NgaySinh")]
        public DateTime? NgaySinh { get; set; }

        [Column("GioiTinh")]
        public string? GioiTinh { get; set; }

        [Column("SoDienThoai")]
        public string? SoDienThoai { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

        [Column("DiaChi")]
        public string? DiaChi { get; set; }

        [Column("CMTND_NV")]
        public string? CMTND_NV { get; set; }

        [Column("NgayTao")]
        public DateTime NgayTao { get; set; }

        [Column("DaXoa")]
        public bool DaXoa { get; set; }

        // Quan hệ: BenhNhan có nhiều HoSoBenhAn
        public List<HoSoBenhAn>? HoSoBenhAn { get; set; }
    }
}
