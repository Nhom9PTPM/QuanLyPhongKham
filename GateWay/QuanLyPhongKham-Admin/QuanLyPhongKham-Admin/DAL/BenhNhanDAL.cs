using QuanLyPhongKham_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace QuanLyPhongKham_Admin.DAL
{
    public class BenhNhanDAL
    {
        private readonly QuanLyPhongKhamContext _context;

        public BenhNhanDAL(QuanLyPhongKhamContext context)
        {
            _context = context;
        }

        // Lấy danh sách bệnh nhân
        public async Task<List<BenhNhan>> GetAllAsync()
        {
            return await _context.BenhNhan
                .Where(b => !b.DaXoa)
                .OrderByDescending(b => b.MaBenhNhan)
                .ToListAsync();
        }

        // Lấy bệnh nhân theo ID
        public async Task<BenhNhan?> GetByIdAsync(int id)
        {
            return await _context.BenhNhan
                .FirstOrDefaultAsync(b => b.MaBenhNhan == id && !b.DaXoa);
        }

        // Thêm bệnh nhân mới
        public async Task AddAsync(BenhNhan benhNhan)
        {
            benhNhan.NgayTao = DateTime.Now;
            _context.BenhNhan.Add(benhNhan);
            await _context.SaveChangesAsync();
        }

        // Cập nhật bệnh nhân
        public async Task UpdateAsync(BenhNhan benhNhan)
        {
            _context.BenhNhan.Update(benhNhan);
            await _context.SaveChangesAsync();
        }

        // Xóa mềm (DaXoa = true)
        public async Task DeleteAsync(int id)
        {
            var bn = await _context.BenhNhan.FindAsync(id);
            if (bn != null)
            {
                bn.DaXoa = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
