using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.BLL;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenhNhanController : ControllerBase
    {
        //  Khai báo biến _bll ở đây (đây là thứ bạn bị thiếu)
        private readonly BenhNhanBLL _bll;

        public BenhNhanController(BenhNhanBLL bll)
        {
            _bll = bll;
        }

        //  Lấy danh sách bệnh nhân
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _bll.LayDanhSach();
            return Ok(new { success = true, data });
        }

        //  Lấy chi tiết bệnh nhân theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _bll.LayChiTiet(id);
            if (data == null)
                return NotFound(new { success = false, message = "Không tìm thấy bệnh nhân." });

            return Ok(new { success = true, data });
        }

        //  Thêm bệnh nhân mới
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BenhNhan bn)
        {
            await _bll.Them(bn);
            return Ok(new { success = true, message = "Thêm bệnh nhân thành công!" });
        }

        //  Cập nhật bệnh nhân
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BenhNhan bn)
        {
            bn.MaBenhNhan = id;
            await _bll.CapNhat(bn);
            return Ok(new { success = true, message = "Cập nhật thành công!" });
        }

        //  Xóa mềm bệnh nhân
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bll.Xoa(id);
            return Ok(new { success = true, message = "Xóa mềm thành công!" });
        }

        //  Lấy thông tin đầy đủ (kết hợp hồ sơ, thuốc, tập tin)
        [HttpGet("ThongTinDayDu/{id}")]
        public async Task<IActionResult> ThongTinDayDu(int id)
        {
            var data = await _bll.LayThongTinDayDu(id);
            if (data == null)
                return NotFound(new { success = false, message = "Không tìm thấy bệnh nhân." });

            return Ok(new { success = true, data });
        }
    }
}
