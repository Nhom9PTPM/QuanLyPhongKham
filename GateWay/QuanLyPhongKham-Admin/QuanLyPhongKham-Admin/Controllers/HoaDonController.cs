using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.BLL;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Models.DTOs;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly HoaDonBLL _bll;

        public HoaDonController(HoaDonBLL bll)
        {
            _bll = bll;
        }

        // ========== 6.1 CRUD ==========
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _bll.LayTatCa();
            return Ok(new { success = true, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hd = await _bll.LayChiTiet(id);
            if (hd == null)
                return NotFound(new { success = false, message = "Không tìm thấy hóa đơn." });
            return Ok(new { success = true, data = hd });
        }

        [HttpPost]
        public async Task<IActionResult> Create(HoaDon hoaDon)
        {
            await _bll.Them(hoaDon);
            return Ok(new { success = true, message = "Tạo hóa đơn thành công!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HoaDon hoaDon)
        {
            hoaDon.MaHoaDon = id;
            await _bll.CapNhat(hoaDon);
            return Ok(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bll.Xoa(id);
            return Ok(new { success = true, message = "Đã xóa hóa đơn!" });
        }

        // ========== 6.2A - Danh sách hóa đơn chưa thanh toán ==========
        [HttpGet("ChuaThanhToan")]
        public async Task<IActionResult> GetChuaThanhToan()
        {
            var data = await _bll.LayHoaDonChuaThanhToan();
            return Ok(new { success = true, data });
        }

        // ========== 6.2B - Thu phí & cập nhật trạng thái thanh toán ==========
        [HttpPut("ThanhToan")]
        public async Task<IActionResult> ThanhToan([FromBody] ThanhToanRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });

            var result = await _bll.ThanhToanHoaDonAsync(request);

            if (!result.Success)
                return BadRequest(new { success = false, message = result.Message });

            return Ok(new { success = true, message = result.Message, data = result.Data });
        }
    }
}
