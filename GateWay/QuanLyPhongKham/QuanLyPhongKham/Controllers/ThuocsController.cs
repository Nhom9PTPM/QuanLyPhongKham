using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThuocsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.Thuocs.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var thuoc = db.Thuocs
                    .Where(x => x.MaThuoc == id)
                    .Select(x => new
                    {
                        x.MaThuoc,
                        x.TenThuoc,
                        x.MaNhaCungCap,
                        x.DonViTinh,
                        x.MoTa,
                        x.Gia,
                        x.TrangThai,
                        x.NgayTao
                    })
                    .SingleOrDefault();

                return Ok(thuoc);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-thuoc")]
        [HttpPost]
        public IActionResult CreateThuoc(Thuoc model)
        {
            db.Thuocs.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-thuoc")]
        [HttpPost]
        public IActionResult UpdateThuoc(Thuoc model)
        {
            var thuoc = db.Thuocs.SingleOrDefault(x => x.MaThuoc == model.MaThuoc);
            if (thuoc != null)
            {
                thuoc.TenThuoc = model.TenThuoc;
                thuoc.MaNhaCungCap = model.MaNhaCungCap;
                thuoc.DonViTinh = model.DonViTinh;
                thuoc.MoTa = model.MoTa;
                thuoc.Gia = model.Gia;
                thuoc.TrangThai = model.TrangThai;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã thuốc không tồn tại");
            }
        }

        [Route("delete-thuoc")]
        [HttpPost]
        public IActionResult DeleteThuoc(int id)
        {
            var thuoc = db.Thuocs.SingleOrDefault(x => x.MaThuoc == id);
            if (thuoc != null)
            {
                db.Thuocs.Remove(thuoc);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã thuốc không tồn tại");
            }
        }
    }
}
