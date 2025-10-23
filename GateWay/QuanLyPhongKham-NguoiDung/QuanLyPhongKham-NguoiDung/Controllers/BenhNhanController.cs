using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_NguoiDung.Models;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenhNhanController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext _context;
        public BenhNhanController(IConfiguration config)
        {
            _context = new QuanLyPhongKhamContext(config);
        }

        [HttpGet("get-all")]
        public IActionResult GetAll() => Ok(_context.BenhNhans.ToList());

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var bn = _context.BenhNhans.Find(id);
            if (bn == null) return NotFound();
            return Ok(bn);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] BenhNhan model)
        {
            _context.BenhNhans.Add(model);
            _context.SaveChanges();
            return Ok("Đăng ký bệnh nhân thành công");
        }

        [HttpGet("search")]
        public IActionResult Search(string keyword)
        {
            var result = _context.BenhNhans
                .Where(x => x.HoTen.Contains(keyword))
                .ToList();
            return Ok(result);
        }
    }
}
