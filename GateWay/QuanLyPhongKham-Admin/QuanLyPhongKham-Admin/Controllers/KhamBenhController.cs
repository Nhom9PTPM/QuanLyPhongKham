using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.BLL;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhamBenhController : ControllerBase
    {
        private readonly KhamBenhBLL _bll;

        public KhamBenhController(KhamBenhBLL bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _bll.LayDanhSach();
            return Ok(new { success = true, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _bll.LayChiTiet(id);
            if (data == null) return NotFound();
            return Ok(new { success = true, data });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KhamBenh kb)
        {
            await _bll.Them(kb);
            return Ok(new { success = true, message = "Thêm lần khám thành công!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] KhamBenh kb)
        {
            kb.MaKham = id;
            await _bll.CapNhat(kb);
            return Ok(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bll.Xoa(id);
            return Ok(new { success = true, message = "Xóa mềm thành công!" });
        }
    }
}
