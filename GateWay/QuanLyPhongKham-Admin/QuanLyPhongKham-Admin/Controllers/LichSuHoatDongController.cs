using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Authorize]
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
        public IActionResult GetAll()
        {
            return Ok(db.LichSuHoatDongs.OrderByDescending(x => x.MaLichSu).ToList());
        }
    }
}
