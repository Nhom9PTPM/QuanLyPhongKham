using QuanLyPhongKham_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyPhongKham_Admin.DAL
{
    public class AuditLogDAL
    {
        private readonly QuanLyPhongKhamContext _context;

        public AuditLogDAL(QuanLyPhongKhamContext context)
        {
            _context = context;
        }

        public async Task GhiLogAsync(LichSuHoatDong log)
        {
            log.DongThoiGian = DateTime.Now;
            _context.LichSuHoatDong.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LichSuHoatDong>> LayTatCaAsync()
        {
            return await _context.LichSuHoatDong
                .OrderByDescending(l => l.DongThoiGian)
                .ToListAsync();
        }
    }
}
