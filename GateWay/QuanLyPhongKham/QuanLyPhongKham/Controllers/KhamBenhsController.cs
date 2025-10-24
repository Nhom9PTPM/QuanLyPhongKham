using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhamBenhsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.KhamBenhs.Where(x => x.DaXoa == false));
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.KhamBenhs.SingleOrDefault(x => x.MaKham == id));
        }

        [HttpPost("create")]
        public IActionResult Create(KhamBenh model)
        {
            db.KhamBenhs.Add(model);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update(KhamBenh model)
        {
            var item = db.KhamBenhs.SingleOrDefault(x => x.MaKham == model.MaKham);
            if (item != null)
            {
                item.ChanDoan = model.ChanDoan;
                item.GhiChu = model.GhiChu;
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.KhamBenhs.SingleOrDefault(x => x.MaKham == id);
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
