using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThongBaosController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.ThongBaos.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var tb = db.ThongBaos
                .Where(x => x.MaThongBao == id)
                .Select(x => new
                {
                    x.MaThongBao,
                    x.MaTaiKhoan,
                    x.TieuDe,
                    x.NoiDung,
                    x.DaDoc,
                    x.NgayTao
                })
                .SingleOrDefault();

            if (tb != null)
                return Ok(tb);
            else
                return NotFound("Không tìm thấy thông báo");
        }

        [Route("create-thongbao")]
        [HttpPost]
        public IActionResult CreateThongBao(ThongBao model)
        {
            db.ThongBaos.Add(model);
            db.SaveChanges();
            return Ok("Tạo thông báo thành công");
        }

        [Route("update-thongbao")]
        [HttpPost]
        public IActionResult UpdateThongBao(ThongBao model)
        {
            var tb = db.ThongBaos.SingleOrDefault(x => x.MaThongBao == model.MaThongBao);
            if (tb != null)
            {
                tb.MaTaiKhoan = model.MaTaiKhoan;
                tb.TieuDe = model.TieuDe;
                tb.NoiDung = model.NoiDung;
                tb.DaDoc = model.DaDoc;
                // NgayTao giữ nguyên
                db.SaveChanges();
                return Ok("Cập nhật thông báo thành công");
            }
            else
            {
                return NotFound("Thông báo không tồn tại");
            }
        }

        [Route("delete-thongbao/{id}")]
        [HttpPost]
        public IActionResult DeleteThongBao(int id)
        {
            var tb = db.ThongBaos.SingleOrDefault(x => x.MaThongBao == id);
            if (tb != null)
            {
                db.ThongBaos.Remove(tb);
                db.SaveChanges();
                return Ok("Xóa thông báo thành công");
            }
            else
            {
                return NotFound("Thông báo không tồn tại");
            }
        }
    }
}
