using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaosController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.ThongBaos.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.ThongBaos.SingleOrDefault(x => x.MaThongBao == id));
        }

        [HttpPost("create")]
        public IActionResult Create(ThongBao model)
        {
            db.ThongBaos.Add(model);
            db.SaveChanges();
            return Ok("Thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(ThongBao model)
        {
            var item = db.ThongBaos.SingleOrDefault(x => x.MaThongBao == model.MaThongBao);
            if (item != null)
            {
                item.DaDoc = model.DaDoc;
                db.SaveChanges();
                return Ok("Cập nhật thành công");
            }
            return BadRequest("Không tồn tại");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.ThongBaos.SingleOrDefault(x => x.MaThongBao == id);
            if (item != null)
            {
                db.ThongBaos.Remove(item);
                db.SaveChanges();
                return Ok("Đã xóa");
            }
            return BadRequest();
        }
    }
}
