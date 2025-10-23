using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_NguoiDung.Models;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext _db;
        public HomeController(IConfiguration config)
        {
            _db = new QuanLyPhongKhamContext(config);
        }

        

        [HttpGet("get-lichhen-moi/{sl}")]
        public IActionResult GetLichHenMoi(int sl)
        {
            var result = _db.LichHens.OrderByDescending(x => x.NgayHen).Take(sl).ToList();
            return Ok(result);
        }

       
    }
}
