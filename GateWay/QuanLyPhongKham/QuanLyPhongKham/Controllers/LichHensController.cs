using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichHensController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.LichHens.Where(x => x.DaXoa == false));
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.LichHens.SingleOrDefault(x => x.MaLichHen == id));
        }

        [HttpPost("create")]
        public IActionResult Create(LichHen model)
        {
            db.LichHens.Add(model);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update(LichHen model)
        {
            var item = db.LichHens.SingleOrDefault(x => x.MaLichHen == model.MaLichHen);
            if (item != null)
            {
                item.TrangThai = model.TrangThai;
                item.GhiChu = model.GhiChu;
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.LichHens.SingleOrDefault(x => x.MaLichHen == id);
            if (item != null)
            {
                item.DaXoa = true;
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
