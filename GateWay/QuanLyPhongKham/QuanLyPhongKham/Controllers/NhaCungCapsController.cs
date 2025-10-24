using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.NhaCungCaps.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == id));
        }

        [HttpPost("create")]
        public IActionResult Create(NhaCungCap model)
        {
            db.NhaCungCaps.Add(model);
            db.SaveChanges();
            return Ok("Thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(NhaCungCap model)
        {
            var item = db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == model.MaNhaCungCap);
            if (item != null)
            {
                item.TenNhaCungCap = model.TenNhaCungCap;
                item.Email = model.Email;
                item.SoDienThoai = model.SoDienThoai;
                item.DiaChi = model.DiaChi;
                db.SaveChanges();
                return Ok("Thành công");
            }
            return BadRequest("Không tồn tại");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == id);
            if (item != null)
            {
                db.NhaCungCaps.Remove(item);
                db.SaveChanges();
                return Ok("Xóa thành công");
            }
            return BadRequest();
        }
    }
}
