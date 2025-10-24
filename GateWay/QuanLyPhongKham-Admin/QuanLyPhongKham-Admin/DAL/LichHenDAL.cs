using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.DAL
{
    public class LichHenDAL
    {
        private readonly QuanLyPhongKhamContext _context;

        public LichHenDAL(QuanLyPhongKhamContext context)
        {
            _context = context;
        }

        // Lấy tất cả lịch hẹn
        public async Task<List<LichHen>> GetAllAsync()
        {
            return await _context.LichHen.Where(l => !l.DaXoa)
                .OrderByDescending(l => l.NgayBatDau)
                .ToListAsync();
        }

        // Lấy chi tiết lịch hẹn
        public async Task<LichHen?> GetByIdAsync(int id)
        {
            return await _context.LichHen.FirstOrDefaultAsync(l => l.MaLichHen == id && !l.DaXoa);
        }

        // Liên kết lịch hẹn với hồ sơ
        public async Task<bool> CapNhatTrangThaiVaLienKetAsync(int maLichHen, int maHoSo)
        {
            var lichHen = await _context.LichHen.FirstOrDefaultAsync(l => l.MaLichHen == maLichHen && !l.DaXoa);
            if (lichHen == null) return false;

            lichHen.TrangThai = "DaKham";
            lichHen.GhiChu = $"Đã liên kết với hồ sơ bệnh án mã {maHoSo}";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
