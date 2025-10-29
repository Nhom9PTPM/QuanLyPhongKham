using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenhNhansController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.BenhNhans.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var benhNhan = db.BenhNhans
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
                        x.CMTND_NV,
                        x.NgayTao,
                        x.DaXoa
                    })
                    .SingleOrDefault();
                return Ok(benhNhan);
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
            return Ok("Thực hiện thành công");
        }

        [Route("update-benhnhan")]
        [HttpPost]
        public IActionResult UpdateBenhNhan(BenhNhan model)
        {
            var benhNhan = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == model.MaBenhNhan);
            if (benhNhan != null)
            {
                benhNhan.HoTen = model.HoTen;
                benhNhan.NgaySinh = model.NgaySinh;
                benhNhan.GioiTinh = model.GioiTinh;
                benhNhan.SoDienThoai = model.SoDienThoai;
                benhNhan.Email = model.Email;
                benhNhan.DiaChi = model.DiaChi;
                benhNhan.CMTND_NV = model.CMTND_NV;
                benhNhan.NgayTao = model.NgayTao;
                benhNhan.DaXoa = model.DaXoa;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã bệnh nhân không tồn tại");
            }
        }

        [Route("delete-benhnhan")]
        [HttpPost]
        public IActionResult DeleteBenhNhan(int id)
        {
            var benhNhan = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == id);
            if (benhNhan != null)
            {
                db.BenhNhans.Remove(benhNhan);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã bệnh nhân không tồn tại");
            }
        }
    }
}
