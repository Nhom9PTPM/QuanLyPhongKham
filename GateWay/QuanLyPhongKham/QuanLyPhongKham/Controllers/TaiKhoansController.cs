using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaiKhoansController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.TaiKhoans.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var taiKhoan = db.TaiKhoans
                .Where(x => x.MaTaiKhoan == id)
                .Select(x => new
                {
                    x.MaTaiKhoan,
                    x.TenDangNhap,
                    x.MatKhau,
                    x.MaNguoiDung,
                    x.MaVaiTro,
                    x.LoaiQuyen,
                    x.TrangThai,
                    x.NgayTao
                })
                .SingleOrDefault();

            if (taiKhoan != null)
                return Ok(taiKhoan);
            else
                return NotFound("Không tìm thấy tài khoản");
        }

        [Route("create-taikhoan")]
        [HttpPost]
        public IActionResult CreateTaiKhoan(TaiKhoan model)
        {
            db.TaiKhoans.Add(model);
            db.SaveChanges();
            return Ok("Tạo tài khoản thành công");
        }

        [Route("update-taikhoan")]
        [HttpPost]
        public IActionResult UpdateTaiKhoan(TaiKhoan model)
        {
            var taiKhoan = db.TaiKhoans.SingleOrDefault(x => x.MaTaiKhoan == model.MaTaiKhoan);
            if (taiKhoan != null)
            {
                taiKhoan.TenDangNhap = model.TenDangNhap;
                taiKhoan.MatKhau = model.MatKhau;
                taiKhoan.MaNguoiDung = model.MaNguoiDung;
                taiKhoan.MaVaiTro = model.MaVaiTro;
                taiKhoan.LoaiQuyen = model.LoaiQuyen;
                taiKhoan.TrangThai = model.TrangThai;
                // NgayTao không cập nhật
                db.SaveChanges();
                return Ok("Cập nhật tài khoản thành công");
            }
            else
            {
                return NotFound("Tài khoản không tồn tại");
            }
        }

        [Route("delete-taikhoan/{id}")]
        [HttpPost]
        public IActionResult DeleteTaiKhoan(int id)
        {
            var taiKhoan = db.TaiKhoans.SingleOrDefault(x => x.MaTaiKhoan == id);
            if (taiKhoan != null)
            {
                db.TaiKhoans.Remove(taiKhoan);
                db.SaveChanges();
                return Ok("Xóa tài khoản thành công");
            }
            else
            {
                return NotFound("Tài khoản không tồn tại");
            }
        }
    }
}
