using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_NguoiDung.Code;
using QuanLyPhongKham_NguoiDung.Models;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichHenController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        public LichHenController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("dat-lich")]
        [HttpPost]
        public IActionResult DatLich([FromBody] LichHen model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Dữ liệu không hợp lệ");

                var benhNhan = db.BenhNhans.FirstOrDefault(x => x.MaBenhNhan == model.MaBenhNhan && x.DaXoa == false);
                if (benhNhan == null)
                    return NotFound("Bệnh nhân không tồn tại");

                if (model.MaBacSi != null)
                {
                    var bacSi = db.BacSis.FirstOrDefault(x => x.MaBacSi == model.MaBacSi && x.TrangThai == true);
                    if (bacSi == null)
                        return NotFound("Bác sĩ không tồn tại hoặc không hoạt động");
                }

                model.TrangThai = "DaDat";
                model.NgayTao = DateTime.Now;
                db.LichHens.Add(model);
                db.SaveChanges();

                return Ok(new { message = "Đặt lịch hẹn thành công", model });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi đặt lịch", error = ex.Message });
            }
        }

        [Route("get-lichhen-benhnhan")]
        [HttpPost]
        public ResponseModel GetLichHenByBenhNhan([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                int page = int.Parse(formData["page"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                int maBenhNhan = int.Parse(formData["MaBenhNhan"].ToString());

                var query = from lh in db.LichHens
                            join bs in db.BacSis on lh.MaBacSi equals bs.MaBacSi into tmp
                            from bacsi in tmp.DefaultIfEmpty()
                            join nd in db.NguoiDungs on bacsi.MaNguoiDung equals nd.MaNguoiDung into tmp2
                            from nguoidung in tmp2.DefaultIfEmpty()
                            where lh.MaBenhNhan == maBenhNhan && lh.DaXoa == false
                            orderby lh.NgayBatDau descending
                            select new
                            {
                                lh.MaLichHen,
                                lh.NgayBatDau,
                                lh.GioHen,
                                lh.TrangThai,
                                lh.GhiChu,
                                BacSi = nguoidung != null ? nguoidung.HoTen : "Chưa chỉ định",
                                ChuyenKhoa = bacsi != null ? bacsi.ChuyenKhoa : ""
                            };

                var data = query.ToList();
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

        [Route("huy-lich/{id}")]
        [HttpPut]
        public IActionResult HuyLich(int id)
        {
            try
            {
                var lichHen = db.LichHens.FirstOrDefault(x => x.MaLichHen == id && x.DaXoa == false);
                if (lichHen == null)
                    return NotFound("Không tìm thấy lịch hẹn");

                lichHen.TrangThai = "DaHuy";
                lichHen.GhiChu = "Bệnh nhân hủy lịch";
                db.SaveChanges();

                return Ok(new { message = "Hủy lịch hẹn thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi hủy lịch", error = ex.Message });
            }
        }

        [Route("chi-tiet/{id}")]
        [HttpGet]
        public IActionResult ChiTietLichHen(int id)
        {
            try
            {
                var lich = (from lh in db.LichHens
                            join bn in db.BenhNhans on lh.MaBenhNhan equals bn.MaBenhNhan
                            join bs in db.BacSis on lh.MaBacSi equals bs.MaBacSi into tmp
                            from bacsi in tmp.DefaultIfEmpty()
                            join nd in db.NguoiDungs on bacsi.MaNguoiDung equals nd.MaNguoiDung into tmp2
                            from nguoidung in tmp2.DefaultIfEmpty()
                            where lh.MaLichHen == id
                            select new
                            {
                                lh.MaLichHen,
                                lh.NgayBatDau,
                                lh.NgayKetThuc,
                                lh.GioHen,
                                lh.TrangThai,
                                lh.GhiChu,
                                BenhNhan = bn.HoTen,
                                BacSi = nguoidung != null ? nguoidung.HoTen : "Chưa chỉ định",
                                ChuyenKhoa = bacsi != null ? bacsi.ChuyenKhoa : ""
                            }).FirstOrDefault();

                if (lich == null)
                    return NotFound("Không tìm thấy lịch hẹn");

                return Ok(lich);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi lấy chi tiết lịch", error = ex.Message });
            }
        }
    }
}
