using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.DAL
{
    public class HoaDonDAL
    {
        private readonly QuanLyPhongKhamContext _context;

        public HoaDonDAL(QuanLyPhongKhamContext context)
        {
            _context = context;
        }

        // Lấy danh sách hóa đơn
        public async Task<List<HoaDon>> GetAllAsync()
        {
            return await _context.HoaDon
                .Include(h => h.BenhNhan)
                .Include(h => h.ChiTietHoaDon)
                .OrderByDescending(h => h.NgayLap)
                .ToListAsync();
        }

        // Lấy chi tiết hóa đơn
        public async Task<HoaDon?> GetByIdAsync(int id)
        {
            return await _context.HoaDon
                .Include(h => h.ChiTietHoaDon)
                .Include(h => h.BenhNhan)
                .FirstOrDefaultAsync(h => h.MaHoaDon == id);
        }

        // Thêm mới
        public async Task AddAsync(HoaDon hoaDon)
        {
            _context.HoaDon.Add(hoaDon);
            await _context.SaveChangesAsync();
        }

        // Cập nhật
        public async Task UpdateAsync(HoaDon hoaDon)
        {
            _context.HoaDon.Update(hoaDon);
            await _context.SaveChangesAsync();
        }

        // Xóa
        public async Task DeleteAsync(int id)
        {
            var hd = await _context.HoaDon.FindAsync(id);
            if (hd != null)
            {
                _context.HoaDon.Remove(hd);
                await _context.SaveChangesAsync();
            }
        }

        // ========== 6.2A - Lấy hóa đơn chưa thanh toán ==========
        public async Task<List<HoaDon>> GetChuaThanhToanAsync()
        {
            return await _context.HoaDon
                .Include(h => h.BenhNhan)
                .Where(h => h.TrangThai == null ||
                            h.TrangThai == "Chưa thanh toán" ||
                            h.TrangThai == "Chua thanh toan")
                .OrderByDescending(h => h.NgayLap)
                .ToListAsync();
        }
        public async Task<HoaDon?> GetHoaDonChiTietAsync(int id)
        {
            return await _context.HoaDon
                .Include(h => h.BenhNhan)
                .Include(h => h.ChiTietHoaDon!)
                    .ThenInclude(ct => ct.Thuoc)
                .FirstOrDefaultAsync(h => h.MaHoaDon == id);
        }

    }
}
