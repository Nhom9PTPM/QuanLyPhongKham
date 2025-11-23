using Microsoft.AspNetCore.Mvc;
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

        [Route("get-bacsi-noibat/{sl}")]
        [HttpGet]
        public IActionResult GetBacSiNoiBat(int sl)
        {
            var query = from k in db.KhamBenhs
                        join b in db.BacSis on k.MaBacSi equals b.MaBacSi
                        join nd in db.NguoiDungs on b.MaNguoiDung equals nd.MaNguoiDung
                        group k by new { b.MaBacSi, nd.HoTen, b.ChuyenKhoa, b.SoPhong } into g
                        select new
                        {
                            g.Key.MaBacSi,
                            g.Key.HoTen,
                            g.Key.ChuyenKhoa,
                            g.Key.SoPhong,
                            TongBenhNhan = g.Count()
                        };
            var result = query.OrderByDescending(x => x.TongBenhNhan).Take(sl).ToList();
            return Ok(result);
        }


        [Route("get-thuoc-banchay/{sl}")]
        [HttpGet]
        public IActionResult GetThuocBanChay(int sl)
        {
            var query = from c in db.ChiTietHoaDons
                        join t in db.Thuocs on c.MaThuoc equals t.MaThuoc
                        group c by new { t.MaThuoc, t.TenThuoc, t.Gia, t.DonViTinh } into g
                        select new
                        {
                            g.Key.MaThuoc,
                            g.Key.TenThuoc,
                            g.Key.Gia,
                            g.Key.DonViTinh,
                            TongBan = g.Sum(x => x.SoLuong)
                        };
            var result = query.OrderByDescending(x => x.TongBan).Take(sl).ToList();
            return Ok(result);
        }

        [Route("get-home/{sl}")]
        [HttpGet]
        public IActionResult GetHome(int sl)
        {
            var topBacSi = GetBacSiNoiBat(sl) as OkObjectResult;
            var topThuoc = GetThuocBanChay(sl) as OkObjectResult;

            return Ok(new
            {
                BacSiNoiBat = topBacSi?.Value,
                ThuocBanChay = topThuoc?.Value
            });
        }

        [HttpGet("tim-kiem")]
        public IActionResult TimKiemBacSi(string keyword, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(keyword))
                return BadRequest("Từ khóa không được để trống");

            var query = from b in db.BacSis
                        join nd in db.NguoiDungs on b.MaNguoiDung equals nd.MaNguoiDung
                        where nd.HoTen.Contains(keyword) || b.ChuyenKhoa.Contains(keyword)
                        select new
                        {
                            b.MaBacSi,
                            HoTen = nd.HoTen,
                            b.ChuyenKhoa,
                            b.SoPhong,
                            b.TrangThai
                        };

            var data = query.ToList();
            var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(new
            {
                TotalItems = data.Count,
                Page = page,
                PageSize = pageSize,
                Data = pagedData
            });
        }
    }
}
