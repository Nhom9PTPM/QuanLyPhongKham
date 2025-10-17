using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_NguoiDung.Models;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        public HomeController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        // 🩺 Bác sĩ có nhiều lịch hẹn nhất (Bác sĩ nổi bật)
        [Route("get-bacsi-noibat/{sl}")]
        [HttpGet]
        public IActionResult BacSiNoiBat(int sl)
        {
            var query = from lh in db.LichHens
                        join bs in db.BacSis on lh.MaBacSi equals bs.MaBacSi
                        group lh by new
                        {
                            bs.MaBacSi,
                            bs.HoTen,
                            bs.ChuyenKhoa,
                            AnhDaiDien = string.IsNullOrEmpty(bs.AnhDaiDien) ? "" : bs.AnhDaiDien
                        } into g
                        select new
                        {
                            MaBacSi = g.Key.MaBacSi,
                            HoTen = g.Key.HoTen,
                            ChuyenKhoa = g.Key.ChuyenKhoa,
                            AnhDaiDien = g.Key.AnhDaiDien,
                            TongSoLich = g.Count()
                        };

            var result = query.OrderByDescending(x => x.TongSoLich).Take(sl).ToList();
            return Ok(new { result });
        }

        [Route("get-benhnhan-tichcuc/{sl}")]
        [HttpGet]
        public IActionResult BenhNhanTichCuc(int sl)
        {
            var query = from lh in db.LichHens
                        join bn in db.BenhNhans on lh.MaBenhNhan equals bn.MaBenhNhan
                        group lh by new
                        {
                            bn.MaBenhNhan,
                            bn.HoTen,
                            bn.GioiTinh,
                            bn.SoDienThoai
                        } into g
                        select new
                        {
                            MaBenhNhan = g.Key.MaBenhNhan,
                            HoTen = g.Key.HoTen,
                            GioiTinh = g.Key.GioiTinh,
                            SoDienThoai = g.Key.SoDienThoai,
                            TongSoLich = g.Count()
                        };

            var result = query.OrderByDescending(x => x.TongSoLich).Take(sl).ToList();
            return Ok(result);
        }

        [Route("get-lichhen-moi/{sl}")]
        [HttpGet]
        public IActionResult LichHenMoi(int sl)
        {
            var result = db.LichHens
                .OrderByDescending(x => x.NgayTao)
                .Take(sl)
                .Select(x => new
                {
                    x.MaLichHen,
                    x.NgayHen,
                    x.GioHen,
                    x.TrangThai,
                    BenhNhan = x.BenhNhan != null ? x.BenhNhan.HoTen : "Không xác định"
                })
                .ToList();
            return Ok(result);
        }

        [Route("get-home/{sl}")]
        [HttpGet]
        public IActionResult TrangChuTongHop(int sl)
        {
            var query1 = from lh in db.LichHens
                         join bs in db.BacSis on lh.MaBacSi equals bs.MaBacSi
                         group lh by new
                         {
                             bs.MaBacSi,
                             bs.HoTen,
                             bs.ChuyenKhoa,
                             AnhDaiDien = string.IsNullOrEmpty(bs.AnhDaiDien) ? "" : bs.AnhDaiDien
                         } into g
                         select new
                         {
                             MaBacSi = g.Key.MaBacSi,
                             HoTen = g.Key.HoTen,
                             ChuyenKhoa = g.Key.ChuyenKhoa,
                             AnhDaiDien = g.Key.AnhDaiDien,
                             TongSoLich = g.Count()
                         };
            var result1 = query1.OrderByDescending(x => x.TongSoLich).Take(sl).ToList();

            var query2 = from lh in db.LichHens
                         join bn in db.BenhNhans on lh.MaBenhNhan equals bn.MaBenhNhan
                         group lh by new
                         {
                             bn.MaBenhNhan,
                             bn.HoTen,
                             bn.GioiTinh,
                             bn.SoDienThoai
                         } into g
                         select new
                         {
                             MaBenhNhan = g.Key.MaBenhNhan,
                             HoTen = g.Key.HoTen,
                             GioiTinh = g.Key.GioiTinh,
                             SoDienThoai = g.Key.SoDienThoai,
                             TongSoLich = g.Count()
                         };
            var result2 = query2.OrderByDescending(x => x.TongSoLich).Take(sl).ToList();

            var result3 = db.LichHens
                .OrderByDescending(x => x.NgayTao)
                .Take(sl)
                .Select(x => new
                {
                    x.MaLichHen,
                    x.NgayHen,
                    x.GioHen,
                    x.TrangThai,
                    BenhNhan = x.BenhNhan != null ? x.BenhNhan.HoTen : "Không xác định"
                })
                .ToList();

            return Ok(new
            {
                listBacSiNoiBat = result1,
                listBenhNhanTichCuc = result2,
                listLichHenMoi = result3
            });
        }
    }
}
