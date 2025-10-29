using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TapTinsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.TapTins.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var tapTin = db.TapTins
                .Where(x => x.MaTapTin == id)
                .Select(x => new
                {
                    x.MaTapTin,
                    x.TenTapTin,
                    x.DuongDan,
                    x.KichThuoc,
                    x.DinhDang,
                    x.MaHoSo,
                    x.NgayTao
                })
                .SingleOrDefault();

            if (tapTin != null)
                return Ok(tapTin);
            else
                return NotFound("Không tìm thấy tập tin");
        }

        [Route("create-taptin")]
        [HttpPost]
        public IActionResult CreateTapTin(TapTin model)
        {
            db.TapTins.Add(model);
            db.SaveChanges();
            return Ok("Tạo tập tin thành công");
        }

        [Route("update-taptin")]
        [HttpPost]
        public IActionResult UpdateTapTin(TapTin model)
        {
            var tapTin = db.TapTins.SingleOrDefault(x => x.MaTapTin == model.MaTapTin);
            if (tapTin != null)
            {
                tapTin.TenTapTin = model.TenTapTin;
                tapTin.DuongDan = model.DuongDan;
                tapTin.KichThuoc = model.KichThuoc;
                tapTin.DinhDang = model.DinhDang;
                tapTin.MaHoSo = model.MaHoSo;
                // NgayTao không cập nhật
                db.SaveChanges();
                return Ok("Cập nhật tập tin thành công");
            }
            else
            {
                return NotFound("Tập tin không tồn tại");
            }
        }

        [Route("delete-taptin/{id}")]
        [HttpPost]
        public IActionResult DeleteTapTin(int id)
        {
            var tapTin = db.TapTins.SingleOrDefault(x => x.MaTapTin == id);
            if (tapTin != null)
            {
                db.TapTins.Remove(tapTin);
                db.SaveChanges();
                return Ok("Xóa tập tin thành công");
            }
            else
            {
                return NotFound("Tập tin không tồn tại");
            }
        }
    }
}
