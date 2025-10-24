using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.DAL
{
    public class KhamBenhDAL
    {
        private readonly QuanLyPhongKhamContext _context;
        public KhamBenhDAL(QuanLyPhongKhamContext context)
        {
            _context = context;
        }

        public async Task<List<KhamBenh>> GetAllAsync()
        {
            return await _context.KhamBenh
                .Where(x => !x.DaXoa)
                .OrderByDescending(x => x.NgayKham)
                .ToListAsync();
        }

        public async Task<KhamBenh?> GetByIdAsync(int id)
        {
            return await _context.KhamBenh.FirstOrDefaultAsync(x => x.MaKham == id && !x.DaXoa);
        }

        public async Task AddAsync(KhamBenh kb)
        {
            kb.NgayTao = DateTime.Now;
            _context.KhamBenh.Add(kb);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(KhamBenh kb)
        {
            _context.KhamBenh.Update(kb);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var kb = await _context.KhamBenh.FindAsync(id);
            if (kb != null)
            {
                kb.DaXoa = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
