using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace QuanLyPhongKham.Models
{
    public class QuanLyPhongKhamContext : DbContext
    {
        public DbSet<BenhNhan> BenhNhans { get; set; }
        public DbSet<LichHen> LichHens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QL_PhongKham;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
