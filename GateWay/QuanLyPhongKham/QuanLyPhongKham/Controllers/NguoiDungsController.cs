using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NguoiDungController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.NguoiDungs.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var nguoiDung = db.NguoiDungs
                    .Where(x => x.MaNguoiDung == id)
                    .Select(x => new
                    {
                        x.MaNguoiDung,
                        x.HoTen,
                        x.GioiTinh,
                        x.NgaySinh,
                        x.SoDienThoai,
                        x.Email,
                        x.DiaChi,
                        x.AnhDaiDien,
                        x.LoaiNguoiDung,
                        x.GhiChu,
                        x.NgayTao,
                        x.DaXoa
                    })
                    .SingleOrDefault();
                return Ok(nguoiDung);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-nguoidung")]
        [HttpPost]
        public IActionResult CreateNguoiDung(NguoiDung model)
        {
            db.NguoiDungs.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-nguoidung")]
        [HttpPost]
        public IActionResult UpdateNguoiDung(NguoiDung model)
        {
            var nguoiDung = db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == model.MaNguoiDung);
            if (nguoiDung != null)
            {
                nguoiDung.HoTen = model.HoTen;
                nguoiDung.GioiTinh = model.GioiTinh;
                nguoiDung.NgaySinh = model.NgaySinh;
                nguoiDung.SoDienThoai = model.SoDienThoai;
                nguoiDung.Email = model.Email;
                nguoiDung.DiaChi = model.DiaChi;
                nguoiDung.AnhDaiDien = model.AnhDaiDien;
                nguoiDung.LoaiNguoiDung = model.LoaiNguoiDung;
                nguoiDung.GhiChu = model.GhiChu;
                nguoiDung.DaXoa = model.DaXoa;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã người dùng không tồn tại");
            }
        }

        [Route("delete-nguoidung")]
        [HttpPost]
        public IActionResult DeleteNguoiDung(int id)
        {
            var nguoiDung = db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == id);
            if (nguoiDung != null)
            {
                db.NguoiDungs.Remove(nguoiDung);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã người dùng không tồn tại");
            }
        }
    }
}
