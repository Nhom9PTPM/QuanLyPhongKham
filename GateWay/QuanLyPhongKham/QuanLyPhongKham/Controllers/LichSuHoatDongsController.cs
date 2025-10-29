using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LichSuHoatDongController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.LichSuHoatDongs.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var item = db.LichSuHoatDongs
                    .Where(x => x.MaLichSu == id)
                    .Select(x => new
                    {
                        x.MaLichSu,
                        x.MaTaiKhoan,
                        x.HanhDong,
                        x.DongThoiGian,
                        x.DiaChiIP,
                        x.ChiTiet
                    })
                    .SingleOrDefault();

                return Ok(item);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-lichsuhoatdong")]
        [HttpPost]
        public IActionResult CreateLichSuHoatDong(LichSuHoatDong model)
        {
            db.LichSuHoatDongs.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-lichsuhoatdong")]
        [HttpPost]
        public IActionResult UpdateLichSuHoatDong(LichSuHoatDong model)
        {
            var item = db.LichSuHoatDongs.SingleOrDefault(x => x.MaLichSu == model.MaLichSu);
            if (item != null)
            {
                item.MaTaiKhoan = model.MaTaiKhoan;
                item.HanhDong = model.HanhDong;
                item.DongThoiGian = model.DongThoiGian;
                item.DiaChiIP = model.DiaChiIP;
                item.ChiTiet = model.ChiTiet;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã lịch sử hoạt động không tồn tại");
            }
        }

        [Route("delete-lichsuhoatdong")]
        [HttpPost]
        public IActionResult DeleteLichSuHoatDong(int id)
        {
            var item = db.LichSuHoatDongs.SingleOrDefault(x => x.MaLichSu == id);
            if (item != null)
            {
                db.LichSuHoatDongs.Remove(item);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã lịch sử hoạt động không tồn tại");
            }
        }
    }
}
