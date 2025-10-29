using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoaDonsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.HoaDons.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var hoaDon = db.HoaDons
                    .Where(x => x.MaHoaDon == id)
                    .Select(x => new
                    {
                        x.MaHoaDon,
                        x.MaBenhNhan,
                        x.MaNguoiThu,
                        x.NgayLap,
                        x.TongTien,
                        x.PhuongThucThanhToan,
                        x.TrangThai,
                        x.GhiChu
                    })
                    .SingleOrDefault();
                return Ok(hoaDon);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-hoadon")]
        [HttpPost]
        public IActionResult CreateHoaDon(HoaDon model)
        {
            db.HoaDons.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-hoadon")]
        [HttpPost]
        public IActionResult UpdateHoaDon(HoaDon model)
        {
            var hd = db.HoaDons.SingleOrDefault(x => x.MaHoaDon == model.MaHoaDon);
            if (hd != null)
            {
                hd.MaBenhNhan = model.MaBenhNhan;
                hd.MaNguoiThu = model.MaNguoiThu;
                hd.NgayLap = model.NgayLap;
                hd.TongTien = model.TongTien;
                hd.PhuongThucThanhToan = model.PhuongThucThanhToan;
                hd.TrangThai = model.TrangThai;
                hd.GhiChu = model.GhiChu;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã hóa đơn không tồn tại");
            }
        }

        [Route("delete-hoadon")]
        [HttpPost]
        public IActionResult DeleteHoaDon(int id)
        {
            var hd = db.HoaDons.SingleOrDefault(x => x.MaHoaDon == id);
            if (hd != null)
            {
                db.HoaDons.Remove(hd);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã hóa đơn không tồn tại");
            }
        }
    }
}
