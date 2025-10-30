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

        // 6.3A - Lấy thống kê tổng hợp (đã có)
        public async Task<object> LayThongKeTongHopAsync()
        {
            var tongBenhNhan = await _context.BenhNhan.CountAsync();
            var tongBacSi = await _context.BacSi.CountAsync();
            var tongLichHen = await _context.LichHen.CountAsync();
            var tongHoaDon = await _context.HoaDon.CountAsync();

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

       
        // 6.3B - Báo cáo doanh thu theo bác sĩ hoặc chuyên khoa
        
        public async Task<List<dynamic>> BaoCaoDoanhThuAsync(DateTime? tuNgay, DateTime? denNgay, string loaiBaoCao)
        {
            // Lấy dữ liệu từ các bảng có liên quan
            var query = _context.KhamBenh
                .Include(kb => kb.BenhNhan)
                .Include(kb => kb.BacSi)
                .Join(_context.HoaDon,
                      kb => kb.MaBenhNhan,
                      hd => hd.MaBenhNhan,
                      (kb, hd) => new
                      {
                          kb.MaBacSi,
                          kb.BacSi.ChuyenKhoa,
                          hd.TongTien,
                          hd.NgayLap
                      })
                .AsQueryable();

            // Lọc theo thời gian
            if (tuNgay.HasValue)
                query = query.Where(x => x.NgayLap >= tuNgay.Value);
            if (denNgay.HasValue)
                query = query.Where(x => x.NgayLap <= denNgay.Value);

            // Gom nhóm theo loại báo cáo
            if (loaiBaoCao?.ToLower() == "chuyenkhoa")
            {
                return await query
                    .GroupBy(x => x.ChuyenKhoa)
                    .Select(g => new
                    {
                        ChuyenKhoa = g.Key,
                        TongDoanhThu = g.Sum(x => x.TongTien),
                        SoHoaDon = g.Count()
                    })
                    .OrderByDescending(x => x.TongDoanhThu)
                    .ToListAsync<dynamic>();
            }
            else // Mặc định theo bác sĩ
            {
                return await query
                    .GroupBy(x => x.MaBacSi)
                    .Select(g => new
                    {
                        MaBacSi = g.Key,
                        TongDoanhThu = g.Sum(x => x.TongTien),
                        SoHoaDon = g.Count()
                    })
                    .OrderByDescending(x => x.TongDoanhThu)
                    .ToListAsync<dynamic>();
            }
        }
    }
}
