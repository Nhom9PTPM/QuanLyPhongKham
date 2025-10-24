using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TonKhoThuocsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.TonKhoThuocs.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.TonKhoThuocs.SingleOrDefault(x => x.MaTonKho == id));
        }

        [HttpPost("create")]
        public IActionResult Create(TonKhoThuoc model)
        {
            db.TonKhoThuocs.Add(model);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update(TonKhoThuoc model)
        {
            var item = db.TonKhoThuocs.SingleOrDefault(x => x.MaTonKho == model.MaTonKho);
            if (item != null)
            {
                item.SoLuong = model.SoLuong;
                item.MaKho = model.MaKho;
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.TonKhoThuocs.SingleOrDefault(x => x.MaTonKho == id);
            if (item != null)
            {
                db.TonKhoThuocs.Remove(item);
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
