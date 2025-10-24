using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonThuocsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(db.DonThuocs.Where(x => x.DaXoa == false));
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(db.DonThuocs.SingleOrDefault(x => x.MaDonThuoc == id));
        }

        [HttpPost("create")]
        public IActionResult Create(DonThuoc model)
        {
            db.DonThuocs.Add(model);
            db.SaveChanges();
            return Ok("Thành công");
        }

        [HttpPost("update")]
        public IActionResult Update(DonThuoc model)
        {
            var item = db.DonThuocs.SingleOrDefault(x => x.MaDonThuoc == model.MaDonThuoc);
            if (item != null)
            {
                item.GhiChu = model.GhiChu;
                db.SaveChanges();
                return Ok("Cập nhật thành công");
            }
            return BadRequest("Không tồn tại");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.DonThuocs.SingleOrDefault(x => x.MaDonThuoc == id);
            if (item != null)
            {
                item.DaXoa = true;
                db.SaveChanges();
                return Ok("Đã xóa mềm");
            }
            return BadRequest();
        }
    }
}
