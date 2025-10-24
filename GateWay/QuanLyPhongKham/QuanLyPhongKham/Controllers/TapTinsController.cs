using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TapTinsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.TapTins.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.TapTins.SingleOrDefault(x => x.MaTapTin == id));
        }

        [HttpPost("create")]
        public IActionResult Create(TapTin model)
        {
            db.TapTins.Add(model);
            db.SaveChanges();
            return Ok("Thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(TapTin model)
        {
            var item = db.TapTins.SingleOrDefault(x => x.MaTapTin == model.MaTapTin);
            if (item != null)
            {
                item.TenTapTin = model.TenTapTin;
                db.SaveChanges();
                return Ok("Cập nhật thành công");
            }
            return BadRequest("Không tồn tại");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.TapTins.SingleOrDefault(x => x.MaTapTin == id);
            if (item != null)
            {
                db.TapTins.Remove(item);
                db.SaveChanges();
                return Ok("Đã xóa");
            }
            return BadRequest();
        }
    }
}
