using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TonKhoThuocsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.TonKhoThuocs.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = db.TonKhoThuocs
                .Where(x => x.MaTonKho == id)
                .Select(x => new
                {
                    x.MaTonKho,
                    x.MaThuoc,
                    x.SoLuong,
                    x.MaKho,
                    x.NgayCapNhat
                })
                .SingleOrDefault();

            if (item != null)
                return Ok(item);
            else
                return NotFound("Không tìm thấy kho thuốc");
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(TonKhoThuoc model)
        {
            db.TonKhoThuocs.Add(model);
            db.SaveChanges();
            return Ok("Thêm kho thuốc thành công");
        }

        [Route("update")]
        [HttpPost]
        public IActionResult Update(TonKhoThuoc model)
        {
            var item = db.TonKhoThuocs.SingleOrDefault(x => x.MaTonKho == model.MaTonKho);
            if (item != null)
            {
                item.MaThuoc = model.MaThuoc;
                item.SoLuong = model.SoLuong;
                item.MaKho = model.MaKho;
                item.NgayCapNhat = model.NgayCapNhat;
                db.SaveChanges();
                return Ok("Cập nhật kho thuốc thành công");
            }
            else
            {
                return NotFound("Kho thuốc không tồn tại");
            }
        }

        [Route("delete/{id}")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = db.TonKhoThuocs.SingleOrDefault(x => x.MaTonKho == id);
            if (item != null)
            {
                db.TonKhoThuocs.Remove(item);
                db.SaveChanges();
                return Ok("Xóa kho thuốc thành công");
            }
            else
            {
                return NotFound("Kho thuốc không tồn tại");
            }
        }
    }
}
