using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TonKhoThuocController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        private ITools _tools;

        public TonKhoThuocController(ITools tools, IConfiguration configuration)
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
                var obj = db.TonKhoThuocs.Where(x => x.MaTonKho == id).Select(
                    x => new {
                        x.MaTonKho,
                        x.MaThuoc,
                        x.SoLuong,
                        x.MaKho,
                        x.NgayCapNhat
                    }).SingleOrDefault();

                return Ok(obj);
            }
            catch { return BadRequest(); }
        }

        [Route("create-tonkho")]
        [HttpPost]
        public IActionResult CreateTonKho(TonKhoThuocModels model)
        {
            try
            {
                model.tonkho.NgayCapNhat = DateTime.Now;
                db.TonKhoThuocs.Add(model.tonkho);
                db.SaveChanges();
                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("update-tonkho")]
        [HttpPost]
        public IActionResult UpdateTonKho(TonKhoThuocEditModels model)
        {
            try
            {
                var obj = db.TonKhoThuocs.SingleOrDefault(x => x.MaTonKho == model.MaTonKho);
                if (obj != null)
                {
                    obj.SoLuong = model.SoLuong ?? obj.SoLuong;
                    obj.MaKho = string.IsNullOrEmpty(model.MaKho) ? obj.MaKho : model.MaKho;
                    obj.NgayCapNhat = DateTime.Now;
                    db.SaveChanges();
                }
                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("delete-tonkho/{id}")]
        [HttpGet]
        public IActionResult DeleteTonKho(int id)
        {
            try
            {
                var obj = db.TonKhoThuocs.SingleOrDefault(x => x.MaTonKho == id);
                db.TonKhoThuocs.Remove(obj);
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

                string TenThuoc = "";
                if (formData.Keys.Contains("TenThuoc") && !string.IsNullOrEmpty(Convert.ToString(formData["TenThuoc"])))
                    TenThuoc = Convert.ToString(formData["TenThuoc"]);

                var query = from t in db.TonKhoThuocs
                            join m in db.Thuocs on t.MaThuoc equals m.MaThuoc
                            select new
                            {
                                t.MaTonKho,
                                t.MaThuoc,
                                m.TenThuoc,
                                t.SoLuong,
                                t.MaKho,
                                t.NgayCapNhat
                            };

                var data = query.Where(x => TenThuoc == "" || x.TenThuoc.Contains(TenThuoc))
                                .OrderByDescending(x => x.MaTonKho)
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
