using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.BLL;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichHenController : ControllerBase
    {
        private readonly LichHenBLL _bll;

        public LichHenController(LichHenBLL bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _bll.LayDanhSach();
            return Ok(new { success = true, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _bll.LayChiTiet(id);
            if (data == null) return NotFound(new { success = false, message = "Không tìm thấy lịch hẹn." });
            return Ok(new { success = true, data });
        }

        // ✅ API liên kết lịch hẹn với hồ sơ
        [HttpPut("LienKet/{maLichHen}/{maHoSo}")]
        public async Task<IActionResult> LienKetLichHenVoiHoSo(int maLichHen, int maHoSo)
        {
            bool ok = await _bll.LienKetVoiHoSo(maLichHen, maHoSo);
            if (!ok) return NotFound(new { success = false, message = "Không tìm thấy lịch hẹn cần cập nhật." });
            return Ok(new { success = true, message = $"Lịch hẹn {maLichHen} đã liên kết với hồ sơ {maHoSo}." });
        }
    }
}
