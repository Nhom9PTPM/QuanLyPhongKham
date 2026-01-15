using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.BLL;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenhNhanController : ControllerBase
    {
        private readonly BenhNhanBLL _bll;

        public BenhNhanController(BenhNhanBLL bll)
        {
            _bll = bll;
        }

        // ================================
        //  LẤY DANH SÁCH BỆNH NHÂN
        // ================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _bll.LayDanhSach();
            return Ok(new { success = true, data });
        }

        // ================================
        //  LẤY CHI TIẾT BỆNH NHÂN
        // ================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _bll.LayChiTiet(id);
            if (data == null)
                return NotFound(new { success = false, message = "Không tìm thấy bệnh nhân." });

            return Ok(new { success = true, data });
        }

        // ================================
        //  THÊM BỆNH NHÂN
        // ================================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BenhNhan bn)
        {
            await _bll.Them(bn);
            return Ok(new { success = true, message = "Thêm bệnh nhân thành công!" });
        }

        // ================================
        //  CẬP NHẬT BỆNH NHÂN
        // ================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BenhNhan bn)
        {
            bn.MaBenhNhan = id;
            await _bll.CapNhat(bn);
            return Ok(new { success = true, message = "Cập nhật thành công!" });
        }

        // ================================
        //  XÓA MỀM BỆNH NHÂN
        // ================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bll.Xoa(id);
            return Ok(new { success = true, message = "Xóa mềm thành công!" });
        }

        // ================================
        //  LẤY THÔNG TIN ĐẦY ĐỦ
        // ================================
        [HttpGet("ThongTinDayDu/{id}")]
        public async Task<IActionResult> ThongTinDayDu(int id)
        {
            var data = await _bll.LayThongTinDayDu(id);
            if (data == null)
                return NotFound(new { success = false, message = "Không tìm thấy bệnh nhân." });

            return Ok(new { success = true, data });
        }

        // ================================
        //   API TÌM KIẾM BỆNH NHÂN
        // ================================
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var data = await _bll.TimKiem(keyword ?? "");
            return Ok(new { success = true, data });
        }
    }
}
