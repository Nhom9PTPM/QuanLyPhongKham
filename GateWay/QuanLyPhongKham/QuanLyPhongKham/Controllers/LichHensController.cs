using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LichHensController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.LichHens.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var lichHen = db.LichHens
                    .Where(x => x.MaLichHen == id)
                    .Select(x => new
                    {
                        x.MaLichHen,
                        x.MaBenhNhan,
                        x.MaBacSi,
                        x.NgayBatDau,
                        x.NgayKetThuc,
                        x.GioHen,
                        x.TrangThai,
                        x.GhiChu,
                        x.NguoiTao,
                        x.NgayTao,
                        x.DaXoa
                    })
                    .SingleOrDefault();
                return Ok(lichHen);
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
            db.LichHens.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-lichhen")]
        [HttpPost]
        public IActionResult UpdateLichHen(LichHen model)
        {
            var lichHen = db.LichHens.SingleOrDefault(x => x.MaLichHen == model.MaLichHen);
            if (lichHen != null)
            {
                lichHen.MaBenhNhan = model.MaBenhNhan;
                lichHen.MaBacSi = model.MaBacSi;
                lichHen.NgayBatDau = model.NgayBatDau;
                lichHen.NgayKetThuc = model.NgayKetThuc;
                lichHen.GioHen = model.GioHen;
                lichHen.TrangThai = model.TrangThai;
                lichHen.GhiChu = model.GhiChu;
                lichHen.NguoiTao = model.NguoiTao;
                lichHen.NgayTao = model.NgayTao;
                lichHen.DaXoa = model.DaXoa;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã lịch hẹn không tồn tại");
            }
        }

        [Route("delete-lichhen")]
        [HttpPost]
        public IActionResult DeleteLichHen(int id)
        {
            var lichHen = db.LichHens.SingleOrDefault(x => x.MaLichHen == id);
            if (lichHen != null)
            {
                db.LichHens.Remove(lichHen);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã lịch hẹn không tồn tại");
            }
        }
    }
}
