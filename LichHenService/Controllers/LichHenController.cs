using Microsoft.AspNetCore.Mvc;
using LichHenService.Models;
using LichHenService.BLL;


namespace LichHenService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LichHenController : ControllerBase
    {
        private readonly LichHenBLL _bll;

        public LichHenController(IConfiguration config)
        {
            _bll = new LichHenBLL(config);
        }

        [HttpGet("details")]
        public IActionResult GetAllWithDetails() => Ok(_bll.GetAllWithDetails());

        [HttpPost]
        public IActionResult Create([FromBody] LichHen lh)
        {
            _bll.Insert(lh);
            return Ok(new { message = "Đặt lịch thành công" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] LichHen lh)
        {
            _bll.Update(lh);
            return Ok(new { message = "Cập nhật thành công" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bll.Delete(id);
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
