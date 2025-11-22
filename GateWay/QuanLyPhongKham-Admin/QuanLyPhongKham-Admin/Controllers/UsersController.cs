using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Code;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext _db;
        private readonly ITools _tools;

        public UsersController(ITools tools, QuanLyPhongKhamContext db)
        {
            _tools = tools;
            _db = db;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = (from n in _db.NguoiDungs
                            join t in _db.TaiKhoans on n.MaNguoiDung equals t.MaNguoiDung
                            where n.MaNguoiDung == id
                            select new
                            {
                                n.MaNguoiDung,
                                n.HoTen,
                                n.GioiTinh,
                                n.NgaySinh,
                                n.SoDienThoai,
                                n.Email,
                                n.DiaChi,
                                n.AnhDaiDien,
                                n.LoaiNguoiDung,
                                n.GhiChu,
                                t.TenDangNhap,
                                t.MatKhau,
                                t.LoaiQuyen,
                                t.MaVaiTro,
                                t.TrangThai
                            }).SingleOrDefault();

                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                string path = $"upload/{file.FileName.Replace("-", "_")}";
                var full = _tools.CreatePathFile(path);

                using var fs = new FileStream(full, FileMode.Create);
                await file.CopyToAsync(fs);

                return Ok(new { path });
            }
            catch
            {
                return StatusCode(500, "Upload lỗi");
            }
        }

        [Route("create-user")]
        [HttpPost]
        public IActionResult Create([FromBody] UserModels model)
        {
            if (model == null || model.nguoidung == null || model.taikhoan == null)
                return BadRequest(new { Message = "Dữ liệu không hợp lệ." });

            try
            {
                // Kiểm tra tên đăng nhập đã tồn tại chưa
                var exists = _db.TaiKhoans.SingleOrDefault(x => x.TenDangNhap == model.taikhoan.TenDangNhap);
                if (exists != null)
                    return BadRequest(new { Message = "Tên đăng nhập đã tồn tại!" });

                // Kiểm tra người dùng trùng tên + email (tuỳ nhu cầu)
                var nguoiDungExists = _db.NguoiDungs
                    .FirstOrDefault(x => x.HoTen == model.nguoidung.HoTen && x.Email == model.nguoidung.Email);
                if (nguoiDungExists != null)
                    return BadRequest(new { Message = "Người dùng đã tồn tại!" });

                // Tạo NguoiDung
                var newNguoiDung = new NguoiDung
                {
                    HoTen = model.nguoidung.HoTen,
                    GioiTinh = model.nguoidung.GioiTinh,
                    NgaySinh = model.nguoidung.NgaySinh,
                    SoDienThoai = model.nguoidung.SoDienThoai,
                    Email = model.nguoidung.Email,
                    DiaChi = model.nguoidung.DiaChi,
                    LoaiNguoiDung = model.nguoidung.LoaiNguoiDung,
                    NgayTao = DateTime.Now,
                    DaXoa = false
                };

                _db.NguoiDungs.Add(newNguoiDung);
                _db.SaveChanges(); 

                var newTaiKhoan = new TaiKhoan
                {
                    TenDangNhap = model.taikhoan.TenDangNhap,
                    MatKhau = model.taikhoan.MatKhau,
                    LoaiQuyen = model.taikhoan.LoaiQuyen,
                    MaVaiTro = model.taikhoan.MaVaiTro,
                    TrangThai = model.taikhoan.TrangThai,
                    NgayTao = DateTime.Now,
                    MaNguoiDung = newNguoiDung.MaNguoiDung
                };

                _db.TaiKhoans.Add(newTaiKhoan);
                _db.SaveChanges();

                return Ok(new { Message = "Tạo người dùng và tài khoản thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Có lỗi xảy ra khi tạo người dùng.", Details = ex.Message });
            }
        }


        [Route("update-user")]
        [HttpPost]
        public IActionResult Update(UserEditModels model)
        {
            try
            {
                var n = _db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == model.MaNguoiDung);
                var t = _db.TaiKhoans.SingleOrDefault(x => x.MaNguoiDung == model.MaNguoiDung);

                if (n == null || t == null) return BadRequest();

                // Update NguoiDung
                n.HoTen = model.HoTen ?? n.HoTen;
                n.Email = model.Email ?? n.Email;
                n.GioiTinh = model.GioiTinh ?? n.GioiTinh;
                n.SoDienThoai = model.SoDienThoai ?? n.SoDienThoai;
                n.DiaChi = model.DiaChi ?? n.DiaChi;
                n.LoaiNguoiDung = model.LoaiNguoiDung ?? n.LoaiNguoiDung;
                n.GhiChu = model.GhiChu ?? n.GhiChu;
                n.AnhDaiDien = model.AnhDaiDien ?? n.AnhDaiDien;
                n.DaXoa = model.DaXoa ?? n.DaXoa;

                // Update TaiKhoan
                t.MatKhau = model.MatKhau ?? t.MatKhau;
                t.LoaiQuyen = model.LoaiQuyen ?? t.LoaiQuyen;
                t.MaVaiTro = model.MaVaiTro ?? t.MaVaiTro;
                t.TrangThai = model.TrangThai ?? t.TrangThai;

                _db.SaveChanges();
                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("delete-user")]
        [HttpDelete]
        public IActionResult Delete(int MaNguoiDung)
        {
            try
            {
                var n = _db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == MaNguoiDung);
                var t = _db.TaiKhoans.SingleOrDefault(x => x.MaNguoiDung == MaNguoiDung);

                if (n == null || t == null) return BadRequest();

                n.DaXoa = true;
                t.TrangThai = false;

                _db.SaveChanges();
                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }


        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> form)
        {
            var res = new ResponseModel();
            try
            {
                int page = int.Parse(form["page"].ToString());
                int size = int.Parse(form["pageSize"].ToString());
                string HoTen = "";

                if (form.Keys.Contains("HoTen"))
                    HoTen = form["HoTen"].ToString();

                var q = from n in _db.NguoiDungs
                        join t in _db.TaiKhoans on n.MaNguoiDung equals t.MaNguoiDung
                        where n.DaXoa == false
                        select new
                        {
                            n.MaNguoiDung,      // thêm dòng này
                            n.HoTen,
                            n.GioiTinh,
                            n.SoDienThoai,
                            t.TenDangNhap,
                            t.LoaiQuyen,
                            t.TrangThai
                        };

                var data = q.Where(x => HoTen == "" || x.HoTen.Contains(HoTen)).ToList();

                res.TotalItems = data.Count;
                res.Page = page;
                res.PageSize = size;
                res.Data = data.Skip((page - 1) * size).Take(size).ToList();
            }
            catch { }

            return res;
        }

    }
}
