using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BacSiController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        private ITools _tools;
        public BacSiController(ITools tools, IConfiguration configuration)
        {
            _tools = tools;
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var bs = db.BacSis.Where(x => x.MaBacSi == id)
                .Select(x => new { x.MaBacSi, x.ChuyenKhoa, x.BangCap, x.KinhNghiem, x.SoPhong })
                .SingleOrDefault();
            return Ok(bs);
        }

        [Route("create-bacsi")]
        [HttpPost]
        public IActionResult Create(BacSi model)
        {
            db.BacSis.Add(model);
            db.SaveChanges();
            return Ok("OK");
        }

        [Route("update-bacsi")]
        [HttpPost]
        public IActionResult Update(BacSi model)
        {
            var bs = db.BacSis.SingleOrDefault(x => x.MaBacSi == model.MaBacSi);

            bs.ChuyenKhoa = model.ChuyenKhoa;
            bs.BangCap = model.BangCap;
            bs.SoPhong = model.SoPhong;

            db.SaveChanges();
            return Ok("OK");
        }

        [Route("delete-bacsi/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var bs = db.BacSis.SingleOrDefault(x => x.MaBacSi == id);
            db.BacSis.Remove(bs);
            db.SaveChanges();
            return Ok("OK");
        }

        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            string CK = "";
            if (formData.Keys.Contains("ChuyenKhoa"))
                CK = formData["ChuyenKhoa"].ToString();

            var data = db.BacSis.Where(x => x.ChuyenKhoa.Contains(CK)).ToList();

            response.TotalItems = data.Count;
            response.Page = page;
            response.PageSize = pageSize;
            response.Data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return response;
        }
    }
}
