using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("TapTin")]
    public class TapTin
    {
        [Key]
        [Column("MaTapTin")]
        public int MaTapTin { get; set; }

        [Column("TenTapTin")]
        public string TenTapTin { get; set; } = string.Empty;

        [Column("DuongDan")]
        public string DuongDan { get; set; } = string.Empty;

        [Column("MaHoSo")]
        public int? MaHoSo { get; set; }

        [ForeignKey(nameof(MaHoSo))]
        public HoSoBenhAn? HoSoBenhAn { get; set; }
    }
}
