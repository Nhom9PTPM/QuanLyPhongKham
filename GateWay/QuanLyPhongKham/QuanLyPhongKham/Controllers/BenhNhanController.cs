using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenhNhanController : ControllerBase
    {
        private QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.BenhNhans.OrderByDescending(x => x.NgayTao).ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var bn = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == id);
                return Ok(bn);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-benhnhan")]
        [HttpPost]
        public IActionResult CreateBenhNhan(BenhNhan model)
        {
            db.BenhNhans.Add(model);
            db.SaveChanges();
            return Ok("Thêm bệnh nhân thành công");
        }

        [Route("update-benhnhan")]
        [HttpPost]
        public IActionResult UpdateBenhNhan(BenhNhan model)
        {
            var bn = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == model.MaBenhNhan);
            if (bn != null)
            {
                bn.HoTen = model.HoTen;
                bn.NgaySinh = model.NgaySinh;
                bn.GioiTinh = model.GioiTinh;
                bn.SoDienThoai = model.SoDienThoai;
                bn.Email = model.Email;
                bn.DiaChi = model.DiaChi;
                bn.CMTND_NV = model.CMTND_NV;
                db.SaveChanges();
                return Ok("Cập nhật bệnh nhân thành công");
            }
            return Ok("Không tìm thấy bệnh nhân");
        }

        [Route("delete-benhnhan/{id}")]
        [HttpGet]
        public IActionResult DeleteBenhNhan(int id)
        {
            var bn = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == id);
            if (bn != null)
            {
                db.BenhNhans.Remove(bn);
                db.SaveChanges();
                return Ok("Xóa bệnh nhân thành công");
            }
            return Ok("Không tìm thấy bệnh nhân");
        }
    }
}
