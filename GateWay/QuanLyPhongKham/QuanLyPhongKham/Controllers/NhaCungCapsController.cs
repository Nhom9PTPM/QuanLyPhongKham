using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Models;
using System.Linq;

namespace QuanLyPhongKham.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhaCungCapsController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db = new QuanLyPhongKhamContext();

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.NhaCungCaps.ToList());
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var item = db.NhaCungCaps
                    .Where(x => x.MaNhaCungCap == id)
                    .Select(x => new
                    {
                        x.MaNhaCungCap,
                        x.TenNhaCungCap,
                        x.DiaChi,
                        x.SoDienThoai,
                        x.Email,
                        x.NguoiLienHe,
                        x.GhiChu,
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

        [Route("create-nhacungcap")]
        [HttpPost]
        public IActionResult CreateNhaCungCap(NhaCungCap model)
        {
            db.NhaCungCaps.Add(model);
            db.SaveChanges();
            return Ok("Thực hiện thành công");
        }

        [Route("update-nhacungcap")]
        [HttpPost]
        public IActionResult UpdateNhaCungCap(NhaCungCap model)
        {
            var item = db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == model.MaNhaCungCap);
            if (item != null)
            {
                item.TenNhaCungCap = model.TenNhaCungCap;
                item.DiaChi = model.DiaChi;
                item.SoDienThoai = model.SoDienThoai;
                item.Email = model.Email;
                item.NguoiLienHe = model.NguoiLienHe;
                item.GhiChu = model.GhiChu;
                item.NgayTao = model.NgayTao;
                item.DaXoa = model.DaXoa;
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã nhà cung cấp không tồn tại");
            }
        }

        [Route("delete-nhacungcap")]
        [HttpPost]
        public IActionResult DeleteNhaCungCap(int id)
        {
            var item = db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == id);
            if (item != null)
            {
                db.NhaCungCaps.Remove(item);
                db.SaveChanges();
                return Ok("Thực hiện thành công");
            }
            else
            {
                return Ok("Mã nhà cung cấp không tồn tại");
            }
        }
    }
}
