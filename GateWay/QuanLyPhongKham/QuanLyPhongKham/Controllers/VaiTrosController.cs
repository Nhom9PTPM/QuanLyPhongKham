using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTrosController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.VaiTros.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var data = db.VaiTros.SingleOrDefault(x => x.MaVaiTro == id);
            return Ok(data);
        }

        [HttpPost("create")]
        public IActionResult Create(VaiTro model)
        {
            db.VaiTros.Add(model);
            db.SaveChanges();
            return Ok("Thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(VaiTro model)
        {
            var item = db.VaiTros.SingleOrDefault(x => x.MaVaiTro == model.MaVaiTro);
            if (item != null)
            {
                item.TenVaiTro = model.TenVaiTro;
                item.MoTa = model.MoTa;
                db.SaveChanges();
                return Ok("Thành công");
            }
            return BadRequest("Không tồn tại");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.VaiTros.SingleOrDefault(x => x.MaVaiTro == id);
            if (item != null)
            {
                db.VaiTros.Remove(item);
                db.SaveChanges();
                return Ok("Xóa thành công");
            }
            return BadRequest("Không tồn tại");
        }
    }
}
