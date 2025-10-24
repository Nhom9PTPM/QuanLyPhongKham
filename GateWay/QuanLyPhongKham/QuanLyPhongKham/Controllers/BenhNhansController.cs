using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenhNhansController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.BenhNhans.Where(x => x.DaXoa == false));
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == id));
        }

        [HttpPost("create")]
        public IActionResult Create(BenhNhan model)
        {
            db.BenhNhans.Add(model);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update(BenhNhan model)
        {
            var item = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == model.MaBenhNhan);
            if (item != null)
            {
                item.HoTen = model.HoTen;
                item.Email = model.Email;
                item.SoDienThoai = model.SoDienThoai;
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == id);
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
