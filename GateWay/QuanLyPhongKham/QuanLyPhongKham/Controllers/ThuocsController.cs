using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuocsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.Thuocs.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.Thuocs.SingleOrDefault(x => x.MaThuoc == id));
        }

        [HttpPost("create")]
        public IActionResult Create(Thuoc model)
        {
            db.Thuocs.Add(model);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update(Thuoc model)
        {
            var item = db.Thuocs.SingleOrDefault(x => x.MaThuoc == model.MaThuoc);
            if (item != null)
            {
                item.TenThuoc = model.TenThuoc;
                item.Gia = model.Gia;
                item.DonViTinh = model.DonViTinh;
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.Thuocs.SingleOrDefault(x => x.MaThuoc == id);
            if (item != null)
            {
                db.Thuocs.Remove(item);
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
