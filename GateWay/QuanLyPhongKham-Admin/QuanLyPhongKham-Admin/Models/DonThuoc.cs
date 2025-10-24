using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("DonThuoc")]
    public class DonThuoc
    {
        [Key]
        [Column("MaDonThuoc")]
        public int MaDonThuoc { get; set; }

        [Column("MaKham")]
        public int? MaKham { get; set; }

        [Column("MaBacSi")]
        public int? MaBacSi { get; set; }

        [Column("NgayKe")]
        public DateTime NgayKe { get; set; }

        [Column("GhiChu")]
        public string? GhiChu { get; set; }

        [Column("NguoiKe")]
        public string? NguoiKe { get; set; }

        [Column("DaXoa")]
        public bool DaXoa { get; set; }

        [ForeignKey(nameof(MaKham))]
        public KhamBenh? KhamBenh { get; set; }

        [InverseProperty(nameof(ChiTietDonThuoc.DonThuoc))]
        public List<ChiTietDonThuoc>? ChiTiet { get; set; }
    }
}
