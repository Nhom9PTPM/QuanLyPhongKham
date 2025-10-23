using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace QuanLyPhongKham.Models
{
    public class QuanLyPhongKhamContext : DbContext
    {
        private readonly IConfiguration? _configuration;

        public QuanLyPhongKhamContext() { }

        public QuanLyPhongKhamContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public QuanLyPhongKhamContext(DbContextOptions<QuanLyPhongKhamContext> options) : base(options) { }

        public DbSet<VaiTro> VaiTro { get; set; }
        public DbSet<NguoiDung> NguoiDung { get; set; }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<BacSi> BacSi { get; set; }
        public DbSet<BenhNhan> BenhNhan { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<Thuoc> Thuoc { get; set; }
        public DbSet<TonKhoThuoc> TonKhoThuoc { get; set; }
        public DbSet<LichHen> LichHen { get; set; }
        public DbSet<HoSoBenhAn> HoSoBenhAn { get; set; }
        public DbSet<KhamBenh> KhamBenh { get; set; }
        public DbSet<DonThuoc> DonThuoc { get; set; }
        public DbSet<ChiTietDonThuoc> ChiTietDonThuoc { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<ThongBao> ThongBao { get; set; }
        public DbSet<TapTin> TapTin { get; set; }
        public DbSet<LichSuHoatDong> LichSuHoatDong { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration != null)
            {
                var conn = _configuration.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrWhiteSpace(conn))
                {
                    optionsBuilder.UseSqlServer(conn);
                    return;
                }
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=QL_PhongKham;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VaiTro>().ToTable("VaiTro");
            modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");
            modelBuilder.Entity<TaiKhoan>().ToTable("TaiKhoan");
            modelBuilder.Entity<BacSi>().ToTable("BacSi");
            modelBuilder.Entity<BenhNhan>().ToTable("BenhNhan");
            modelBuilder.Entity<NhaCungCap>().ToTable("NhaCungCap");
            modelBuilder.Entity<Thuoc>().ToTable("Thuoc");
            modelBuilder.Entity<TonKhoThuoc>().ToTable("TonKhoThuoc");
            modelBuilder.Entity<LichHen>().ToTable("LichHen");
            modelBuilder.Entity<HoSoBenhAn>().ToTable("HoSoBenhAn");
            modelBuilder.Entity<KhamBenh>().ToTable("KhamBenh");
            modelBuilder.Entity<DonThuoc>().ToTable("DonThuoc");
            modelBuilder.Entity<ChiTietDonThuoc>().ToTable("ChiTietDonThuoc");
            modelBuilder.Entity<HoaDon>().ToTable("HoaDon");
            modelBuilder.Entity<ChiTietHoaDon>().ToTable("ChiTietHoaDon");
            modelBuilder.Entity<ThongBao>().ToTable("ThongBao");
            modelBuilder.Entity<TapTin>().ToTable("TapTin");
            modelBuilder.Entity<LichSuHoatDong>().ToTable("LichSuHoatDong");

            modelBuilder.Entity<TaiKhoan>()
                .HasIndex(t => t.TenDangNhap)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
