using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_NguoiDung.Models;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LichHenController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext _context;
        public LichHenController(IConfiguration config)
        {
            _context = new QuanLyPhongKhamContext(config);
        }

        [HttpPost("dat-lich")]
        public IActionResult DatLich([FromBody] LichHen lich)
        {
            lich.TrangThai = "Chờ xác nhận";
            _context.LichHens.Add(lich);
            _context.SaveChanges();
            return Ok("Đặt lịch thành công!");
        }

        [HttpGet("get-by-benhnhan/{maBN}")]
        public IActionResult GetByBenhNhan(int maBN)
        {
            var list = _context.LichHens.Where(x => x.MaBenhNhan == maBN).ToList();
            return Ok(list);
        }

        [HttpGet("search")]
        public IActionResult Search(string keyword)
        {
            var list = _context.LichHens
                .Where(x => x.LyDoKham.Contains(keyword))
                .ToList();
            return Ok(list);
        }
    }
}
