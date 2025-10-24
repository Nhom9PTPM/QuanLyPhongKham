using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietDonThuocsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.ChiTietDonThuocs.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.ChiTietDonThuocs.SingleOrDefault(x => x.MaChiTietDon == id));
        }

        [HttpPost("create")]
        public IActionResult Create(ChiTietDonThuoc model)
        {
            db.ChiTietDonThuocs.Add(model);
            db.SaveChanges();
            return Ok("Thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(ChiTietDonThuoc model)
        {
            var item = db.ChiTietDonThuocs.SingleOrDefault(x => x.MaChiTietDon == model.MaChiTietDon);
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
            var item = db.ChiTietDonThuocs.SingleOrDefault(x => x.MaChiTietDon == id);
            if (item != null)
            {
                db.ChiTietDonThuocs.Remove(item);
                db.SaveChanges();
                return Ok("Đã xóa");
            }
            return BadRequest();
        }
    }
}
