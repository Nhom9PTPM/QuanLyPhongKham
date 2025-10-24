using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;

namespace QuanLyPhongKham_Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThuocController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        private ITools _tools;

        public ThuocController(ITools tools, IConfiguration configuration)
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
                var obj = db.Thuocs.SingleOrDefault(x => x.MaThuoc == id);
                return Ok(obj);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-thuoc")]
        [HttpPost]
        public IActionResult CreateThuoc(ThuocModels model)
        {
            try
            {
                model.thuoc.NgayTao = DateTime.Now;
                db.Thuocs.Add(model.thuoc);
                db.SaveChanges();
                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("update-thuoc")]
        [HttpPost]
        public IActionResult UpdateThuoc(ThuocEditModels model)
        {
            try
            {
                var obj = db.Thuocs.SingleOrDefault(x => x.MaThuoc == model.thuoc.MaThuoc);
                if (obj != null)
                {
                    obj.TenThuoc = string.IsNullOrEmpty(model.thuoc.TenThuoc) ? obj.TenThuoc : model.thuoc.TenThuoc;
                    obj.MoTa = model.thuoc.MoTa;
                    obj.DonViTinh = model.thuoc.DonViTinh;
                    obj.Gia = model.thuoc.Gia;
                    obj.MaNhaCungCap = model.thuoc.MaNhaCungCap;
                    obj.TrangThai = model.thuoc.TrangThai;
                    db.SaveChanges();
                }
                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("delete-thuoc/{MaThuoc}")]
        [HttpGet]
        public IActionResult DeleteThuoc(int MaThuoc)
        {
            try
            {
                var obj = db.Thuocs.SingleOrDefault(x => x.MaThuoc == MaThuoc);
                db.Thuocs.Remove(obj);
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
                else return BadRequest();
            }
            catch
            {
                return StatusCode(500, "Không thể upload tệp");
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

                string TenThuoc = "";
                if (formData.Keys.Contains("TenThuoc") && !string.IsNullOrEmpty(Convert.ToString(formData["TenThuoc"])))
                    TenThuoc = Convert.ToString(formData["TenThuoc"]);

                var query = from t in db.Thuocs
                            join n in db.NhaCungCaps on t.MaNhaCungCap equals n.MaNhaCungCap into tmp
                            from ncc in tmp.DefaultIfEmpty()
                            select new
                            {
                                t.MaThuoc,
                                t.TenThuoc,
                                t.Gia,
                                t.DonViTinh,
                                t.TrangThai,
                                ncc.TenNhaCungCap
                            };

                var data = query.Where(x => (TenThuoc == "" || x.TenThuoc.Contains(TenThuoc)))
                                .OrderByDescending(x => x.MaThuoc)
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
