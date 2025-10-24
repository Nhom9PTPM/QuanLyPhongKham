using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Code
{
    public class BenhNhanModels
    {
        public BenhNhan benhnhan { get; set; }
    }

    public class BenhNhanEditModels
    {
        public int MaBenhNhan { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? CMTND_NV { get; set; }
        public bool? DaXoa { get; set; }
    }
}
