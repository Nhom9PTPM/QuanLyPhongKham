using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietHoaDonsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.ChiTietHoaDons.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var chiTiet = db.ChiTietHoaDons
                    .Where(x => x.MaChiTietHoaDon == id)
                    .Select(x => new
                    {
                        x.MaChiTietHoaDon,
                        x.MaHoaDon,
                        x.MaDichVu,
                        x.MaThuoc,
                        x.SoLuong,
                        x.DonGia
                    })
                    .SingleOrDefault();
                return Ok(chiTiet);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-chitiethoadon")]
        [HttpPost]
        public IActionResult CreateChiTietHoaDon(ChiTietHoaDon model)
        {
            db.ChiTietHoaDons.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-chitiethoadon")]
        [HttpPost]
        public IActionResult UpdateChiTietHoaDon(ChiTietHoaDon model)
        {
            var chiTiet = db.ChiTietHoaDons.SingleOrDefault(x => x.MaChiTietHoaDon == model.MaChiTietHoaDon);
            if (chiTiet != null)
            {
                chiTiet.MaHoaDon = model.MaHoaDon;
                chiTiet.MaDichVu = model.MaDichVu;
                chiTiet.MaThuoc = model.MaThuoc;
                chiTiet.SoLuong = model.SoLuong;
                chiTiet.DonGia = model.DonGia;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã chi tiết hóa đơn không tồn tại");
            }
        }

        [Route("delete-chitiethoadon")]
        [HttpPost]
        public IActionResult DeleteChiTietHoaDon(int id)
        {
            var chiTiet = db.ChiTietHoaDons.SingleOrDefault(x => x.MaChiTietHoaDon == id);
            if (chiTiet != null)
            {
                db.ChiTietHoaDons.Remove(chiTiet);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã chi tiết hóa đơn không tồn tại");
            }
        }
    }
}
