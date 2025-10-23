
namespace QuanLyPhongKham_Admin.Code
{
    public class UserModels
    {
        public class UserLogin
        {
            public int MaNguoiDung { get; set; }
            public string HoTen { get; set; } = "";
            public string TaiKhoan { get; set; } = "";
            public string? VaiTro { get; set; } = "";
        }

        public class LoginRequest
        {
            public string TaiKhoan { get; set; } = "";
            public string MatKhau { get; set; } = "";
        }

        public class LoginResponse
        {
            public string Token { get; set; } = "";
            public string HoTen { get; set; } = "";
            public string VaiTro { get; set; } = "";
        }
    }
}
