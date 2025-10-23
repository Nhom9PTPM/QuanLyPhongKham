using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class Thuoc
    {
        [Key]
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; } = "";
        [ForeignKey("NhaCungCap")]
        public int? MaNhaCungCap { get; set; }
        public NhaCungCap? NhaCungCap { get; set; }

        public string? DonViTinh { get; set; }
        public string? MoTa { get; set; }
        public double? Gia { get; set; }
        public bool TrangThai { get; set; } = true;
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public ICollection<TonKhoThuoc>? TonKhos { get; set; }
        public ICollection<ChiTietDonThuoc>? ChiTietDonThuocs { get; set; }
        public ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
    }
}
