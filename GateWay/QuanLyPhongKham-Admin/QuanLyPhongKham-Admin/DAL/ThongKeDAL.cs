using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.DAL
{
    public class ThongKeDAL
    {
        private readonly QuanLyPhongKhamContext _context;

        public ThongKeDAL(QuanLyPhongKhamContext context)
        {
            _context = context;
        }

        // 🔹 Lấy thống kê tổng hợp
        public async Task<object> LayThongKeTongHopAsync()
        {
            var tongBenhNhan = await _context.BenhNhan.CountAsync();
            var tongBacSi = await _context.BacSi.CountAsync();
            var tongLichHen = await _context.LichHen.CountAsync();
            var tongHoaDon = await _context.HoaDon.CountAsync();

            // Tính tổng doanh thu
            var tongDoanhThu = await _context.HoaDon.SumAsync(h => (double?)h.TongTien) ?? 0;

            return new
            {
                tongBenhNhan,
                tongBacSi,
                tongLichHen,
                tongHoaDon,
                tongDoanhThu
            };
        }
    }
}
