using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;
using System.Linq;

namespace QuanLyPhongKham_Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BacSiController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db;
        private readonly ITools _tools;

        public BacSiController(ITools tools, IConfiguration config)
        {
            _tools = tools;
            db = new QuanLyPhongKhamContext(config);
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var bs = db.BacSis
                    .Where(x => x.MaBacSi == id)
                    .Select(x => new
                    {
                        x.MaBacSi,
                        x.MaNguoiDung,
                        HoTen = x.MaNguoiDungNavigation.HoTen,
                        x.ChuyenKhoa,
                        x.BangCap,
                        x.KinhNghiem,
                        x.SoPhong,
                        x.NgayTao,
                        x.TrangThai
                    })
                    .SingleOrDefault();

                if (bs == null)
                    return NotFound(new { message = "Không tìm thấy bác sĩ." });

                return Ok(bs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi lấy dữ liệu bác sĩ.", error = ex.Message });
            }
        }

        [HttpPost("create-bacsi")]
        public IActionResult CreateBacSi([FromBody] BacSi model)
        {
            try
            {
                if (model == null)
                    return BadRequest(new { message = "Dữ liệu bác sĩ không hợp lệ." });

                model.NgayTao = DateTime.Now;
                model.TrangThai = true;

                db.BacSis.Add(model);
                db.SaveChanges();

                return Ok(new { message = "Thêm bác sĩ thành công.", maBacSi = model.MaBacSi });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi tạo bác sĩ.", error = ex.Message });
            }
        }

        [HttpPost("update-bacsi")]
        public IActionResult UpdateBacSi([FromBody] BacSi model)
        {
            try
            {
                if (model == null || model.MaBacSi <= 0)
                    return BadRequest(new { message = "Dữ liệu cập nhật không hợp lệ." });

                var bs = db.BacSis.SingleOrDefault(x => x.MaBacSi == model.MaBacSi);
                if (bs == null)
                    return NotFound(new { message = "Không tìm thấy bác sĩ cần cập nhật." });

                bs.ChuyenKhoa = string.IsNullOrEmpty(model.ChuyenKhoa) ? bs.ChuyenKhoa : model.ChuyenKhoa;
                bs.BangCap = model.BangCap ?? bs.BangCap;
                bs.KinhNghiem = model.KinhNghiem ?? bs.KinhNghiem;
                bs.SoPhong = model.SoPhong ?? bs.SoPhong;
                bs.TrangThai = model.TrangThai;

                db.SaveChanges();
                return Ok(new { message = "Cập nhật thông tin bác sĩ thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi cập nhật bác sĩ.", error = ex.Message });
            }
        }

        [HttpGet("delete-bacsi/{id}")]
        public IActionResult DeleteBacSi(int id)
        {
            try
            {
                var bs = db.BacSis.SingleOrDefault(x => x.MaBacSi == id);
                if (bs == null)
                    return NotFound(new { message = "Không tìm thấy bác sĩ cần xoá." });

                db.BacSis.Remove(bs);
                db.SaveChanges();

                return Ok(new { message = "Xoá bác sĩ thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi xoá bác sĩ.", error = ex.Message });
            }
        }

        [HttpPost("search")]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var res = new ResponseModel();
            try
            {
                int page = int.Parse(formData["page"].ToString());
                int size = int.Parse(formData["pageSize"].ToString());

                string CK = "";
                if (formData.ContainsKey("ChuyenKhoa"))
                    CK = formData["ChuyenKhoa"].ToString();

                var q = db.BacSis
                    .Where(x => CK == "" || x.ChuyenKhoa.Contains(CK))
                    .Select(x => new
                    {
                        x.MaBacSi,
                        HoTen = x.MaNguoiDungNavigation.HoTen,
                        x.ChuyenKhoa,
                        x.SoPhong,
                        x.TrangThai
                    })
                    .ToList();

                res.TotalItems = q.Count;
                res.Page = page;
                res.PageSize = size;
                res.Data = q.Skip((page - 1) * size).Take(size).ToList();

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm bác sĩ: " + ex.Message);
            }
        }
    }
}
