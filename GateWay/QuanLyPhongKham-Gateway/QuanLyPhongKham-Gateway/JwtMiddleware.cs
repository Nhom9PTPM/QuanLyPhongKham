using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QuanLyPhongKham_Gateway.Helper;
using QuanLyPhongKham_Gateway.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace QuanLyPhongKham_Gateway
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly QuanLyPhongKhamContext db;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _next = next;
            _appSettings = appSettings.Value;
            db = new QuanLyPhongKhamContext(configuration);
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Expose-Headers", "*");

            if (!context.Request.Path.Equals("/api/token", StringComparison.OrdinalIgnoreCase))
            {
                return _next(context);
            }

            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return GenerateToken(context);
            }

            context.Response.StatusCode = 400;
            return context.Response.WriteAsync("Bad request.");
        }

        public async Task GenerateToken(HttpContext context)
        {
            var taikhoan = context.Request.Form["Taikhoan"].ToString();
            var matkhau = context.Request.Form["Matkhau"].ToString();

            var query = from n in db.NguoiDungs
                        join t in db.TaiKhoans on n.MaNguoiDung equals t.MaNguoiDung
                        select new
                        {
                            n.MaNguoiDung,
                            n.HoTen,
                            n.GioiTinh,
                            n.AnhDaiDien,
                            TaiKhoan = t.TenDangNhap,
                            t.LoaiQuyen,
                            t.MatKhau
                        };

            var user = query.SingleOrDefault(x => x.TaiKhoan == taikhoan && x.MatKhau == matkhau);

            if (user == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonConvert.SerializeObject(new
                {
                    code = (int)HttpStatusCode.BadRequest,
                    error = "Tài khoản hoặc mật khẩu không đúng"
                });
                await context.Response.WriteAsync(result);
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.MaNguoiDung.ToString()),
                    new Claim(ClaimTypes.Name, user.HoTen),
                    new Claim(ClaimTypes.Role, user.LoaiQuyen ?? "NguoiDung")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            var response = new
            {
                user.MaNguoiDung,
                user.HoTen,
                user.TaiKhoan,
                Token = token
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, Formatting.Indented));
        }
    }
}
