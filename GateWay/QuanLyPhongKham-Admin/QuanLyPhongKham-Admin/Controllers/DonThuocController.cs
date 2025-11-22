using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QuanLyPhongKham_Admin.Models;
using QuanLyPhongKham_Admin.Code;
using System;
using System.Linq;
using System.Collections.Generic;

namespace QuanLyPhongKham_Admin.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DonThuocController : ControllerBase
    {
        private readonly QuanLyPhongKhamContext db;

        public DonThuocController(QuanLyPhongKhamContext context)
        {
            db = context;
        }
        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var donThuoc = db.DonThuocs
                    .Where(x => x.MaDonThuoc == id)
                    .Select(x => new
                    {
                        x.MaDonThuoc,
                        x.MaKham,
                        x.MaBacSi,
                        x.GhiChu,
                        x.NguoiKe,
                        x.NgayKe
                    })
                    .SingleOrDefault();

                if (donThuoc == null)
                    return NotFound("Không tìm thấy đơn thuốc");

                var chiTiet = db.ChiTietDonThuocs
                    .Where(x => x.MaDonThuoc == id)
                    .Select(x => new
                    {
                        x.MaChiTietDon,
                        x.MaThuoc,
                        x.SoLuong,
                        x.CachDung,
                        x.DonGia
                    })
                    .ToList();

                return Ok(new { donThuoc, chiTiet });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("create-donthuoc")]
        [HttpPost]
        public IActionResult CreateDonThuoc(DonThuocModels model)
        {
            if (model == null || model.donthuoc == null)
                return BadRequest("Dữ liệu đơn thuốc không hợp lệ");

            try
            {
                var donThuoc = new DonThuoc
                {
                    MaKham = model.donthuoc.MaKham,
                    MaBacSi = model.donthuoc.MaBacSi,
                    NgayKe = DateTime.Now,
                    GhiChu = model.donthuoc.GhiChu,
                    NguoiKe = model.donthuoc.NguoiKe,
                    DaXoa = false
                };

                db.DonThuocs.Add(donThuoc);
                db.SaveChanges();

                if (model.listchitiet != null && model.listchitiet.Count > 0)
                {
                    foreach (var x in model.listchitiet)
                    {
                        // Kiểm tra tồn tại Thuốc
                        var thuoc = db.Thuocs.Find(x.MaThuoc);
                        if (thuoc == null)
                            return BadRequest($"Thuốc với MaThuoc={x.MaThuoc} không tồn tại");

                        var ct = new ChiTietDonThuoc
                        {
                            MaDonThuoc = donThuoc.MaDonThuoc,
                            MaThuoc = x.MaThuoc,
                            SoLuong = x.SoLuong,
                            CachDung = x.CachDung,
                            DonGia = x.DonGia
                        };
                        db.ChiTietDonThuocs.Add(ct);
                    }
                    db.SaveChanges();
                }

                return Ok(new { donThuoc.MaDonThuoc });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }

        }

        [Route("update-donthuoc")]
        [HttpPost]
        public IActionResult UpdateDonThuoc(DonThuocEditModels model)
        {
            if (model == null || model.donthuoc == null)
                return BadRequest("Dữ liệu đơn thuốc không hợp lệ");

            try
            {
                // Lấy đơn thuốc cần update
                var dt = db.DonThuocs.SingleOrDefault(x => x.MaDonThuoc == model.donthuoc.MaDonThuoc);
                if (dt == null)
                    return NotFound("Đơn thuốc không tồn tại");

                // Cập nhật thông tin đơn thuốc
                dt.GhiChu = model.donthuoc.GhiChu;
                dt.NgayKe = model.donthuoc.NgayKe;
                dt.NguoiKe = model.donthuoc.NguoiKe;
                db.SaveChanges();

                // Lấy danh sách chi tiết hiện có trong DB
                var currentChiTiet = db.ChiTietDonThuocs.Where(c => c.MaDonThuoc == dt.MaDonThuoc).ToList();

                // Xoá những chi tiết không còn trong danh sách gửi lên
                var incomingIds = model.listchitiet?.Select(x => x.MaChiTietDon).ToList() ?? new List<int>();
                var toDelete = currentChiTiet.Where(c => !incomingIds.Contains(c.MaChiTietDon)).ToList();
                if (toDelete.Count > 0)
                    db.ChiTietDonThuocs.RemoveRange(toDelete);

                db.SaveChanges();

                // Thêm hoặc cập nhật chi tiết
                if (model.listchitiet != null && model.listchitiet.Count > 0)
                {
                    foreach (var x in model.listchitiet)
                    {
                        // Kiểm tra tồn tại Thuốc
                        var thuoc = db.Thuocs.Find(x.MaThuoc);
                        if (thuoc == null)
                            return BadRequest($"Thuốc với MaThuoc={x.MaThuoc} không tồn tại");

                        if (x.MaChiTietDon == 0)
                        {
                            // Thêm mới chi tiết
                            var newCt = new ChiTietDonThuoc
                            {
                                MaDonThuoc = dt.MaDonThuoc,
                                MaThuoc = x.MaThuoc,
                                SoLuong = x.SoLuong,
                                CachDung = x.CachDung,
                                DonGia = x.DonGia
                            };
                            db.ChiTietDonThuocs.Add(newCt);
                        }
                        else
                        {
                            // Cập nhật chi tiết cũ
                            var oldCt = db.ChiTietDonThuocs.SingleOrDefault(s => s.MaChiTietDon == x.MaChiTietDon);
                            if (oldCt != null)
                            {
                                oldCt.MaThuoc = x.MaThuoc;
                                oldCt.SoLuong = x.SoLuong;
                                oldCt.CachDung = x.CachDung;
                                oldCt.DonGia = x.DonGia;
                            }
                        }
                    }
                    db.SaveChanges();
                }

                return Ok("OK");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("delete-donthuoc/{id}")]
        [HttpGet]
        public IActionResult DeleteDonThuoc(int id)
        {
            try
            {
                var chiTiet = db.ChiTietDonThuocs.Where(x => x.MaDonThuoc == id).ToList();
                if (chiTiet.Count > 0)
                    db.ChiTietDonThuocs.RemoveRange(chiTiet);

                var dt = db.DonThuocs.SingleOrDefault(x => x.MaDonThuoc == id);
                if (dt != null)
                    db.DonThuocs.Remove(dt);

                db.SaveChanges();

                return Ok("OK");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();

            try
            {
                int page = int.Parse(formData["page"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());

                string BacSi = "";
                if (formData.ContainsKey("BacSi") && formData["BacSi"] != null)
                    BacSi = formData["BacSi"].ToString();

                var query = from d in db.DonThuocs
                            join b in db.BacSis on d.MaBacSi equals b.MaBacSi into bs
                            from bacsi in bs.DefaultIfEmpty()
                            select new
                            {
                                d.MaDonThuoc,
                                d.GhiChu,
                                d.NgayKe,

                                BacSi = bacsi != null && bacsi.MaNguoiDungNavigation != null
                                    ? bacsi.MaNguoiDungNavigation.HoTen
                                    : null
                            };

                var data = query
                    .Where(x => string.IsNullOrEmpty(BacSi) || (x.BacSi ?? "").Contains(BacSi))
                    .OrderByDescending(x => x.MaDonThuoc)
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
