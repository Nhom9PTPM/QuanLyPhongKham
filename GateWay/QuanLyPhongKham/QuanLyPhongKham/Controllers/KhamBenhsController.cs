using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhamBenhsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.KhamBenhs.Where(x => x.DaXoa == false).ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var item = db.KhamBenhs
                    .Where(x => x.MaKham == id && x.DaXoa == false)
                    .Select(x => new
                    {
                        x.MaKham,
                        x.MaHoSo,
                        x.MaBenhNhan,
                        x.MaBacSi,
                        x.NgayKham,
                        x.ChanDoan,
                        x.ChiDinhXN,
                        x.ChiDinhCLS,
                        x.GhiChu,
                        x.NguoiThucHien,
                        x.NgayTao,
                        x.DaXoa
                    })
                    .SingleOrDefault();

                return Ok(item);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-khambenh")]
        [HttpPost]
        public IActionResult CreateKhamBenh(KhamBenh model)
        {
            db.KhamBenhs.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-khambenh")]
        [HttpPost]
        public IActionResult UpdateKhamBenh(KhamBenh model)
        {
            var item = db.KhamBenhs.SingleOrDefault(x => x.MaKham == model.MaKham && x.DaXoa == false);
            if (item != null)
            {
                item.MaHoSo = model.MaHoSo;
                item.MaBenhNhan = model.MaBenhNhan;
                item.MaBacSi = model.MaBacSi;
                item.NgayKham = model.NgayKham;
                item.ChanDoan = model.ChanDoan;
                item.ChiDinhXN = model.ChiDinhXN;
                item.ChiDinhCLS = model.ChiDinhCLS;
                item.GhiChu = model.GhiChu;
                item.NguoiThucHien = model.NguoiThucHien;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã khám bệnh không tồn tại");
            }
        }

        [Route("delete-khambenh")]
        [HttpPost]
        public IActionResult DeleteKhamBenh(int id)
        {
            var item = db.KhamBenhs.SingleOrDefault(x => x.MaKham == id && x.DaXoa == false);
            if (item != null)
            {
                item.DaXoa = true;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã khám bệnh không tồn tại");
            }
        }
    }
}
