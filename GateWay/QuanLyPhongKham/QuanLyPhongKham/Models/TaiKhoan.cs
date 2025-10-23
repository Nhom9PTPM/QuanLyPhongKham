using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }

        public string TenDangNhap { get; set; } = "";
        public string MatKhau { get; set; } = ""; 

        [ForeignKey("NguoiDung")]
        public int MaNguoiDung { get; set; }
        public NguoiDung? NguoiDung { get; set; }

        [ForeignKey("VaiTro")]
        public int? MaVaiTro { get; set; }
        public VaiTro? VaiTro { get; set; }

        public string? LoaiQuyen { get; set; } 
        public bool TrangThai { get; set; } = true;
        public DateTime NgayTao { get; set; } = DateTime.UtcNow;
    }
}
