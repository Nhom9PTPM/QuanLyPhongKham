using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using System;
using System.Linq;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;

        public DashboardController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("get-tong-quan")]
        [HttpGet]
        public IActionResult GetTongQuan()
        {
            try
            {
                var result = new
                {
                    TongBacSi = db.BacSis.Count(),
                    TongBenhNhan = db.BenhNhans.Count(),
                    TongHoaDon = db.HoaDons.Count(),
                    TongDoanhThu = db.HoaDons.Sum(h => (decimal?)h.TongTien) ?? 0
                };
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("get-doanhthu-thang/{nam}")]
        [HttpGet]
        public IActionResult GetDoanhThuTheoThang(int nam)
        {
            try
            {
                var data = db.HoaDons
                    .Where(h => h.NgayLap.Year == nam)
                    .GroupBy(h => h.NgayLap.Month)
                    .Select(g => new
                    {
                        Thang = g.Key,
                        TongDoanhThu = g.Sum(h => h.TongTien)
                    })
                    .OrderBy(x => x.Thang)
                    .ToList();

                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("get-luotkham-thang/{nam}")]
        [HttpGet]
        public IActionResult GetLuotKhamTheoThang(int nam)
        {
            try
            {
                var data = db.LichHens
                    .Where(l => l.NgayBatDau.Year == nam)
                    .GroupBy(l => l.NgayBatDau.Month)
                    .Select(g => new
                    {
                        Thang = g.Key,
                        SoLuotKham = g.Count()
                    })
                    .OrderBy(x => x.Thang)
                    .ToList();

                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("get-doanhthu-theo-khoang")]
        [HttpGet]
        public IActionResult GetDoanhThuTheoKhoang(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                var data = db.HoaDons
                    .Where(h => h.NgayLap >= tuNgay && h.NgayLap <= denNgay)
                    .GroupBy(h => h.NgayLap.Date)
                    .Select(g => new
                    {
                        Ngay = g.Key,
                        TongTien = g.Sum(h => h.TongTien)
                    })
                    .OrderBy(x => x.Ngay)
                    .ToList();

                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
