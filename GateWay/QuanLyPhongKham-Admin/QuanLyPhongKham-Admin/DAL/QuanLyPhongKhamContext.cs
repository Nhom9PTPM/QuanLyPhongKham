using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham_Admin.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace QuanLyPhongKham_Admin.DAL
{
    public class QuanLyPhongKhamContext : DbContext
    {
        public QuanLyPhongKhamContext(DbContextOptions<QuanLyPhongKhamContext> options)
            : base(options) { }

        public DbSet<BenhNhan> BenhNhan { get; set; }
        public DbSet<HoSoBenhAn> HoSoBenhAn { get; set; }
        public DbSet<TapTin> TapTin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
