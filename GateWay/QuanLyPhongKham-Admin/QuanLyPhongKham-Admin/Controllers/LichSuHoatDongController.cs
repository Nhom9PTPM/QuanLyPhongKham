using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuHoatDongController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;

        public LichSuHoatDongController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.LichSuHoatDongs.OrderByDescending(x => x.MaLichSu).ToList());
        }
    }
}
