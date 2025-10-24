using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacSisController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.BacSis.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.BacSis.SingleOrDefault(x => x.MaBacSi == id));
        }

        [HttpPost("create")]
        public IActionResult Create(BacSi model)
        {
            db.BacSis.Add(model);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update(BacSi model)
        {
            var item = db.BacSis.SingleOrDefault(x => x.MaBacSi == model.MaBacSi);
            if (item != null)
            {
                item.BangCap = model.BangCap;
                item.ChuyenKhoa = model.ChuyenKhoa;
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.BacSis.SingleOrDefault(x => x.MaBacSi == id);
            if (item != null)
            {
                db.BacSis.Remove(item);
                db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
