using System;

namespace QuanLyPhongKham.Models
{
    public partial class TonKhoThuoc
    {
        public int MaTonKho { get; set; }
        public int MaThuoc { get; set; }
        public int SoLuong { get; set; }
        public string? MaKho { get; set; }
        public DateTime NgayCapNhat { get; set; }

        public virtual Thuoc MaThuocNavigation { get; set; } = null!;
    }
}
