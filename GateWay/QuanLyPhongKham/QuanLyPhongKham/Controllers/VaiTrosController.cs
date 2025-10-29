using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaiTrosController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.VaiTros.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var item = db.VaiTros
                .Where(x => x.MaVaiTro == id)
                .Select(x => new
                {
                    x.MaVaiTro,
                    x.TenVaiTro,
                    x.MoTa,
                    x.NgayTao,
                    x.DaXoa
                })
                .SingleOrDefault();

            if (item != null)
                return Ok(item);
            else
                return NotFound("Không tìm thấy vai trò");
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(VaiTro model)
        {
            db.VaiTros.Add(model);
            db.SaveChanges();
            return Ok("Thêm vai trò thành công");
        }

        [Route("update")]
        [HttpPost]
        public IActionResult Update(VaiTro model)
        {
            var item = db.VaiTros.SingleOrDefault(x => x.MaVaiTro == model.MaVaiTro);
            if (item != null)
            {
                item.TenVaiTro = model.TenVaiTro;
                item.MoTa = model.MoTa;
                item.NgayTao = model.NgayTao;
                item.DaXoa = model.DaXoa;
                db.SaveChanges();
                return Ok("Cập nhật vai trò thành công");
            }
            else
            {
                return NotFound("Vai trò không tồn tại");
            }
        }

        [Route("delete/{id}")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = db.VaiTros.SingleOrDefault(x => x.MaVaiTro == id);
            if (item != null)
            {
                db.VaiTros.Remove(item);
                db.SaveChanges();
                return Ok("Xóa vai trò thành công");
            }
            else
            {
                return NotFound("Vai trò không tồn tại");
            }
        }
    }
}
