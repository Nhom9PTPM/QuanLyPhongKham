using Microsoft.AspNetCore.Mvc;
using LichHenService.DAL;
using LichHenService.Models;
using LichHenService.BLL;

namespace LichHenService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhacLichController : ControllerBase
    {
        private readonly NhacLichBLL _bll;

        public NhacLichController(IConfiguration config)
        {
            _bll = new NhacLichBLL(config);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int? maBenhNhan)
        {
            return Ok(_bll.GetAllWithDetail(from, to, maBenhNhan));
        }

        [HttpPost("send")]
        public IActionResult Send([FromBody] NhacLich nhac)
        {
            _bll.Create(nhac);
            return Ok(new
            {
                message = $"[MÔ PHỎNG] Đã gửi {nhac.hinhThuc} nhắc lịch cho lịch hẹn {nhac.maLichHen}",
                time = DateTime.Now
            });
        }
    }
}
