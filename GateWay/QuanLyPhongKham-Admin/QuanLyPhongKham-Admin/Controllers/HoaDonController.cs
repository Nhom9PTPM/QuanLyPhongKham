using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;

        public HoaDonController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var hd = db.HoaDons.Where(x => x.MaHoaDon == id).SingleOrDefault();
                var ct = db.ChiTietHoaDons.Where(x => x.MaHoaDon == id).ToList();
                return Ok(new { hd, ct });
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("create-hoadon")]
        [HttpPost]
        public IActionResult CreateHoaDon(HoaDonModels model)
        {
            try
            {
                db.HoaDons.Add(model.hoadon);
                db.SaveChanges();

                if (model.listchitiet != null && model.listchitiet.Count > 0)
                {
                    foreach (var x in model.listchitiet)
                        x.MaHoaDon = model.hoadon.MaHoaDon;

                    model.hoadon.ChiTietHoaDons = model.listchitiet;
                    db.SaveChanges();
                }

                return Ok("OK");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("update-hoadon")]
        [HttpPost]
        public IActionResult UpdateHoaDon(HoaDonEditModels model)
        {
            try
            {
                var hd = db.HoaDons.SingleOrDefault(x => x.MaHoaDon == model.hoadon.MaHoaDon);

                hd.TrangThai = model.hoadon.TrangThai;
                hd.PhuongThucThanhToan = model.hoadon.PhuongThucThanhToan;
                db.SaveChanges();

                if (model.listchitiet != null && model.listchitiet.Count > 0)
                {
                    foreach (var x in model.listchitiet)
                    {
                        if (x.MaChiTietHoaDon == 0)
                        {
                            var c = new ChiTietHoaDon();
                            c.MaHoaDon = hd.MaHoaDon;
                            c.MaThuoc = x.MaThuoc;
                            c.SoLuong = x.SoLuong;
                            c.DonGia = x.DonGia;
                            db.ChiTietHoaDons.Add(c);
                        }
                        else
                        {
                            var obj = db.ChiTietHoaDons.SingleOrDefault(s => s.MaChiTietHoaDon == x.MaChiTietHoaDon);
                            obj.SoLuong = x.SoLuong;
                            obj.DonGia = x.DonGia;
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

        [Route("delete-hoadon/{id}")]
        [HttpGet]
        public IActionResult DeleteHoaDon(int id)
        {
            try
            {
                var ct = db.ChiTietHoaDons.Where(x => x.MaHoaDon == id).ToList();
                db.ChiTietHoaDons.RemoveRange(ct);
                db.SaveChanges();

                var hd = db.HoaDons.SingleOrDefault(x => x.MaHoaDon == id);
                db.HoaDons.Remove(hd);
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

                string TrangThai = "";
                if (formData.Keys.Contains("TrangThai"))
                    TrangThai = Convert.ToString(formData["TrangThai"]);

                var query = from h in db.HoaDons
                            join b in db.BenhNhans on h.MaBenhNhan equals b.MaBenhNhan into bn
                            from benh in bn.DefaultIfEmpty()
                            select new
                            {
                                h.MaHoaDon,
                                h.NgayLap,
                                h.TongTien,
                                h.PhuongThucThanhToan,
                                h.TrangThai,
                                TenBenhNhan = benh.HoTen
                            };

                var data = query.Where(x =>
                        (TrangThai == "" || x.TrangThai.Contains(TrangThai)))
                        .OrderByDescending(x => x.MaHoaDon).ToList();

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
