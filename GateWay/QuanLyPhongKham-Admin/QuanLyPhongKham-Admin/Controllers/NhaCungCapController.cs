using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db;

        public NhaCungCapController(QuanLyPhongKhamContext context)
        {
            db = context;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var obj = db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == id);
                return Ok(obj);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create(NhaCungCap model)
        {
            try
            {
                model.NgayTao = DateTime.Now;
                db.NhaCungCaps.Add(model);
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
        public IActionResult Update(NhaCungCap model)
        {
            try
            {
                var obj = db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == model.MaNhaCungCap);
                if (obj != null)
                {
                    obj.TenNhaCungCap = model.TenNhaCungCap;
                    obj.DiaChi = model.DiaChi;
                    obj.SoDienThoai = model.SoDienThoai;
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
                var obj = db.NhaCungCaps.SingleOrDefault(x => x.MaNhaCungCap == id);
                if (obj != null)
                {
                    db.NhaCungCaps.Remove(obj);
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
                if (formData.Keys.Contains("TenNhaCungCap"))
                    ten = Convert.ToString(formData["TenNhaCungCap"]);

                var data = db.NhaCungCaps
                             .Where(x => string.IsNullOrEmpty(ten) || x.TenNhaCungCap.Contains(ten))
                             .OrderByDescending(x => x.MaNhaCungCap)
                             .ToList();

                response.TotalItems = data.Count;
                response.Page = page;
                response.PageSize = pageSize;
                response.Data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            catch
            {
                return null; 
            }
            return response;
        }
    }
}
