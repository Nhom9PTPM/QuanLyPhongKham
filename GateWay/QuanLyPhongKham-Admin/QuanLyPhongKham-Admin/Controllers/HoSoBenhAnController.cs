using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Code;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoSoBenhAnController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        private ITools _tools;
        public HoSoBenhAnController(ITools tools, IConfiguration configuration)
        {
            _tools = tools;
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var hs = db.HoSoBenhAns.Where(x => x.MaHoSo == id).SingleOrDefault();
                var ct = db.TapTins.Where(x => x.MaHoSo == id).ToList();
                return Ok(new { hs, ct });
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-hoso")]
        [HttpPost]
        public IActionResult CreateHoSo(HoSoBenhAnModels model)
        {
            try
            {
                model.hosobenhan.NgayLap = DateTime.Now;
                db.HoSoBenhAns.Add(model.hosobenhan);
                db.SaveChanges();

                if (model.listchitiet != null && model.listchitiet.Count > 0)
                {
                    foreach (var x in model.listchitiet)
                        x.MaHoSo = model.hosobenhan.MaHoSo;

                    model.hosobenhan.TapTins = model.listchitiet;
                    db.SaveChanges();
                }

                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("update-hoso")]
        [HttpPost]
        public IActionResult UpdateHoSo(HoSoBenhAnEditModels model)
        {
            try
            {
                var hs = db.HoSoBenhAns.SingleOrDefault(x => x.MaHoSo == model.hosobenhan.MaHoSo);

                hs.ChanDoanChinh = model.hosobenhan.ChanDoanChinh;
                hs.TomTatBenhLy = model.hosobenhan.TomTatBenhLy;
                hs.LichSuBenhLy = model.hosobenhan.LichSuBenhLy;
                db.SaveChanges();

                if (model.listchitiet != null && model.listchitiet.Count > 0)
                {
                    foreach (var x in model.listchitiet)
                    {
                        if (x.TrangThai == 1)
                        {
                            var t = new TapTin();
                            t.TenTapTin = x.TenTapTin;
                            t.DuongDan = x.DuongDan;
                            t.KichThuoc = x.KichThuoc;
                            t.DinhDang = x.DinhDang;
                            t.MaHoSo = hs.MaHoSo;
                            db.TapTins.Add(t);
                            db.SaveChanges();
                        }
                        else if (x.TrangThai == 0)
                        {
                            var obj = db.TapTins.SingleOrDefault(s => s.MaTapTin == x.MaTapTin);
                            db.TapTins.Remove(obj);
                            db.SaveChanges();
                        }
                    }
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
            try
            {
                var ct = db.TapTins.Where(x => x.MaHoSo == id).ToList();
                db.TapTins.RemoveRange(ct);
                db.SaveChanges();

                var hs = db.HoSoBenhAns.SingleOrDefault(x => x.MaHoSo == id);
                db.HoSoBenhAns.Remove(hs);
                db.SaveChanges();

                return Ok("OK");
            }
            catch
            {
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

                string Ten = "";
                if (formData.Keys.Contains("HoTen"))
                    Ten = Convert.ToString(formData["HoTen"]);

                var query = from h in db.HoSoBenhAns
                            join b in db.BenhNhans on h.MaBenhNhan equals b.MaBenhNhan
                            select new
                            {
                                h.MaHoSo,
                                h.ChanDoanChinh,
                                h.NgayLap,
                                BenhNhan = b.HoTen
                            };

                var data = query.Where(x =>
                    (Ten == "" || x.BenhNhan.Contains(Ten))
                ).OrderByDescending(x => x.MaHoSo).ToList();

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
