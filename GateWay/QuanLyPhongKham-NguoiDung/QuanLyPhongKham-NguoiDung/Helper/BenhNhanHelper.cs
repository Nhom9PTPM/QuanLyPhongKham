using QuanLyPhongKham_NguoiDung.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace QuanLyPhongKham_NguoiDung.Helper
{
    public static class BenhNhanHelper
    {
        
        public static int? GetMaBenhNhanFromUser(QuanLyPhongKhamContext db, ClaimsPrincipal user)
        {
            if (user == null) return null;

            var username = user.Identity?.Name;
            if (string.IsNullOrEmpty(username)) return null;

            var taiKhoan = db.TaiKhoans.FirstOrDefault(t => t.TenDangNhap == username);
            if (taiKhoan == null) return null;

            var nguoiDung = db.NguoiDungs.FirstOrDefault(u => u.MaNguoiDung == taiKhoan.MaNguoiDung);
            if (nguoiDung == null) return null;

            var benhNhan = db.BenhNhans.FirstOrDefault(b => b.Email == nguoiDung.Email);
            if (benhNhan == null) return null;

            return benhNhan.MaBenhNhan;
        }
    }
}
