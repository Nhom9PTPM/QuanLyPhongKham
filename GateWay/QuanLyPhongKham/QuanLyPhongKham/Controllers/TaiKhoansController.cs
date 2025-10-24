using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoansController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.TaiKhoans.ToList());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.TaiKhoans.SingleOrDefault(x => x.MaTaiKhoan == id));
        }

        [HttpPost("create")]
        public IActionResult Create(TaiKhoan model)
        {
            db.TaiKhoans.Add(model);
            db.SaveChanges();
            return Ok("Tạo tài khoản thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(TaiKhoan model)
        {
            var item = db.TaiKhoans.SingleOrDefault(x => x.MaTaiKhoan == model.MaTaiKhoan);
            if (item != null)
            {
                item.MatKhau = model.MatKhau;
                item.TrangThai = model.TrangThai;
                db.SaveChanges();
                return Ok("Cập nhật thành công");
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.TaiKhoans.SingleOrDefault(x => x.MaTaiKhoan == id);
            if (item != null)
            {
                db.TaiKhoans.Remove(item);
                db.SaveChanges();
                return Ok("Đã xóa");
            }
            return BadRequest();
        }
    }
}
