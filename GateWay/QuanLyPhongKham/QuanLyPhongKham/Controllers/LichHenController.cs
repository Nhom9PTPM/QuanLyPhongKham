using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichHenController : ControllerBase
    {
        private QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = db.LichHens
                .Select(x => new
                {
                    x.MaLichHen,
                    x.NgayHen,
                    x.GioHen,
                    x.TrangThai,
                    BenhNhan = x.BenhNhan != null ? x.BenhNhan.HoTen : ""
                })
                .OrderByDescending(x => x.NgayHen)
                .ToList();
            return Ok(data);
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var lh = db.LichHens.SingleOrDefault(x => x.MaLichHen == id);
                return Ok(lh);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-lichhen")]
        [HttpPost]
        public IActionResult CreateLichHen(LichHen model)
        {
            model.NgayTao = DateTime.UtcNow;
            db.LichHens.Add(model);
            db.SaveChanges();
            return Ok("Thêm lịch hẹn thành công");
        }

        [Route("update-lichhen")]
        [HttpPost]
        public IActionResult UpdateLichHen(LichHen model)
        {
            var lh = db.LichHens.SingleOrDefault(x => x.MaLichHen == model.MaLichHen);
            if (lh != null)
            {
                lh.TrangThai = model.TrangThai ?? lh.TrangThai;
                lh.GhiChu = model.GhiChu ?? lh.GhiChu;
                lh.NgayHen = model.NgayHen ?? lh.NgayHen;
                lh.GioHen = model.GioHen ?? lh.GioHen;
                db.SaveChanges();
                return Ok("Cập nhật lịch hẹn thành công");
            }
            return Ok("Không tìm thấy lịch hẹn");
        }

        [Route("delete-lichhen/{id}")]
        [HttpGet]
        public IActionResult DeleteLichHen(int id)
        {
            var lh = db.LichHens.SingleOrDefault(x => x.MaLichHen == id);
            if (lh != null)
            {
                db.LichHens.Remove(lh);
                db.SaveChanges();
                return Ok("Xóa lịch hẹn thành công");
            }
            return Ok("Không tìm thấy lịch hẹn");
        }
    }
}
