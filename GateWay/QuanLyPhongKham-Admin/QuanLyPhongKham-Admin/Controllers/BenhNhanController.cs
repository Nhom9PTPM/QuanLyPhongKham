using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Code;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BenhNhanController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext _db;
        private readonly ITools _tools;

        public BenhNhanController(ITools tools, IConfiguration configuration)
        {
            _tools = tools;
            _db = new QuanLyPhongKhamContext(configuration);
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var bn = _db.BenhNhans
                        .Where(x => x.MaBenhNhan == id)
                        .Select(x => new
                        {
                            x.MaBenhNhan,
                            x.HoTen,
                            x.NgaySinh,
                            x.GioiTinh,
                            x.SoDienThoai,
                            x.Email,
                            x.DiaChi,
                            x.CMTND_NV,
                            x.NgayTao,
                            x.DaXoa
                        })
                        .SingleOrDefault();
            if (bn == null) return NotFound();
            return Ok(bn);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] BenhNhanModels model)
        {
            if (model == null || model.benhnhan == null) return BadRequest();

            model.benhnhan.NgayTao = DateTime.Now;
            model.benhnhan.DaXoa = false;
            _db.BenhNhans.Add(model.benhnhan);
            _db.SaveChanges();
            return Ok(new { message = "OK", model.benhnhan.MaBenhNhan });
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] BenhNhanEditModels model)
        {
            if (model == null) return BadRequest();
            var bn = _db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == model.MaBenhNhan);
            if (bn == null) return NotFound();

            bn.HoTen = model.HoTen ?? bn.HoTen;
            bn.NgaySinh = model.NgaySinh ?? bn.NgaySinh;
            bn.GioiTinh = model.GioiTinh ?? bn.GioiTinh;
            bn.SoDienThoai = model.SoDienThoai ?? bn.SoDienThoai;
            bn.Email = model.Email ?? bn.Email;
            bn.DiaChi = model.DiaChi ?? bn.DiaChi;
            bn.CMTND_NV = model.CMTND_NV ?? bn.CMTND_NV;
            if (model.DaXoa != null) bn.DaXoa = model.DaXoa.Value;

            _db.SaveChanges();
            return Ok("OK");
        }

        [HttpDelete("delete/{MaBenhNhan}")]
        public IActionResult Delete(int MaBenhNhan)
        {
            var bn = _db.BenhNhans.SingleOrDefault(x => x.MaBenhNhan == MaBenhNhan);
            if (bn == null) return NotFound();

            bn.DaXoa = true;
            _db.SaveChanges();
            return Ok("OK");
        }

        [HttpPost("search")]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var resp = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());

                string HoTen = "";
                if (formData.Keys.Contains("HoTen") && formData["HoTen"] != null)
                    HoTen = formData["HoTen"].ToString();

                string SoDienThoai = "";
                if (formData.Keys.Contains("SoDienThoai") && formData["SoDienThoai"] != null)
                    SoDienThoai = formData["SoDienThoai"].ToString();

                DateTime? fr_NgaySinh = null;
                if (formData.Keys.Contains("fr_NgaySinh") && !string.IsNullOrEmpty(Convert.ToString(formData["fr_NgaySinh"])))
                {
                    var dt = Convert.ToDateTime(formData["fr_NgaySinh"].ToString());
                    fr_NgaySinh = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                }
                DateTime? to_NgaySinh = null;
                if (formData.Keys.Contains("to_NgaySinh") && !string.IsNullOrEmpty(Convert.ToString(formData["to_NgaySinh"])))
                {
                    var dt = Convert.ToDateTime(formData["to_NgaySinh"].ToString());
                    to_NgaySinh = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                }

                var query = _db.BenhNhans
                               .Where(x => x.DaXoa == false)
                               .Select(x => new
                               {
                                   x.MaBenhNhan,
                                   x.HoTen,
                                   x.NgaySinh,
                                   x.GioiTinh,
                                   x.SoDienThoai,
                                   x.Email,
                                   x.DiaChi,
                                   x.CMTND_NV
                               });

                var filtered = query.Where(x =>
                    (string.IsNullOrEmpty(HoTen) || x.HoTen.Contains(HoTen)) &&
                    (string.IsNullOrEmpty(SoDienThoai) || x.SoDienThoai.Contains(SoDienThoai)) &&
                    (
                        (fr_NgaySinh == null && to_NgaySinh == null) ||
                        (fr_NgaySinh != null && to_NgaySinh == null && x.NgaySinh >= fr_NgaySinh) ||
                        (fr_NgaySinh == null && to_NgaySinh != null && x.NgaySinh <= to_NgaySinh) ||
                        (x.NgaySinh >= fr_NgaySinh && x.NgaySinh <= to_NgaySinh)
                    )
                ).ToList();

                resp.TotalItems = filtered.Count;
                resp.Page = page;
                resp.PageSize = pageSize;
                resp.Data = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resp;
        }
    }
}
