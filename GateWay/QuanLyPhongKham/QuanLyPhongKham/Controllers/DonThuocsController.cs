using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonThuocsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.DonThuocs.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var donThuoc = db.DonThuocs
                    .Where(x => x.MaDonThuoc == id)
                    .Select(x => new
                    {
                        x.MaDonThuoc,
                        x.MaKham,
                        x.MaBacSi,
                        x.NgayKe,
                        x.GhiChu,
                        x.NguoiKe,
                        x.DaXoa
                    })
                    .SingleOrDefault();
                return Ok(donThuoc);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-donthuoc")]
        [HttpPost]
        public IActionResult CreateDonThuoc(DonThuoc model)
        {
            db.DonThuocs.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-donthuoc")]
        [HttpPost]
        public IActionResult UpdateDonThuoc(DonThuoc model)
        {
            var donThuoc = db.DonThuocs.SingleOrDefault(x => x.MaDonThuoc == model.MaDonThuoc);
            if (donThuoc != null)
            {
                donThuoc.MaKham = model.MaKham;
                donThuoc.MaBacSi = model.MaBacSi;
                donThuoc.NgayKe = model.NgayKe;
                donThuoc.GhiChu = model.GhiChu;
                donThuoc.NguoiKe = model.NguoiKe;
                donThuoc.DaXoa = model.DaXoa;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã đơn thuốc không tồn tại");
            }
        }

        [Route("delete-donthuoc")]
        [HttpPost]
        public IActionResult DeleteDonThuoc(int id)
        {
            var donThuoc = db.DonThuocs.SingleOrDefault(x => x.MaDonThuoc == id);
            if (donThuoc != null)
            {
                db.DonThuocs.Remove(donThuoc);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã đơn thuốc không tồn tại");
            }
        }
    }
}
