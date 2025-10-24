using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.HoaDons.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.HoaDons.SingleOrDefault(x => x.MaHoaDon == id));
        }

        [HttpPost("create")]
        public IActionResult Create(HoaDon model)
        {
            db.HoaDons.Add(model);
            db.SaveChanges();
            return Ok("Tạo hóa đơn thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(HoaDon model)
        {
            var item = db.HoaDons.SingleOrDefault(x => x.MaHoaDon == model.MaHoaDon);
            if (item != null)
            {
                item.TongTien = model.TongTien;
                item.TrangThai = model.TrangThai;
                db.SaveChanges();
                return Ok("Cập nhật thành công");
            }
            return BadRequest("Không tồn tại");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.HoaDons.SingleOrDefault(x => x.MaHoaDon == id);
            if (item != null)
            {
                db.HoaDons.Remove(item);
                db.SaveChanges();
                return Ok("Đã xóa hóa đơn");
            }
            return BadRequest();
        }
    }
}
