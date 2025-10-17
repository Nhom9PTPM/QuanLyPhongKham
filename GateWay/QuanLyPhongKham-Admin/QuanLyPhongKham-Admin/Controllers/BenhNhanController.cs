using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenhNhanController : ControllerBase
    {
        private PhongKhamContext db = null;

        public BenhNhanController(IConfiguration configuration)
        {
            db = new PhongKhamContext(configuration);
        }

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var list = db.BenhNhans
                    .Select(x => new
                    {
                        x.MaBenhNhan,
                        x.HoTen,
                        x.NgaySinh,
                        x.GioiTinh,
                        x.SoDienThoai,
                        x.Email,
                        x.DiaChi,
                        x.CMTND_NV
                    })
                    .OrderByDescending(x => x.MaBenhNhan)
                    .ToList();
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var bn = db.BenhNhans
                    .Where(x => x.MaBenhNhan == id)
                    .Select(x => new
                    {
                        x.MaBenhNhan,
                        x.HoTen,
                        x.NgaySinh,
                        x.GioiTinh,
                        x.SoDienThoai,
                        x.Email,
                        x.DiaChi,
                        x.CMTND_NV
                    })
                    .SingleOrDefault();
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
            try
            {
                model.NgayTao = DateTime.UtcNow;
                db.BenhNhans.Add(model);
                db.SaveChanges();
                return Ok("Thêm bệnh nhân thành công");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("update-benhnhan")]
        [HttpPost]
        public IActionResult UpdateBenhNhan(BenhNhan model)
        {
            try
            {
                var bn = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == model.MaBenhNhan);
                if (bn != null)
                {
                    bn.HoTen = model.HoTen ?? bn.HoTen;
                    bn.NgaySinh = model.NgaySinh ?? bn.NgaySinh;
                    bn.GioiTinh = model.GioiTinh ?? bn.GioiTinh;
                    bn.DiaChi = model.DiaChi ?? bn.DiaChi;
                    bn.Email = model.Email ?? bn.Email;
                    bn.SoDienThoai = model.SoDienThoai ?? bn.SoDienThoai;
                    bn.CMTND_NV = model.CMTND_NV ?? bn.CMTND_NV;
                    db.SaveChanges();
                    return Ok("Cập nhật thành công");
                }
                return Ok("Không tìm thấy bệnh nhân");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("delete-benhnhan/{id}")]
        [HttpGet]
        public IActionResult DeleteBenhNhan(int id)
        {
            try
            {
                var obj = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == id);
                if (obj != null)
                {
                    db.BenhNhans.Remove(obj);
                    db.SaveChanges();
                    return Ok("Xóa thành công");
                }
                return Ok("Không tìm thấy bệnh nhân");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
