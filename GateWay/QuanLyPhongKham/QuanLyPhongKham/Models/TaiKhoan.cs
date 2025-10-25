using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            ThongBaos = new HashSet<ThongBao>();
        }

        public int MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public int MaNguoiDung { get; set; }
        public int? MaVaiTro { get; set; }
        public string? LoaiQuyen { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }

        public virtual NguoiDung MaNguoiDungNavigation { get; set; } = null!;
        public virtual VaiTro? MaVaiTroNavigation { get; set; }

        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
