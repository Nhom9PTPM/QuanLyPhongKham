using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.BLL;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenhNhanController : ControllerBase
    {
        private readonly BenhNhanBLL _benhNhanBLL;

        public BenhNhanController(BenhNhanBLL benhNhanBLL)
        {
            _benhNhanBLL = benhNhanBLL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _benhNhanBLL.LayDanhSach();
            return Ok(new { success = true, data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _benhNhanBLL.LayChiTiet(id);
            if (data == null) return NotFound();
            return Ok(new { success = true, data });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BenhNhan bn)
        {
            await _benhNhanBLL.Them(bn);
            return Ok(new { success = true, message = "Thêm bệnh nhân thành công!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BenhNhan bn)
        {
            bn.MaBenhNhan = id;
            await _benhNhanBLL.CapNhat(bn);
            return Ok(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _benhNhanBLL.Xoa(id);
            return Ok(new { success = true, message = "Xóa mềm thành công!" });
        }
    }
}
