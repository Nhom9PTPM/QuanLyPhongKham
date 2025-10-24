using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Code;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db;
        private readonly ITools _tools;

        public UsersController(ITools tools, IConfiguration config)
        {
            _tools = tools;
            db = new QuanLyPhongKhamContext(config);
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = (from n in db.NguoiDungs
                            join t in db.TaiKhoans on n.MaNguoiDung equals t.MaNguoiDung
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
        public IActionResult Create(UserModels model)
        {
            try
            {
                // Check tồn tại account
                var exists = db.TaiKhoans.SingleOrDefault(x => x.TenDangNhap == model.taikhoan.TenDangNhap);
                if (exists != null) return Ok("Tên đăng nhập đã tồn tại!");

                db.NguoiDungs.Add(model.nguoidung);
                db.SaveChanges();

                model.taikhoan.MaNguoiDung = model.nguoidung.MaNguoiDung;
                db.TaiKhoans.Add(model.taikhoan);
                db.SaveChanges();

                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("update-user")]
        [HttpPost]
        public IActionResult Update(UserEditModels model)
        {
            try
            {
                var n = db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == model.MaNguoiDung);
                var t = db.TaiKhoans.SingleOrDefault(x => x.MaNguoiDung == model.MaNguoiDung);

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

                db.SaveChanges();
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
                var n = db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == MaNguoiDung);
                var t = db.TaiKhoans.SingleOrDefault(x => x.MaNguoiDung == MaNguoiDung);

                if (n == null || t == null) return BadRequest();

                n.DaXoa = true;
                t.TrangThai = false;

                db.SaveChanges();
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

                var q = from n in db.NguoiDungs
                        join t in db.TaiKhoans on n.MaNguoiDung equals t.MaNguoiDung
                        where n.DaXoa == false
                        select new
                        {
                            n.HoTen,
                            n.GioiTinh,
                            n.SoDienThoai,
                            t.TenDangNhap,
                            t.LoaiQuyen
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
