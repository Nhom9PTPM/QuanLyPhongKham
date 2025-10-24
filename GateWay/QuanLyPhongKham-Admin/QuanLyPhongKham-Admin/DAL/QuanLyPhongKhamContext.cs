using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;

namespace QuanLyPhongKham_Admin.DAL
{
    public class QuanLyPhongKhamContext : DbContext
    {
        public QuanLyPhongKhamContext(DbContextOptions<QuanLyPhongKhamContext> options) : base(options) { }

        public DbSet<BenhNhan> BenhNhan { get; set; }
        public DbSet<HoSoBenhAn> HoSoBenhAn { get; set; }
        public DbSet<TapTin> TapTin { get; set; }

        // 🔽 Thêm vào đây
        public DbSet<KhamBenh> KhamBenh { get; set; }
        public DbSet<DonThuoc> DonThuoc { get; set; }
        public DbSet<ChiTietDonThuoc> ChiTietDonThuoc { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
