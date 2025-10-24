using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaoController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;

        public ThongBaoController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(ThongBao model)
        {
            db.ThongBaos.Add(model);
            db.SaveChanges();
            return Ok("OK");
        }

        [Route("read/{id}")]
        [HttpGet]
        public IActionResult Read(int id)
        {
            var tb = db.ThongBaos.SingleOrDefault(x => x.MaThongBao == id);
            tb.DaDoc = true;
            db.SaveChanges();
            return Ok("OK");
        }
    }
}
