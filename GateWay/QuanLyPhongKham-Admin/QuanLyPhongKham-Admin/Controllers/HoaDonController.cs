using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db;

        public HoaDonController(QuanLyPhongKhamContext context)
        {
            db = context;
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
        public IActionResult CreateHD([FromBody] HoaDonModels model)
        {
            var hd = model.hoadon;

            // Thiết lập thông tin ban đầu
            hd.NgayLap = DateTime.Now;

            // Nếu list chi tiết rỗng thì trả lỗi
            if (model.listchitiet == null || !model.listchitiet.Any())
                return BadRequest("Danh sách chi tiết hóa đơn không được rỗng.");

            // Tính tổng tiền từ chi tiết
            double tong = 0;
            foreach (var ct in model.listchitiet)
            {
                tong += (ct.SoLuong ?? 0) * (ct.DonGia ?? 0);
            }
            hd.TongTien = tong;

            // Thêm hóa đơn vào database
            db.HoaDons.Add(hd);
            db.SaveChanges(); // Save để có MaHoaDon cho chi tiết

            // Thêm chi tiết hóa đơn
            foreach (var ct in model.listchitiet)
            {
                ct.MaHoaDon = hd.MaHoaDon;
                db.ChiTietHoaDons.Add(ct);
            }

            db.SaveChanges();

            return Ok(new { message = "Tạo hóa đơn thành công!", MaHoaDon = hd.MaHoaDon });
        }


        [Route("update-hoadon")]
        [HttpPost]
        public IActionResult UpdateHoaDon([FromBody] HoaDonModels model)
        {
            var hd = db.HoaDons.Include(x => x.ChiTietHoaDons)
                                .FirstOrDefault(x => x.MaHoaDon == model.hoadon.MaHoaDon);
            if (hd == null) return BadRequest("Không tồn tại hóa đơn!");

            // Cập nhật thông tin chung
            hd.MaBenhNhan = model.hoadon.MaBenhNhan;
            hd.PhuongThucThanhToan = model.hoadon.PhuongThucThanhToan;
            hd.TrangThai = model.hoadon.TrangThai;
            hd.NgayLap = DateTime.Now;

            // Danh sách chi tiết gửi lên
            var updatedList = model.listchitiet;

            // Xoá chi tiết đã bị loại bỏ
            var chiTietToDelete = hd.ChiTietHoaDons
                                     .Where(c => !updatedList.Any(u => u.MaChiTietHoaDon == c.MaChiTietHoaDon))
                                     .ToList();
            if (chiTietToDelete.Any())
                db.ChiTietHoaDons.RemoveRange(chiTietToDelete);

            double tong = 0;

            foreach (var ct in updatedList)
            {
                if (ct.MaChiTietHoaDon > 0)
                {
                    // Cập nhật chi tiết đã tồn tại
                    var existingCT = hd.ChiTietHoaDons.FirstOrDefault(x => x.MaChiTietHoaDon == ct.MaChiTietHoaDon);
                    if (existingCT != null)
                    {
                        existingCT.MaThuoc = ct.MaThuoc;
                        existingCT.SoLuong = ct.SoLuong;
                        existingCT.DonGia = ct.DonGia;
                    }
                }
                else
                {
                    // Thêm chi tiết mới
                    var newCT = new ChiTietHoaDon()
                    {
                        MaHoaDon = hd.MaHoaDon,
                        MaThuoc = ct.MaThuoc,
                        SoLuong = ct.SoLuong,
                        DonGia = ct.DonGia
                    };
                    db.ChiTietHoaDons.Add(newCT);
                }

                tong += (ct.SoLuong ?? 0) * (ct.DonGia ?? 0);
            }

            hd.TongTien = tong;

            db.SaveChanges();

            return Ok("Cập nhật thành công!");
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
