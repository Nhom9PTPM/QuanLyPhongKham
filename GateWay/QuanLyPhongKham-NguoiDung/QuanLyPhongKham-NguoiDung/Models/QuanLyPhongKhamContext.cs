using Microsoft.EntityFrameworkCore;

namespace QuanLyPhongKham_NguoiDung.Models
{
    public class QuanLyPhongKhamContext : DbContext
    {
        private readonly IConfiguration _config;
        public QuanLyPhongKhamContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public DbSet<BenhNhan> BenhNhans { get; set; }
        public DbSet<LichHen> LichHens { get; set; }
        public DbSet<BacSi> BacSis { get; set; }
        
    }
}
