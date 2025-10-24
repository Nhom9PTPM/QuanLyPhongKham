using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.NguoiDungs.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == id));
        }

        [HttpPost("create")]
        public IActionResult Create(NguoiDung model)
        {
            db.NguoiDungs.Add(model);
            db.SaveChanges();
            return Ok("Thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(NguoiDung model)
        {
            var item = db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == model.MaNguoiDung);
            if (item != null)
            {
                item.HoTen = model.HoTen;
                item.Email = model.Email;
                item.GioiTinh = model.GioiTinh;
                item.DiaChi = model.DiaChi;
                item.SoDienThoai = model.SoDienThoai;
                db.SaveChanges();
                return Ok("Thành công");
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.NguoiDungs.SingleOrDefault(x => x.MaNguoiDung == id);
            if (item != null)
            {
                db.NguoiDungs.Remove(item);
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
