using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BacSisController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.BacSis.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var bacSi = db.BacSis
                    .Where(x => x.MaBacSi == id)
                    .Select(x => new
                    {
                        x.MaBacSi,
                        x.MaNguoiDung,
                        x.ChuyenKhoa,
                        x.BangCap,
                        x.KinhNghiem,
                        x.SoPhong,
                        x.TrangThai,
                        x.NgayTao
                    })
                    .SingleOrDefault();
                return Ok(bacSi);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-bacsi")]
        [HttpPost]
        public IActionResult CreateBacSi(BacSi model)
        {
            db.BacSis.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-bacsi")]
        [HttpPost]
        public IActionResult UpdateBacSi(BacSi model)
        {
            var bacSi = db.BacSis.SingleOrDefault(x => x.MaBacSi == model.MaBacSi);
            if (bacSi != null)
            {
                bacSi.MaNguoiDung = model.MaNguoiDung;
                bacSi.ChuyenKhoa = model.ChuyenKhoa;
                bacSi.BangCap = model.BangCap;
                bacSi.KinhNghiem = model.KinhNghiem;
                bacSi.SoPhong = model.SoPhong;
                bacSi.TrangThai = model.TrangThai;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã bác sĩ không tồn tại");
            }
        }
        [Route("delete-bacsi")]
        [HttpPost]
        public IActionResult DeleteBacSi(int id)
        {
            var bacSi = db.BacSis.SingleOrDefault(x => x.MaBacSi == id);
            if (bacSi != null)
            {
                db.BacSis.Remove(bacSi);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã bác sĩ không tồn tại");
            }
        }
    }
}
