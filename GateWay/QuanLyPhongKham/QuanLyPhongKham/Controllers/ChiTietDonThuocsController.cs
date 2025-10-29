using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiTietDonThuocsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.ChiTietDonThuocs.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var ct = db.ChiTietDonThuocs
                    .Where(x => x.MaChiTietDon == id)
                    .Select(x => new
                    {
                        x.MaChiTietDon,
                        x.MaDonThuoc,
                        x.MaThuoc,
                        x.SoLuong,
                        x.CachDung,
                        x.DonGia
                    })
                    .SingleOrDefault();
                return Ok(ct);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-chitietdonthuoc")]
        [HttpPost]
        public IActionResult CreateChiTietDonThuoc(ChiTietDonThuoc model)
        {
            db.ChiTietDonThuocs.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-chitietdonthuoc")]
        [HttpPost]
        public IActionResult UpdateChiTietDonThuoc(ChiTietDonThuoc model)
        {
            var ct = db.ChiTietDonThuocs.SingleOrDefault(x => x.MaChiTietDon == model.MaChiTietDon);
            if (ct != null)
            {
                ct.MaDonThuoc = model.MaDonThuoc;
                ct.MaThuoc = model.MaThuoc;
                ct.SoLuong = model.SoLuong;
                ct.CachDung = model.CachDung;
                ct.DonGia = model.DonGia;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã chi tiết đơn thuốc không tồn tại");
            }
        }

        [Route("delete-chitietdonthuoc")]
        [HttpPost]
        public IActionResult DeleteChiTietDonThuoc(int id)
        {
            var ct = db.ChiTietDonThuocs.SingleOrDefault(x => x.MaChiTietDon == id);
            if (ct != null)
            {
                db.ChiTietDonThuocs.Remove(ct);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã chi tiết đơn thuốc không tồn tại");
            }
        }
    }
}
