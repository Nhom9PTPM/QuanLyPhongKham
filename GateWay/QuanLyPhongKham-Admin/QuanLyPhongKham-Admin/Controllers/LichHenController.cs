using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LichHenController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db;

        public LichHenController(QuanLyPhongKhamContext context)
        {
            db = context;
        }


        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var obj = db.LichHens.Where(x => x.MaLichHen == id)
                    .Select(x => new
                    {
                        x.MaLichHen,
                        x.MaBenhNhan,
                        x.MaBacSi,
                        x.NgayBatDau,
                        x.NgayKetThuc,
                        x.GioHen,
                        x.TrangThai,
                        x.GhiChu,
                        x.NguoiTao
                    }).SingleOrDefault();

                return Ok(obj);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(LichHen model)
        {
            try
            {
                model.NgayTao = DateTime.Now;
                db.LichHens.Add(model);
                db.SaveChanges();
                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("update")]
        [HttpPost]
        public IActionResult Update(LichHen model)
        {
            try
            {
                var obj = db.LichHens.SingleOrDefault(x => x.MaLichHen == model.MaLichHen);
                if (obj != null)
                {
                    obj.GioHen = model.GioHen;
                    obj.NgayBatDau = model.NgayBatDau;
                    obj.NgayKetThuc = model.NgayKetThuc;
                    obj.MaBacSi = model.MaBacSi;
                    obj.TrangThai = model.TrangThai;
                    obj.GhiChu = model.GhiChu;

                    db.SaveChanges();
                    return Ok("OK");
                }

                return Ok("Không tồn tại!");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var obj = db.LichHens.SingleOrDefault(x => x.MaLichHen == id);
                db.LichHens.Remove(obj);
                db.SaveChanges();

                return Ok("OK");
            }
            catch
            {
                return BadRequest();
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

                string HoTen = "";
                if (formData.Keys.Contains("HoTen"))
                    HoTen = Convert.ToString(formData["HoTen"]);

                string TrangThai = "";
                if (formData.Keys.Contains("TrangThai"))
                    TrangThai = Convert.ToString(formData["TrangThai"]);

                DateTime? fr_date = null;
                if (formData.Keys.Contains("fr_Ngay"))
                {
                    if (formData["fr_Ngay"] != null && formData["fr_Ngay"].ToString() != "")
                    {
                        var dt = Convert.ToDateTime(formData["fr_Ngay"]);
                        fr_date = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                    }
                }

                DateTime? to_date = null;
                if (formData.Keys.Contains("to_Ngay"))
                {
                    if (formData["to_Ngay"] != null && formData["to_Ngay"].ToString() != "")
                    {
                        var dt = Convert.ToDateTime(formData["to_Ngay"]);
                        to_date = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                    }
                }

                var query = from l in db.LichHens
                            join b in db.BenhNhans on l.MaBenhNhan equals b.MaBenhNhan
                            select new
                            {
                                l.MaLichHen,
                                b.HoTen,
                                l.NgayBatDau,
                                l.GioHen,
                                l.TrangThai,
                                l.GhiChu
                            };

                var data = query.Where(x =>
                        (HoTen == "" || x.HoTen.Contains(HoTen)) &&
                        (TrangThai == "" || x.TrangThai == TrangThai) &&
                        (
                            (fr_date == null && to_date == null) ||
                            (fr_date != null && x.NgayBatDau >= fr_date && to_date == null) ||
                            (fr_date == null && to_date != null && x.NgayBatDau <= to_date) ||
                            (x.NgayBatDau >= fr_date && x.NgayBatDau <= to_date)
                        )
                ).OrderByDescending(x => x.MaLichHen).ToList();

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
