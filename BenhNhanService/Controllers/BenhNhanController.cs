using Microsoft.AspNetCore.Mvc;
using BenhNhanService.Models;
using BenhNhanService.BLL;


namespace BenhNhanService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenhNhanController : ControllerBase
    {
        private readonly BenhNhanBLL _bll;

        public BenhNhanController(IConfiguration config)
        {
            _bll = new BenhNhanBLL(config);
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_bll.GetAll());

        [HttpPost]
        public IActionResult Create([FromBody] BenhNhan bn)
        {
            _bll.Insert(bn);
            return Ok(new { message = "Thêm bệnh nhân thành công" });
        }

        [HttpPut]
        public IActionResult Update([FromBody] BenhNhan bn)
        {
            _bll.Update(bn);
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
