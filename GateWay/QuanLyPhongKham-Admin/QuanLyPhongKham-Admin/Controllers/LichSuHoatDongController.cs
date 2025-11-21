using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuHoatDongController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db;

        public LichSuHoatDongController(QuanLyPhongKhamContext context)
        {
            db = context;
        }

        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await db.LichSuHoatDongs
                    .AsNoTracking()
                    .OrderByDescending(x => x.MaLichSu)
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Lỗi khi lấy lịch sử hoạt động!",
                    error = ex.Message
                });
            }
        }


        
        [Route("paging")]
        [HttpGet]
        public async Task<IActionResult> GetPaging(int page = 1, int pageSize = 20)
        {
            try
            {
                var query = db.LichSuHoatDongs.AsNoTracking();

                var total = await query.CountAsync();
                var data = await query
                    .OrderByDescending(x => x.MaLichSu)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new { total, data });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Lỗi khi phân trang!",
                    error = ex.Message
                });
            }
        }


        [Route("thong-ke-theo-hanh-dong")]
        [HttpGet]
        public async Task<IActionResult> ThongKeTheoHanhDong()
        {
            try
            {
                var data = await db.LichSuHoatDongs
                    .GroupBy(x => x.HanhDong)
                    .Select(g => new
                    {
                        HanhDong = g.Key,
                        SoLuong = g.Count()
                    })
                    .OrderByDescending(x => x.SoLuong)
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Lỗi khi thống kê hành động!",
                    error = ex.Message
                });
            }
        }

        [Route("thong-ke-theo-hanh-dong-theo-ngay")]
        [HttpGet]
        public async Task<IActionResult> ThongKeTheoHanhDongTheoNgay(
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            try
            {
                var query = db.LichSuHoatDongs.AsQueryable();

                if (fromDate.HasValue)
                    query = query.Where(x => x.DongThoiGian >= fromDate.Value);

                if (toDate.HasValue)
                    query = query.Where(x => x.DongThoiGian <= toDate.Value);

                var data = await query
                    .GroupBy(x => x.HanhDong)
                    .Select(g => new
                    {
                        HanhDong = g.Key,
                        SoLuong = g.Count()
                    })
                    .OrderByDescending(x => x.SoLuong)
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Lỗi khi thống kê theo ngày!",
                    error = ex.Message
                });
            }
        }

        [Route("thong-ke-theo-tai-khoan")]
        [HttpGet]
        public async Task<IActionResult> ThongKeTheoTaiKhoan()
        {
            try
            {
                var data = await db.LichSuHoatDongs
                    .GroupBy(x => x.MaTaiKhoan)
                    .Select(g => new
                    {
                        MaTaiKhoan = g.Key,
                        SoLuong = g.Count()
                    })
                    .OrderByDescending(x => x.SoLuong)
                    .ToListAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Lỗi khi thống kê theo tài khoản!",
                    error = ex.Message
                });
            }
        }
    }
}
