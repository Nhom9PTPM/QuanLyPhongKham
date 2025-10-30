using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("KhamBenh")]
    public class KhamBenh
    {
        [Key]
        [Column("MaKham")]
        public int MaKham { get; set; }

        [Column("MaHoSo")]
        public int? MaHoSo { get; set; }

        [Column("MaBenhNhan")]
        public int MaBenhNhan { get; set; }

        [Column("MaBacSi")]
        public int? MaBacSi { get; set; }

        [Column("NgayKham")]
        public DateTime NgayKham { get; set; }

        [Column("ChanDoan")]
        public string? ChanDoan { get; set; }

        [Column("ChiDinhXN")]
        public string? ChiDinhXN { get; set; }

        [Column("ChiDinhCLS")]
        public string? ChiDinhCLS { get; set; }

        [Column("GhiChu")]
        public string? GhiChu { get; set; }

        [Column("NguoiThucHien")]
        public string? NguoiThucHien { get; set; }

        [Column("NgayTao")]
        public DateTime NgayTao { get; set; }

        [Column("DaXoa")]
        public bool DaXoa { get; set; }

        // ===== Quan hệ =====

        [ForeignKey(nameof(MaHoSo))]
        public HoSoBenhAn? HoSoBenhAn { get; set; }

        public List<DonThuoc>? DonThuoc { get; set; }

        //  Thêm 2 thuộc tính này để fix lỗi DAL
        [ForeignKey(nameof(MaBenhNhan))]
        public BenhNhan? BenhNhan { get; set; }

        [ForeignKey(nameof(MaBacSi))]
        public BacSi? BacSi { get; set; }
    }
}
