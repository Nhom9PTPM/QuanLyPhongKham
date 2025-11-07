using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_NguoiDung.Code;
using QuanLyPhongKham_NguoiDung.Models;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuocController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        public ThuocController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("search")]
        [HttpPost]
        public ResponseModel SearchThuoc([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                int page = int.Parse(formData["page"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                string tenThuoc = "";
                if (formData.Keys.Contains("TenThuoc") && !string.IsNullOrEmpty(Convert.ToString(formData["TenThuoc"])))
                    tenThuoc = Convert.ToString(formData["TenThuoc"]);

                int? maNhaCungCap = null;
                if (formData.Keys.Contains("MaNhaCungCap") && !string.IsNullOrEmpty(Convert.ToString(formData["MaNhaCungCap"])))
                    maNhaCungCap = Convert.ToInt32(formData["MaNhaCungCap"]);

                var query = from t in db.Thuocs
                            join ncc in db.NhaCungCaps on t.MaNhaCungCap equals ncc.MaNhaCungCap into tmp
                            from nhaCungCap in tmp.DefaultIfEmpty()
                            where (tenThuoc == "" || t.TenThuoc.Contains(tenThuoc))
                               && (maNhaCungCap == null || t.MaNhaCungCap == maNhaCungCap)
                            orderby t.NgayTao descending
                            select new
                            {
                                t.MaThuoc,
                                t.TenThuoc,
                                t.DonViTinh,
                                t.MoTa,
                                t.Gia,
                                TenNhaCungCap = nhaCungCap != null ? nhaCungCap.TenNhaCungCap : "Không rõ"
                            };

                var data = query.ToList();
                response.TotalItems = data.Count;
                response.Page = page;
                response.PageSize = pageSize;
                response.Data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

        [Route("chi-tiet/{maThuoc}")]
        [HttpGet]
        public IActionResult ChiTietThuoc(int maThuoc)
        {
            try
            {
                var result = (from t in db.Thuocs
                              join ncc in db.NhaCungCaps on t.MaNhaCungCap equals ncc.MaNhaCungCap into tmp
                              from nhaCungCap in tmp.DefaultIfEmpty()
                              where t.MaThuoc == maThuoc
                              select new
                              {
                                  t.MaThuoc,
                                  t.TenThuoc,
                                  t.MoTa,
                                  t.DonViTinh,
                                  t.Gia,
                                  TenNhaCungCap = nhaCungCap != null ? nhaCungCap.TenNhaCungCap : "Không rõ",
                                  TonKho = (from tk in db.TonKhoThuocs
                                            where tk.MaThuoc == t.MaThuoc
                                            select tk.SoLuong).FirstOrDefault()
                              }).FirstOrDefault();

                if (result == null)
                    return NotFound("Không tìm thấy thuốc");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi lấy chi tiết thuốc", error = ex.Message });
            }
        }

        [Route("get-banchay/{sl}")]
        [HttpGet]
        public IActionResult GetThuocBanChay(int sl)
        {
            try
            {
                var query = from c in db.ChiTietHoaDons
                            join t in db.Thuocs on c.MaThuoc equals t.MaThuoc
                            group c by new { t.MaThuoc, t.TenThuoc, t.Gia, t.DonViTinh } into g
                            select new
                            {
                                g.Key.MaThuoc,
                                g.Key.TenThuoc,
                                g.Key.Gia,
                                g.Key.DonViTinh,
                                TongBan = g.Sum(x => x.SoLuong)
                            };
                var result = query.OrderByDescending(x => x.TongBan).Take(sl).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi lấy danh sách thuốc bán chạy", error = ex.Message });
            }
        }

        [Route("loc-gia")]
        [HttpPost]
        public IActionResult LocTheoGia([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                double min = 0, max = double.MaxValue;
                if (formData.Keys.Contains("Min") && !string.IsNullOrEmpty(formData["Min"].ToString()))
                    min = Convert.ToDouble(formData["Min"]);
                if (formData.Keys.Contains("Max") && !string.IsNullOrEmpty(formData["Max"].ToString()))
                    max = Convert.ToDouble(formData["Max"]);

                var result = db.Thuocs
                    .Where(x => x.Gia >= min && x.Gia <= max)
                    .Select(x => new
                    {
                        x.MaThuoc,
                        x.TenThuoc,
                        x.Gia,
                        x.DonViTinh
                    }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi lọc thuốc", error = ex.Message });
            }
        }
    }
}
