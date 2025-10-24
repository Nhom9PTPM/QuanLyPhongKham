using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonThuocController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        public DonThuocController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var dt = db.DonThuocs.Where(x => x.MaDonThuoc == id).Select(
                    x => new
                    {
                        x.MaDonThuoc,
                        x.MaKham,
                        x.MaBacSi,
                        x.GhiChu,
                        x.NguoiKe,
                        x.NgayKe
                    }).SingleOrDefault();

                var chitiet = db.ChiTietDonThuocs.Where(x => x.MaDonThuoc == id).ToList();

                return Ok(new { dt, chitiet });
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-donthuoc")]
        [HttpPost]
        public IActionResult CreateDonThuoc(DonThuocModels model)
        {
            try
            {
                model.donthuoc.NgayKe = DateTime.Now;
                db.DonThuocs.Add(model.donthuoc);
                db.SaveChanges();

                if (model.listchitiet != null && model.listchitiet.Count > 0)
                {
                    foreach (var x in model.listchitiet)
                        x.MaDonThuoc = model.donthuoc.MaDonThuoc;

                    model.donthuoc.ChiTietDonThuocs = model.listchitiet;
                    db.SaveChanges();
                }

                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("update-donthuoc")]
        [HttpPost]
        public IActionResult UpdateDonThuoc(DonThuocEditModels model)
        {
            try
            {
                var dt = db.DonThuocs.SingleOrDefault(x => x.MaDonThuoc == model.donthuoc.MaDonThuoc);

                dt.GhiChu = model.donthuoc.GhiChu;
                db.SaveChanges();

                if (model.listchitiet != null && model.listchitiet.Count > 0)
                {
                    foreach (var x in model.listchitiet)
                    {
                        if (x.MaChiTietDon == 0)
                        {
                            var c = new ChiTietDonThuoc();
                            c.MaDonThuoc = dt.MaDonThuoc;
                            c.MaThuoc = x.MaThuoc;
                            c.SoLuong = x.SoLuong;
                            c.CachDung = x.CachDung;
                            c.DonGia = x.DonGia;
                            db.ChiTietDonThuocs.Add(c);
                        }
                        else
                        {
                            var obj = db.ChiTietDonThuocs.SingleOrDefault(s => s.MaChiTietDon == x.MaChiTietDon);
                            if (obj != null)
                            {
                                obj.SoLuong = x.SoLuong;
                                obj.CachDung = x.CachDung;
                                obj.DonGia = x.DonGia;
                            }
                        }
                        db.SaveChanges();
                    }
                }

                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("delete-donthuoc/{id}")]
        [HttpGet]
        public IActionResult DeleteDonThuoc(int id)
        {
            try
            {
                var ct = db.ChiTietDonThuocs.Where(x => x.MaDonThuoc == id).ToList();
                db.ChiTietDonThuocs.RemoveRange(ct);
                db.SaveChanges();

                var dt = db.DonThuocs.SingleOrDefault(x => x.MaDonThuoc == id);
                db.DonThuocs.Remove(dt);
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
                string BacSi = "";
                if (formData.Keys.Contains("BacSi"))
                    BacSi = Convert.ToString(formData["BacSi"]);

                var query = from d in db.DonThuocs
                            join b in db.BacSis on d.MaBacSi equals b.MaBacSi into bs
                            from bacsi in bs.DefaultIfEmpty()
                            select new
                            {
                                d.MaDonThuoc,
                                d.GhiChu,
                                d.NgayKe,
                                BacSi = bacsi.SoPhong
                            };

                var data = query.Where(x => (BacSi == "" || x.BacSi.Contains(BacSi)))
                                .OrderByDescending(x => x.MaDonThuoc).ToList();

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
