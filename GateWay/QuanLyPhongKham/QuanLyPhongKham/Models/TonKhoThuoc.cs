using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class TonKhoThuoc
    {
        [Key]
        public int MaTonKho { get; set; }

        [ForeignKey("Thuoc")]
        public int MaThuoc { get; set; }
        public Thuoc? Thuoc { get; set; }

        public int SoLuong { get; set; } = 0;
        public string? MaKho { get; set; }
        public DateTime NgayCapNhat { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
