using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_NguoiDung.Models;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenhNhanController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;

        public BenhNhanController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var bn = db.BenhNhans
                    .Where(x => x.MaBenhNhan == id)
                    .Select(x => new
                    {
                        x.MaBenhNhan,
                        x.HoTen,
                        x.NgaySinh,
                        x.GioiTinh,
                        x.SoDienThoai,
                        x.Email,
                        x.DiaChi,
                        x.CMTND_NV
                    })
                    .SingleOrDefault();
                return Ok(bn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("update-benhnhan")]
        [HttpPost]
        public IActionResult UpdateBenhNhan(BenhNhan model)
        {
            try
            {
                var bn = db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == model.MaBenhNhan);
                if (bn != null)
                {
                    bn.HoTen = model.HoTen ?? bn.HoTen;
                    bn.NgaySinh = model.NgaySinh ?? bn.NgaySinh;
                    bn.GioiTinh = model.GioiTinh ?? bn.GioiTinh;
                    bn.SoDienThoai = model.SoDienThoai ?? bn.SoDienThoai;
                    bn.Email = model.Email ?? bn.Email;
                    bn.DiaChi = model.DiaChi ?? bn.DiaChi;
                    db.SaveChanges();
                    return Ok("Cập nhật thông tin thành công");
                }
                return Ok("Không tìm thấy bệnh nhân");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
