using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace QuanLyPhongKham_Admin.Models
{
    public partial class QuanLyPhongKhamContext : DbContext
    {
        private string connectstring;

        public QuanLyPhongKhamContext(IConfiguration configuration)
        {
            connectstring = configuration["ConnectionStrings:DefaultConnection"].ToString();
        }

        public QuanLyPhongKhamContext(DbContextOptions<QuanLyPhongKhamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<VaiTro> VaiTros { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public virtual DbSet<BacSi> BacSis { get; set; } = null!;
        public virtual DbSet<BenhNhan> BenhNhans { get; set; } = null!;
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
        public virtual DbSet<Thuoc> Thuocs { get; set; } = null!;
        public virtual DbSet<TonKhoThuoc> TonKhoThuocs { get; set; } = null!;
        public virtual DbSet<LichHen> LichHens { get; set; } = null!;
        public virtual DbSet<HoSoBenhAn> HoSoBenhAns { get; set; } = null!;
        public virtual DbSet<KhamBenh> KhamBenhs { get; set; } = null!;
        public virtual DbSet<DonThuoc> DonThuocs { get; set; } = null!;
        public virtual DbSet<ChiTietDonThuoc> ChiTietDonThuocs { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; } = null!;
        public virtual DbSet<ThongBao> ThongBaos { get; set; } = null!;
        public virtual DbSet<TapTin> TapTins { get; set; } = null!;
        public virtual DbSet<LichSuHoatDong> LichSuHoatDongs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectstring);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasKey(e => e.MaVaiTro);
                entity.Property(e => e.TenVaiTro).HasMaxLength(50);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNguoiDung);
                entity.Property(e => e.HoTen).HasMaxLength(200);
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTaiKhoan);

                entity.Property(e => e.TenDangNhap).HasMaxLength(100);
                entity.Property(e => e.MatKhau).HasMaxLength(200);

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.MaNguoiDung);

                entity.HasOne(d => d.MaVaiTroNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.MaVaiTro);
            });

            modelBuilder.Entity<BacSi>(entity =>
            {
                entity.HasKey(e => e.MaBacSi);

                entity.Property(e => e.ChuyenKhoa).HasMaxLength(200);

                entity.HasOne(d => d.MaNguoiDungNavigation)
                    .WithMany(p => p.BacSis)
                    .HasForeignKey(d => d.MaNguoiDung);
            });

            modelBuilder.Entity<BenhNhan>(entity =>
            {
                entity.HasKey(e => e.MaBenhNhan);
                entity.Property(e => e.HoTen).HasMaxLength(200);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNhaCungCap);
            });

            modelBuilder.Entity<Thuoc>(entity =>
            {
                entity.HasKey(e => e.MaThuoc);

                entity.HasOne(d => d.MaNhaCungCapNavigation)
                    .WithMany(p => p.Thuocs)
                    .HasForeignKey(d => d.MaNhaCungCap);
            });

            modelBuilder.Entity<TonKhoThuoc>(entity =>
            {
                entity.HasKey(e => e.MaTonKho);

                entity.HasOne(d => d.MaThuocNavigation)
                    .WithMany(p => p.TonKhoThuocs)
                    .HasForeignKey(d => d.MaThuoc);
            });

            modelBuilder.Entity<LichHen>(entity =>
            {
                entity.HasKey(e => e.MaLichHen);

                entity.HasOne(d => d.MaBacSiNavigation)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.MaBacSi);

                entity.HasOne(d => d.MaBenhNhanNavigation)
                    .WithMany(p => p.LichHens)
                    .HasForeignKey(d => d.MaBenhNhan);
            });

            modelBuilder.Entity<HoSoBenhAn>(entity =>
            {
                entity.HasKey(e => e.MaHoSo);

                entity.HasOne(d => d.MaBenhNhanNavigation)
                    .WithMany(p => p.HoSoBenhAns)
                    .HasForeignKey(d => d.MaBenhNhan);
            });

            modelBuilder.Entity<KhamBenh>(entity =>
            {
                entity.HasKey(e => e.MaKham);

                entity.HasOne(d => d.MaHoSoNavigation)
                    .WithMany(p => p.KhamBenhs)
                    .HasForeignKey(d => d.MaHoSo);

                entity.HasOne(d => d.MaBenhNhanNavigation)
                    .WithMany(p => p.KhamBenhs)
                    .HasForeignKey(d => d.MaBenhNhan);

                entity.HasOne(d => d.MaBacSiNavigation)
                    .WithMany(p => p.KhamBenhs)
                    .HasForeignKey(d => d.MaBacSi);
            });

            modelBuilder.Entity<DonThuoc>(entity =>
            {
                entity.HasKey(e => e.MaDonThuoc);

                entity.HasOne(d => d.MaKhamNavigation)
                    .WithMany(p => p.DonThuocs)
                    .HasForeignKey(d => d.MaKham);
            });

            modelBuilder.Entity<ChiTietDonThuoc>(entity =>
            {
                entity.HasKey(e => e.MaChiTietDon);

                entity.HasOne(d => d.MaDonThuocNavigation)
                    .WithMany(p => p.ChiTietDonThuocs)
                    .HasForeignKey(d => d.MaDonThuoc);

                entity.HasOne(d => d.MaThuocNavigation)
                    .WithMany(p => p.ChiTietDonThuocs)
                    .HasForeignKey(d => d.MaThuoc);
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon);

                entity.HasOne(d => d.MaBenhNhanNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaBenhNhan);
            });

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => e.MaChiTietHoaDon);

                entity.HasOne(d => d.MaHoaDonNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaHoaDon);

                entity.HasOne(d => d.MaThuocNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaThuoc);
            });

            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => e.MaThongBao);

                entity.HasOne(d => d.MaTaiKhoanNavigation)
                    .WithMany(p => p.ThongBaos)
                    .HasForeignKey(d => d.MaTaiKhoan);
            });

            modelBuilder.Entity<TapTin>(entity =>
            {
                entity.HasKey(e => e.MaTapTin);

                entity.HasOne(d => d.MaHoSoNavigation)
                    .WithMany(p => p.TapTins)
                    .HasForeignKey(d => d.MaHoSo);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
