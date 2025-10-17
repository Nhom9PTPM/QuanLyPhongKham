using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_NguoiDung.Models;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LichHenController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;

        public LichHenController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("create-lichhen")]
        [HttpPost]
        public IActionResult CreateLichHen(LichHen model)
        {
            try
            {
                model.TrangThai = "Chờ duyệt";
                model.NgayTao = DateTime.Now;
                db.LichHens.Add(model);
                db.SaveChanges();
                return Ok("Đặt lịch hẹn thành công");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("get-by-benhnhan/{id}")]
        [HttpGet]
        public IActionResult GetByBenhNhan(int id)
        {
            try
            {
                var list = db.LichHens
                    .Where(x => x.MaBenhNhan == id)
                    .OrderByDescending(x => x.NgayHen)
                    .Select(x => new
                    {
                        x.MaLichHen,
                        x.NgayHen,
                        x.GioHen,
                        x.TrangThai,
                        x.GhiChu
                    })
                    .ToList();
                return Ok(new { result = list });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("huy-lich/{id}")]
        [HttpGet]
        public IActionResult HuyLich(int id)
        {
            try
            {
                var lh = db.LichHens.SingleOrDefault(x => x.MaLichHen == id);
                if (lh != null && lh.TrangThai == "Chờ duyệt")
                {
                    lh.TrangThai = "Đã hủy";
                    db.SaveChanges();
                    return Ok("Hủy lịch thành công");
                }
                return Ok("Không thể hủy lịch đã duyệt hoặc không tồn tại");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
