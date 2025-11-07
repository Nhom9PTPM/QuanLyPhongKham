using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham_NguoiDung.Code;
using QuanLyPhongKham_NguoiDung.Models;

namespace QuanLyPhongKham_NguoiDung.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoSoBenhAnController : ControllerBase
    {
        private QuanLyPhongKhamContext db = null;
        public HoSoBenhAnController(IConfiguration configuration)
        {
            db = new QuanLyPhongKhamContext(configuration);
        }

        [Route("get-hoso-benhnhan")]
        [HttpPost]
        public ResponseModel GetHoSoBenhNhan([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                int page = int.Parse(formData["page"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                int maBenhNhan = int.Parse(formData["MaBenhNhan"].ToString());

                var query = from hs in db.HoSoBenhAns
                            where hs.MaBenhNhan == maBenhNhan && hs.DaXoa == false
                            orderby hs.NgayLap descending
                            select new
                            {
                                hs.MaHoSo,
                                hs.TomTatBenhLy,
                                hs.ChanDoanChinh,
                                hs.NgayLap,
                                hs.NguoiLap
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

        [Route("chi-tiet/{maHoSo}")]
        [HttpGet]
        public IActionResult ChiTietHoSo(int maHoSo)
        {
            try
            {
                var hoSo = (from hs in db.HoSoBenhAns
                            join bn in db.BenhNhans on hs.MaBenhNhan equals bn.MaBenhNhan
                            where hs.MaHoSo == maHoSo
                            select new
                            {
                                hs.MaHoSo,
                                hs.MaBenhNhan,
                                BenhNhan = bn.HoTen,
                                hs.TomTatBenhLy,
                                hs.ChanDoanChinh,
                                hs.LichSuBenhLy,
                                hs.TapTinDinhKem,
                                hs.NguoiLap,
                                hs.NgayLap
                            }).FirstOrDefault();

                if (hoSo == null)
                    return NotFound("Không tìm thấy hồ sơ bệnh án");

                var kham = db.KhamBenhs
                    .Where(x => x.MaHoSo == maHoSo && x.DaXoa == false)
                    .Select(x => new
                    {
                        x.MaKham,
                        x.ChanDoan,
                        x.ChiDinhXN,
                        x.ChiDinhCLS,
                        x.NgayKham,
                        BacSi = (from bs in db.BacSis
                                 join nd in db.NguoiDungs on bs.MaNguoiDung equals nd.MaNguoiDung
                                 where bs.MaBacSi == x.MaBacSi
                                 select nd.HoTen).FirstOrDefault()
                    }).ToList();

                var donThuoc = (from dt in db.DonThuocs
                                join kb in db.KhamBenhs on dt.MaKham equals kb.MaKham
                                where kb.MaHoSo == maHoSo && dt.DaXoa == false
                                select new
                                {
                                    dt.MaDonThuoc,
                                    dt.NgayKe,
                                    dt.NguoiKe,
                                    dt.GhiChu,
                                    ChiTiet = (from ct in db.ChiTietDonThuocs
                                               join t in db.Thuocs on ct.MaThuoc equals t.MaThuoc
                                               where ct.MaDonThuoc == dt.MaDonThuoc
                                               select new
                                               {
                                                   t.TenThuoc,
                                                   ct.SoLuong,
                                                   ct.CachDung,
                                                   ct.DonGia
                                               }).ToList()
                                }).ToList();

                var tapTin = db.TapTins
                    .Where(x => x.MaHoSo == maHoSo)
                    .Select(x => new
                    {
                        x.MaTapTin,
                        x.TenTapTin,
                        x.DuongDan,
                        x.DinhDang,
                        x.NgayTao
                    }).ToList();

                return Ok(new
                {
                    HoSo = hoSo,
                    KhamBenh = kham,
                    DonThuoc = donThuoc,
                    TapTin = tapTin
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi lấy chi tiết hồ sơ", error = ex.Message });
            }
        }

        [Route("get-hoadon-benhnhan/{maBenhNhan}")]
        [HttpGet]
        public IActionResult GetHoaDonByBenhNhan(int maBenhNhan)
        {
            try
            {
                var query = from hd in db.HoaDons
                            where hd.MaBenhNhan == maBenhNhan
                            orderby hd.NgayLap descending
                            select new
                            {
                                hd.MaHoaDon,
                                hd.NgayLap,
                                hd.TongTien,
                                hd.PhuongThucThanhToan,
                                hd.TrangThai,
                                ChiTiet = (from ct in db.ChiTietHoaDons
                                           join t in db.Thuocs on ct.MaThuoc equals t.MaThuoc into tmp
                                           from thuoc in tmp.DefaultIfEmpty()
                                           where ct.MaHoaDon == hd.MaHoaDon
                                           select new
                                           {
                                               TenThuoc = thuoc != null ? thuoc.TenThuoc : ct.MaDichVu,
                                               ct.SoLuong,
                                               ct.DonGia
                                           }).ToList()
                            };

                var result = query.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi khi lấy danh sách hóa đơn", error = ex.Message });
            }
        }
    }
}
