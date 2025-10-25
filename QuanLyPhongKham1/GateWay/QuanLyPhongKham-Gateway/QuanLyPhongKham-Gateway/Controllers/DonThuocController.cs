using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Gateway.Data;
using QuanLyPhongKham_Gateway.DTO;
using System.Data;
using System.Text.Json;

namespace PhongKhamAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonThuocController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DonThuocController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("them")]
        public async Task<IActionResult> ThemDonThuoc([FromBody] DonThuocCreateDto model)
        {
            if (model == null || model.ChiTiet == null || model.ChiTiet.Count == 0)
                return BadRequest("Danh sách chi tiết thuốc không được để trống.");

            string jsonChiTiet = JsonSerializer.Serialize(model.ChiTiet);

            try
            {
                using SqlCommand cmd = new SqlCommand("sp_ThemDonThuoc");
                cmd.Parameters.AddWithValue("@MaKham", model.MaKham);
                cmd.Parameters.AddWithValue("@MaBacSi", model.MaBacSi);
                cmd.Parameters.AddWithValue("@GhiChu", (object?)model.GhiChu ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NguoiTao", model.NguoiTao);
                cmd.Parameters.AddWithValue("@ChiTietJSON", jsonChiTiet);

                using var reader = await _context.ExecuteReaderAsync(cmd);

                string? thongBao = null;
                int maDonThuocMoi = 0;

                if (await reader.ReadAsync())
                {
                    thongBao = reader["ThongBao"]?.ToString();
                    maDonThuocMoi = Convert.ToInt32(reader["MaDonThuocMoi"]);
                }

                return Ok(new
                {
                    Message = thongBao ?? "Thêm đơn thuốc thành công",
                    MaDonThuoc = maDonThuocMoi
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Lỗi khi thêm đơn thuốc", Detail = ex.Message });
            }
        }
        // API Sửa Đơn Thuốc
        [HttpPut("sua")]
        public async Task<IActionResult> SuaDonThuoc([FromBody] DonThuocUpdateDto dto)
        {
            try
            {
                string jsonChiTiet1 = JsonSerializer.Serialize(dto.ChiTietJSON);
                using (var cmd = new SqlCommand("sp_SuaDonThuoc"))
                {
                    cmd.Parameters.AddWithValue("@MaDonThuoc", dto.MaDonThuoc);
                    
                    cmd.Parameters.AddWithValue("@NguoiSua", dto.NguoiSua);
                    cmd.Parameters.AddWithValue("@GhiChu", dto.GhiChu ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ChiTietJSON", jsonChiTiet1);


                    await _context.ExecuteNonQueryAsync(cmd);
                }

                return Ok(new { message = "Sửa đơn thuốc thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi sửa đơn thuốc.", error = ex.Message });
            }
        }

        // ✅ API Xóa Đơn Thuốc
        [HttpDelete("xoa")]
        public async Task<IActionResult> XoaDonThuoc([FromBody] DonThuocDeleteDto dto)
        {
            try
            {
                using (var cmd = new SqlCommand("sp_XoaDonThuoc"))
                {
                    cmd.Parameters.AddWithValue("@MaDonThuoc", dto.MaDonThuoc);
                    cmd.Parameters.AddWithValue("@NguoiXoa", dto.NguoiXoa);

                    await _context.ExecuteNonQueryAsync(cmd);
                }

                return Ok(new { message = "Xóa đơn thuốc thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa đơn thuốc.", error = ex.Message });
            }
        }
    }

}

