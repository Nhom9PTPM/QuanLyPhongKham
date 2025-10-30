using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.BLL;
using QuanLyPhongKham_Admin.Models.DTOs;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly ThongKeBLL _bll;

        public ThongKeController(ThongKeBLL bll)
        {
            _bll = bll;
        }

        // ===========================================================
        // 6.3A - API: Thống kê tổng hợp
        // ===========================================================
        [HttpGet("TongHop")]
        public async Task<IActionResult> GetTongHop()
        {
            var result = await _bll.LayThongKeTongHopAsync();
            return Ok(new
            {
                success = true,
                message = "Thống kê tổng hợp hệ thống",
                data = result
            });
        }

        // ===========================================================
        // 6.3B - API: Báo cáo doanh thu theo bác sĩ hoặc chuyên khoa
        // ===========================================================
        /// <summary>
        /// 6.3 - Báo cáo doanh thu theo bác sĩ / chuyên khoa
        /// </summary>
        /// <param name="request">
        /// TuNgay, DenNgay, LoaiBaoCao ("BacSi" hoặc "ChuyenKhoa")
        /// </param>
        [HttpPost("BaoCaoDoanhThu")]
        public async Task<IActionResult> BaoCaoDoanhThu([FromBody] BaoCaoDoanhThuRequest request)
        {
            if (request == null)
                return BadRequest(new { success = false, message = "Dữ liệu đầu vào không hợp lệ." });

            var data = await _bll.BaoCaoDoanhThuAsync(request);

            if (data == null || data.Count == 0)
            {
                return Ok(new
                {
                    success = true,
                    message = "Không có dữ liệu doanh thu trong khoảng thời gian này.",
                    data = new List<object>()
                });
            }

            return Ok(new
            {
                success = true,
                message = $"Báo cáo doanh thu theo {(request.LoaiBaoCao ?? "BacSi")}",
                data
            });
        }
    }
}
