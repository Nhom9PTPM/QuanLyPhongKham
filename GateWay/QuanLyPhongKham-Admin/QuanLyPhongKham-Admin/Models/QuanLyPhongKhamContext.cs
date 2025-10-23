using Microsoft.EntityFrameworkCore;

namespace QuanLyPhongKham_Admin.Models
{
    public class QuanLyPhongKhamContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public QuanLyPhongKhamContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<BenhNhan> BenhNhans { get; set; }
        public DbSet<LichHen> LichHens { get; set; }

    }
}
