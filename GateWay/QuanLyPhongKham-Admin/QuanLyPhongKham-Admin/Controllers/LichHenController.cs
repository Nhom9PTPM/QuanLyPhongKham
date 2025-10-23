using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
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

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var list = db.LichHens
                    .Include(x => x.BenhNhan)
                    .Select(x => new
                    {
                        x.MaLichHen,
                        x.NgayHen,
                        x.GioHen,
                        x.TrangThai,
                        x.GhiChu,
                        BenhNhan = x.BenhNhan.HoTen
                    })
                    .OrderByDescending(x => x.MaLichHen)
                    .ToList();
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var lh = db.LichHens
                    .Include(x => x.BenhNhan)
                    .Where(x => x.MaLichHen == id)
                    .Select(x => new
                    {
                        x.MaLichHen,
                        x.NgayHen,
                        x.GioHen,
                        x.TrangThai,
                        x.GhiChu,
                        BenhNhan = x.BenhNhan.HoTen
                    })
                    .SingleOrDefault();
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
            try
            {
                model.TrangThai = "Chờ duyệt";
                db.LichHens.Add(model);
                db.SaveChanges();
                return Ok("Tạo lịch hẹn thành công");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("update-lichhen")]
        [HttpPost]
        public IActionResult UpdateLichHen(LichHen model)
        {
            try
            {
                var lh = db.LichHens.SingleOrDefault(x => x.MaLichHen == model.MaLichHen);
                if (lh != null)
                {
                    lh.NgayHen = model.NgayHen != default ? model.NgayHen : lh.NgayHen;
                    lh.GioHen = model.GioHen ?? lh.GioHen;
                    lh.GhiChu = model.GhiChu ?? lh.GhiChu;
                    lh.TrangThai = model.TrangThai ?? lh.TrangThai;
                    db.SaveChanges();
                    return Ok("Cập nhật lịch hẹn thành công");
                }
                return Ok("Không tìm thấy lịch hẹn");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("delete-lichhen/{id}")]
        [HttpGet]
        public IActionResult DeleteLichHen(int id)
        {
            try
            {
                var obj = db.LichHens.SingleOrDefault(x => x.MaLichHen == id);
                if (obj != null)
                {
                    db.LichHens.Remove(obj);
                    db.SaveChanges();
                    return Ok("Xóa lịch hẹn thành công");
                }
                return Ok("Không tìm thấy lịch hẹn");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("duyet-lichhen/{id}")]
        [HttpGet]
        public IActionResult DuyetLichHen(int id)
        {
            try
            {
                var lh = db.LichHens.SingleOrDefault(x => x.MaLichHen == id);
                if (lh != null)
                {
                    lh.TrangThai = "Đã duyệt";
                    db.SaveChanges();
                    return Ok("Duyệt lịch hẹn thành công");
                }
                return Ok("Không tìm thấy lịch hẹn");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
