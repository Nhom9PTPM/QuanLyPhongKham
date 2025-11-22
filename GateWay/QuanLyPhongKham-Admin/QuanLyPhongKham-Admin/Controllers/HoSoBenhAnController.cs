using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Code;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HoSoBenhAnController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db;
        private readonly ITools _tools;

        public HoSoBenhAnController(ITools tools, QuanLyPhongKhamContext context)
        {
            _tools = tools;
            db = context;
        }

        [Route("get-all")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = db.BenhNhans
                              .Select(b => new { b.MaBenhNhan, b.HoTen })
                              .ToList();
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var hs = db.HoSoBenhAns
                           .Where(x => x.MaHoSo == id)
                           .Select(h => new
                           {
                               h.MaHoSo,
                               h.MaBenhNhan,
                               h.ChanDoanChinh,
                               h.TomTatBenhLy,
                               h.LichSuBenhLy,
                               BenhNhan = db.BenhNhans
                                            .Where(b => b.MaBenhNhan == h.MaBenhNhan)
                                            .Select(b => b.HoTen)
                                            .FirstOrDefault()
                           })
                           .SingleOrDefault();

                var ct = db.TapTins.Where(x => x.MaHoSo == id).ToList();
                return Ok(new { hs, ct });
            }
            catch
            {
                return BadRequest();
            }
        }

        // --- Tạo hồ sơ bệnh án sử dụng PostModel ---
        [Route("create-hoso")]
        [HttpPost]
        public IActionResult CreateHoSo([FromBody] HoSoBenhAnPostModel model)
        {
            if (model == null)
                return BadRequest("Dữ liệu không hợp lệ");

            using var transaction = db.Database.BeginTransaction();
            try
            {
                // 1. Lưu hồ sơ bệnh án
                var hs = new HoSoBenhAn
                {
                    MaBenhNhan = model.MaBenhNhan,
                    TomTatBenhLy = model.TomTatBenhLy,
                    ChanDoanChinh = model.ChanDoanChinh,
                    LichSuBenhLy = model.LichSuBenhLy,
                    NguoiLap = model.NguoiLap,
                    NgayLap = DateTime.Now,
                    DaXoa = false
                };

                db.HoSoBenhAns.Add(hs);
                db.SaveChanges();

                // 2. Lưu tập tin chi tiết nếu có
                if (model.TapTins != null && model.TapTins.Count > 0)
                {
                    foreach (var t in model.TapTins)
                    {
                        var tapTin = new TapTin
                        {
                            TenTapTin = t.TenTapTin,
                            DuongDan = t.DuongDan,
                            KichThuoc = t.KichThuoc,
                            DinhDang = t.DinhDang,
                            MaHoSo = hs.MaHoSo,
                            NgayTao = DateTime.Now
                        };
                        db.TapTins.Add(tapTin);
                    }
                    db.SaveChanges();
                }

                transaction.Commit();
                return Ok(new { Message = "Tạo hồ sơ thành công", MaHoSo = hs.MaHoSo });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(new { Message = "Lỗi khi tạo hồ sơ", Error = ex.Message });
            }
        }


        [Route("update-hoso")]
        [HttpPost]
        public IActionResult UpdateHoSo(HoSoBenhAnEditModels model) 
        {
            try
            {
                // Lấy hồ sơ theo MaHoSo
                var hs = db.HoSoBenhAns.SingleOrDefault(x => x.MaHoSo == model.hosobenhan.MaHoSo);
                if (hs == null) return NotFound();

                // Cập nhật thông tin hồ sơ
                hs.ChanDoanChinh = model.hosobenhan.ChanDoanChinh;
                hs.TomTatBenhLy = model.hosobenhan.TomTatBenhLy;
                hs.LichSuBenhLy = model.hosobenhan.LichSuBenhLy;
                hs.NguoiLap = model.hosobenhan.NguoiLap;
                db.SaveChanges();

                // Cập nhật tập tin
                if (model.listchitiet != null && model.listchitiet.Count > 0)
                {
                    foreach (var x in model.listchitiet)
                    {
                        if (x.TrangThai == 1) // Thêm mới
                        {
                            var t = new TapTin();
                            t.TenTapTin = x.TenTapTin;
                            t.DuongDan = x.DuongDan;
                            t.KichThuoc = x.KichThuoc;
                            t.DinhDang = x.DinhDang;
                            t.MaHoSo = hs.MaHoSo;
                            db.TapTins.Add(t);
                        }
                        else if (x.TrangThai == 0) // Xóa
                        {
                            var obj = db.TapTins.SingleOrDefault(s => s.MaTapTin == x.MaTapTin);
                            if (obj != null) db.TapTins.Remove(obj);
                        }
                    }
                    db.SaveChanges();
                }

                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }




        [Route("delete-hoso/{id}")]
        [HttpGet]
        public IActionResult DeleteHoSo(int id)
        {
            using var transaction = db.Database.BeginTransaction();
            try
            {
                var ct = db.TapTins.Where(x => x.MaHoSo == id).ToList();
                db.TapTins.RemoveRange(ct);

                var hs = db.HoSoBenhAns.SingleOrDefault(x => x.MaHoSo == id);
                if (hs != null) db.HoSoBenhAns.Remove(hs);

                db.SaveChanges();
                transaction.Commit();
                return Ok("Xóa thành công");
            }
            catch
            {
                transaction.Rollback();
                return BadRequest();
            }
        }

        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"upload/{file.FileName.Replace("-", "_").Replace("%", "")}";
                    var fullPath = _tools.CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(new { filePath });
                }
                return BadRequest();
            }
            catch
            {
                return StatusCode(500, "Upload lỗi");
            }
        }

        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());

                string Ten = formData.ContainsKey("HoTen") ? Convert.ToString(formData["HoTen"]) : "";

                var query = from h in db.HoSoBenhAns
                            join b in db.BenhNhans on h.MaBenhNhan equals b.MaBenhNhan
                            select new
                            {
                                h.MaHoSo,
                                h.ChanDoanChinh,
                                h.NgayLap,
                                BenhNhan = b.HoTen
                            };

                var data = query
                    .Where(x => string.IsNullOrEmpty(Ten) || x.BenhNhan.Contains(Ten))
                    .OrderByDescending(x => x.MaHoSo)
                    .ToList();

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
    }
}
