using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham_Admin.Models
{
    [Table("Thuoc")]
    public class Thuoc
    {
        [Key]
        [Column("MaThuoc")]
        public int MaThuoc { get; set; }

        [Column("TenThuoc")]
        public string? TenThuoc { get; set; }

        [Column("DonViTinh")]
        public string? DonViTinh { get; set; }

        [Column("MoTa")]
        public string? MoTa { get; set; }

        [Column("TrangThai")]
        public bool? TrangThai { get; set; } 


        [Column("NgayTao")]
        public DateTime? NgayTao { get; set; }

   
    }
}
