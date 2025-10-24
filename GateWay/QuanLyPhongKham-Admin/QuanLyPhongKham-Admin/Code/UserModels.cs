using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Code
{
    public class UserModels
    {
        public NguoiDung nguoidung { get; set; }
        public TaiKhoan taikhoan { get; set; }
    }

    public class UserEditModels
    {
        public int MaNguoiDung { get; set; }
        public string? HoTen { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? LoaiNguoiDung { get; set; }
        public string? GhiChu { get; set; }
        public bool? DaXoa { get; set; }

        public int? MaTaiKhoan { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public int? MaVaiTro { get; set; }
        public string? LoaiQuyen { get; set; }
        public bool? TrangThai { get; set; }
    }
}
