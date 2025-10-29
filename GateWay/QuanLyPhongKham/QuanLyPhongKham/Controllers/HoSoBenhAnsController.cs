using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoSoBenhAnsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.HoSoBenhAns.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var hs = db.HoSoBenhAns
                    .Where(x => x.MaHoSo == id)
                    .Select(x => new
                    {
                        x.MaHoSo,
                        x.MaBenhNhan,
                        x.TomTatBenhLy,
                        x.ChanDoanChinh,
                        x.LichSuBenhLy,
                        x.TapTinDinhKem,
                        x.NgayLap,
                        x.NguoiLap,
                        x.DaXoa
                    })
                    .SingleOrDefault();
                return Ok(hs);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-hosobenhan")]
        [HttpPost]
        public IActionResult CreateHoSoBenhAn(HoSoBenhAn model)
        {
            db.HoSoBenhAns.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-hosobenhan")]
        [HttpPost]
        public IActionResult UpdateHoSoBenhAn(HoSoBenhAn model)
        {
            var hs = db.HoSoBenhAns.SingleOrDefault(x => x.MaHoSo == model.MaHoSo);
            if (hs != null)
            {
                hs.MaBenhNhan = model.MaBenhNhan;
                hs.TomTatBenhLy = model.TomTatBenhLy;
                hs.ChanDoanChinh = model.ChanDoanChinh;
                hs.LichSuBenhLy = model.LichSuBenhLy;
                hs.TapTinDinhKem = model.TapTinDinhKem;
                hs.NgayLap = model.NgayLap;
                hs.NguoiLap = model.NguoiLap;
                hs.DaXoa = model.DaXoa;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã hồ sơ bệnh án không tồn tại");
            }
        }

        [Route("delete-hosobenhan")]
        [HttpPost]
        public IActionResult DeleteHoSoBenhAn(int id)
        {
            var hs = db.HoSoBenhAns.SingleOrDefault(x => x.MaHoSo == id);
            if (hs != null)
            {
                db.HoSoBenhAns.Remove(hs);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã hồ sơ bệnh án không tồn tại");
            }
        }
    }
}
