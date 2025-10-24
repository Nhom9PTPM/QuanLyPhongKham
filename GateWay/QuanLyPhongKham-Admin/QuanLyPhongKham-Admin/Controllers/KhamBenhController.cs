using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhamBenhController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        public KhamBenhController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var obj = db.KhamBenhs.Where(x => x.MaKham == id).Select(
                    x => new
                    {
                        x.MaKham,
                        x.MaHoSo,
                        x.MaBenhNhan,
                        x.MaBacSi,
                        x.NgayKham,
                        x.ChanDoan,
                        x.ChiDinhXN,
                        x.ChiDinhCLS,
                        x.GhiChu,
                        x.NguoiThucHien
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
        public IActionResult Create(KhamBenh model)
        {
            try
            {
                model.NgayKham = DateTime.Now;
                model.NgayTao = DateTime.Now;
                db.KhamBenhs.Add(model);
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
        public IActionResult Update(KhamBenh model)
        {
            try
            {
                var obj = db.KhamBenhs.SingleOrDefault(x => x.MaKham == model.MaKham);
                if (obj != null)
                {
                    obj.ChanDoan = model.ChanDoan;
                    obj.ChiDinhXN = model.ChiDinhXN;
                    obj.ChiDinhCLS = model.ChiDinhCLS;
                    obj.GhiChu = model.GhiChu;
                    obj.MaBacSi = model.MaBacSi;
                    obj.NguoiThucHien = model.NguoiThucHien;

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
                var obj = db.KhamBenhs.SingleOrDefault(x => x.MaKham == id);
                db.KhamBenhs.Remove(obj);
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

                string ten = "";
                if (formData.Keys.Contains("HoTen"))
                    ten = Convert.ToString(formData["HoTen"]);

                DateTime? fr_date = null;
                if (formData.Keys.Contains("fr_NgayKham") && formData["fr_NgayKham"] != null)
                {
                    var dt = Convert.ToDateTime(formData["fr_NgayKham"].ToString());
                    fr_date = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                }

                DateTime? to_date = null;
                if (formData.Keys.Contains("to_NgayKham") && formData["to_NgayKham"] != null)
                {
                    var dt = Convert.ToDateTime(formData["to_NgayKham"].ToString());
                    to_date = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                }

                var query = from k in db.KhamBenhs
                            join b in db.BenhNhans on k.MaBenhNhan equals b.MaBenhNhan
                            select new
                            {
                                k.MaKham,
                                b.HoTen,
                                k.NgayKham,
                                k.ChanDoan,
                                k.NguoiThucHien
                            };

                var data = query.Where(x =>
                        (ten == "" || x.HoTen.Contains(ten)) &&
                        (
                            (fr_date == null && to_date == null) ||
                            (fr_date != null && x.NgayKham >= fr_date && to_date == null) ||
                            (fr_date == null && to_date != null && x.NgayKham <= to_date) ||
                            (x.NgayKham >= fr_date && x.NgayKham <= to_date)
                        )
                ).OrderByDescending(x => x.MaKham).ToList();

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
