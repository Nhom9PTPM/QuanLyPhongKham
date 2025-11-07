using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_NguoiDung.Code;
using QuanLyPhongKham_NguoiDung.Models;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        public TaiKhoanController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        private string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.UTF8.GetBytes(password);
                byte[] hash = md5.ComputeHash(input);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        [Route("dang-nhap")]
        [HttpPost]
        public IActionResult DangNhap([FromBody] Dictionary<string, string> formData)
        {
            try
            {
                string username = formData["TenDangNhap"];
                string password = formData["MatKhau"];
                string hashPass = HashPassword(password);

                var user = (from tk in db.TaiKhoans
                            join nd in db.NguoiDungs on tk.MaNguoiDung equals nd.MaNguoiDung
                            join vt in db.VaiTros on tk.MaVaiTro equals vt.MaVaiTro into tmp
                            from role in tmp.DefaultIfEmpty()
                            where tk.TenDangNhap == username && tk.MatKhau == hashPass && tk.TrangThai == true
                            select new
                            {
                                tk.MaTaiKhoan,
                                tk.TenDangNhap,
                                VaiTro = role != null ? role.TenVaiTro : "",
                                nd.HoTen,
                                nd.Email,
                                nd.SoDienThoai,
                                nd.AnhDaiDien,
                                nd.LoaiNguoiDung
                            }).FirstOrDefault();

                if (user == null)
                    return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không đúng." });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi đăng nhập", error = ex.Message });
            }
        }

        [Route("dang-ky")]
        [HttpPost]
        public IActionResult DangKy([FromBody] Dictionary<string, string> formData)
        {
            try
            {
                string hoTen = formData["HoTen"];
                string email = formData["Email"];
                string soDienThoai = formData["SoDienThoai"];
                string tenDangNhap = formData["TenDangNhap"];
                string matKhau = formData["MatKhau"];
                string hashPass = HashPassword(matKhau);

                if (db.TaiKhoans.Any(x => x.TenDangNhap == tenDangNhap))
                    return BadRequest(new { message = "Tên đăng nhập đã tồn tại." });

                var nd = new NguoiDung()
                {
                    HoTen = hoTen,
                    SoDienThoai = soDienThoai,
                    Email = email,
                    GioiTinh = "",
                    DiaChi = "",
                    LoaiNguoiDung = "BenhNhan",
                    NgayTao = DateTime.Now
                };
                db.NguoiDungs.Add(nd);
                db.SaveChanges();

                var bn = new BenhNhan()
                {
                    HoTen = hoTen,
                    Email = email,
                    SoDienThoai = soDienThoai,
                    NgayTao = DateTime.Now
                };
                db.BenhNhans.Add(bn);
                db.SaveChanges();

                var tk = new TaiKhoan()
                {
                    TenDangNhap = tenDangNhap,
                    MatKhau = hashPass,
                    MaNguoiDung = nd.MaNguoiDung,
                    MaVaiTro = db.VaiTros.FirstOrDefault(x => x.TenVaiTro == "BenhNhan")?.MaVaiTro,
                    LoaiQuyen = "BenhNhan",
                    TrangThai = true,
                    NgayTao = DateTime.Now
                };
                db.TaiKhoans.Add(tk);
                db.SaveChanges();

                return Ok(new { message = "Đăng ký thành công.", TaiKhoan = tenDangNhap });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi đăng ký", error = ex.Message });
            }
        }

        [Route("doi-matkhau")]
        [HttpPost]
        public IActionResult DoiMatKhau([FromBody] Dictionary<string, string> formData)
        {
            try
            {
                string tenDangNhap = formData["TenDangNhap"];
                string matKhauCu = HashPassword(formData["MatKhauCu"]);
                string matKhauMoi = HashPassword(formData["MatKhauMoi"]);

                var tk = db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == tenDangNhap && x.MatKhau == matKhauCu);
                if (tk == null)
                    return BadRequest(new { message = "Mật khẩu cũ không đúng." });

                tk.MatKhau = matKhauMoi;
                db.SaveChanges();

                return Ok(new { message = "Đổi mật khẩu thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi đổi mật khẩu", error = ex.Message });
            }
        }

        [Route("thong-tin/{tenDangNhap}")]
        [HttpGet]
        public IActionResult ThongTinNguoiDung(string tenDangNhap)
        {
            try
            {
                var info = (from tk in db.TaiKhoans
                            join nd in db.NguoiDungs on tk.MaNguoiDung equals nd.MaNguoiDung
                            where tk.TenDangNhap == tenDangNhap
                            select new
                            {
                                nd.HoTen,
                                nd.Email,
                                nd.SoDienThoai,
                                nd.DiaChi,
                                nd.GioiTinh,
                                nd.NgaySinh,
                                nd.AnhDaiDien,
                                nd.LoaiNguoiDung
                            }).FirstOrDefault();

                if (info == null)
                    return NotFound("Không tìm thấy người dùng.");

                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi lấy thông tin người dùng", error = ex.Message });
            }
        }
    }
}
