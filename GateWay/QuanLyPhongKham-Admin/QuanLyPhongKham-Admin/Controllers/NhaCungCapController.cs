using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;

        public NhaCungCapController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(NhaCungCap model)
        {
            db.NhaCungCaps.Add(model);
            db.SaveChanges();
            return Ok("OK");
        }

        [Route("update")]
        [HttpPost]
        public IActionResult Update(NhaCungCap model)
        {
            var obj = db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == model.MaNhaCungCap);
            if (obj != null)
            {
                obj.TenNhaCungCap = model.TenNhaCungCap;
                obj.DiaChi = model.DiaChi;
                obj.SoDienThoai = model.SoDienThoai;
                db.SaveChanges();
            }
            return Ok("OK");
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == id);
            db.NhaCungCaps.Remove(obj);
            db.SaveChanges();
            return Ok("OK");
        }
    }
}
