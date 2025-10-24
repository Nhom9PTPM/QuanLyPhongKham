using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuHoatDongsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.LichSuHoatDongs.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.LichSuHoatDongs.SingleOrDefault(x => x.MaLichSu == id));
        }

        [HttpPost("create")]
        public IActionResult Create(LichSuHoatDong model)
        {
            db.LichSuHoatDongs.Add(model);
            db.SaveChanges();
            return Ok("Thành công");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.LichSuHoatDongs.SingleOrDefault(x => x.MaLichSu == id);
            if (item != null)
            {
                db.LichSuHoatDongs.Remove(item);
                db.SaveChanges();
                return Ok("Đã xóa");
            }
            return BadRequest();
        }
    }
}
