using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietHoaDonsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.ChiTietHoaDons.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.ChiTietHoaDons.SingleOrDefault(x => x.MaChiTietHoaDon == id));
        }

        [HttpPost("create")]
        public IActionResult Create(ChiTietHoaDon model)
        {
            db.ChiTietHoaDons.Add(model);
            db.SaveChanges();
            return Ok("Thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(ChiTietHoaDon model)
        {
            var item = db.ChiTietHoaDons.SingleOrDefault(x => x.MaChiTietHoaDon == model.MaChiTietHoaDon);
            if (item != null)
            {
                item.SoLuong = model.SoLuong;
                item.DonGia = model.DonGia;
                db.SaveChanges();
                return Ok("Cập nhật thành công");
            }
            return BadRequest("Không tồn tại");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.ChiTietHoaDons.SingleOrDefault(x => x.MaChiTietHoaDon == id);
            if (item != null)
            {
                db.ChiTietHoaDons.Remove(item);
                db.SaveChanges();
                return Ok("Đã xóa");
            }
            return BadRequest();
        }
    }
}
