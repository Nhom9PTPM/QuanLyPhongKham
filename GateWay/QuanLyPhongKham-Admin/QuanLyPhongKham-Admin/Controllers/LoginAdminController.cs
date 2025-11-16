using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using QuanLyPhongKham_Admin.Helper;
using Microsoft.EntityFrameworkCore;

namespace QuanLyPhongKham_Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginAdminController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext _db;
        private readonly AppSettings _appSettings;

        public LoginAdminController(QuanLyPhongKhamContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _db.TaiKhoans
                .FirstOrDefault(x => x.TenDangNhap == model.TenDangNhap
                                  && x.MatKhau == model.MatKhau);

            if (user == null)
                return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu!" });

            // Tạo token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("MaTaiKhoan", user.MaTaiKhoan.ToString()),
                    new Claim("MaVaiTro", user.MaVaiTro.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                maTaiKhoan = user.MaTaiKhoan,
                maNguoiDung = user.MaNguoiDung,
                maVaiTro = user.MaVaiTro
            });
        }
    }
}
