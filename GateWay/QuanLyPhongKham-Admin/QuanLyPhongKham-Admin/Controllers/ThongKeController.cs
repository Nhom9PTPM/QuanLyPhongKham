using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.BLL;

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

        // 🔹 API: Thống kê tổng hợp
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
    }
}
