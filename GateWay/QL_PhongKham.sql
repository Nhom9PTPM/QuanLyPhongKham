﻿IF EXISTS(SELECT * FROM sys.databases WHERE name = N'QL_PhongKham')
BEGIN
    ALTER DATABASE [QL_PhongKham] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [QL_PhongKham];
END
CREATE DATABASE [QL_PhongKham];
GO
USE [QL_PhongKham];
GO


-- QL_PhongKham_FULL.sql
-- Phiên bản: Hệ thống Quản lý Phòng Khám (phiên bản chuẩn, có dữ liệu mẫu)

SET NOCOUNT ON;
GO

CREATE TABLE dbo.VaiTro
(
    MaVaiTro INT IDENTITY(1,1) PRIMARY KEY,
    TenVaiTro NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(250) NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    DaXoa BIT NOT NULL DEFAULT 0
);
GO

CREATE TABLE dbo.NguoiDung
(
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(200) NOT NULL,
    GioiTinh NVARCHAR(10) NULL,
    NgaySinh DATE NULL,
    SoDienThoai NVARCHAR(20) NULL,
    Email NVARCHAR(150) NULL,
    DiaChi NVARCHAR(300) NULL,
    AnhDaiDien NVARCHAR(300) NULL,
    LoaiNguoiDung NVARCHAR(30) NULL,
    GhiChu NVARCHAR(500) NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    DaXoa BIT NOT NULL DEFAULT 0
);
GO

CREATE TABLE dbo.TaiKhoan
(
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(100) NOT NULL UNIQUE,
    MatKhau NVARCHAR(200) NOT NULL,
    MaNguoiDung INT NOT NULL,
    MaVaiTro INT NULL,
    LoaiQuyen NVARCHAR(50) NULL,
    TrangThai BIT NOT NULL DEFAULT 1,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_TaiKhoan_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES dbo.NguoiDung(MaNguoiDung),
    CONSTRAINT FK_TaiKhoan_VaiTro FOREIGN KEY (MaVaiTro) REFERENCES dbo.VaiTro(MaVaiTro)
);
GO

CREATE TABLE dbo.BacSi
(
    MaBacSi INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NULL,
    ChuyenKhoa NVARCHAR(200) NULL,
    BangCap NVARCHAR(500) NULL,
    KinhNghiem NVARCHAR(500) NULL,
    SoPhong NVARCHAR(50) NULL,
    TrangThai BIT NOT NULL DEFAULT 1,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_BacSi_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES dbo.NguoiDung(MaNguoiDung)
);
GO

CREATE TABLE dbo.BenhNhan
(
    MaBenhNhan INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(200) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(10) NULL,
    SoDienThoai NVARCHAR(20) NULL,
    Email NVARCHAR(150) NULL,
    DiaChi NVARCHAR(300) NULL,
    CMTND_NV NVARCHAR(50) NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    DaXoa BIT NOT NULL DEFAULT 0
);
GO

CREATE TABLE dbo.NhaCungCap
(
    MaNhaCungCap INT IDENTITY(1,1) PRIMARY KEY,
    TenNhaCungCap NVARCHAR(250) NOT NULL,
    DiaChi NVARCHAR(300) NULL,
    SoDienThoai NVARCHAR(50) NULL,
    Email NVARCHAR(150) NULL,
    NguoiLienHe NVARCHAR(200) NULL,
    GhiChu NVARCHAR(500) NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    DaXoa BIT NOT NULL DEFAULT 0
);
GO

CREATE TABLE dbo.Thuoc
(
    MaThuoc INT IDENTITY(1,1) PRIMARY KEY,
    TenThuoc NVARCHAR(250) NOT NULL,
    MaNhaCungCap INT NULL,
    DonViTinh NVARCHAR(50) NULL,
    MoTa NVARCHAR(1000) NULL,
    Gia FLOAT NULL,
    TrangThai BIT NOT NULL DEFAULT 1,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Thuoc_NhaCungCap FOREIGN KEY (MaNhaCungCap) REFERENCES dbo.NhaCungCap(MaNhaCungCap)
);
GO

CREATE TABLE dbo.TonKhoThuoc
(
    MaTonKho INT IDENTITY(1,1) PRIMARY KEY,
    MaThuoc INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 0,
    MaKho NVARCHAR(50) NULL,
    NgayCapNhat DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_TonKho_Thuoc FOREIGN KEY (MaThuoc) REFERENCES dbo.Thuoc(MaThuoc)
);
GO

CREATE TABLE dbo.LichHen
(
    MaLichHen INT IDENTITY(1,1) PRIMARY KEY,
    MaBenhNhan INT NOT NULL,
    MaBacSi INT NULL,
    NgayBatDau DATETIME NOT NULL,
    NgayKetThuc DATETIME NULL,
    GioHen NVARCHAR(50) NULL,
    TrangThai NVARCHAR(50) NULL DEFAULT 'DaDat',
    GhiChu NVARCHAR(500) NULL,
    NguoiTao NVARCHAR(150) NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    DaXoa BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_LichHen_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan),
    CONSTRAINT FK_LichHen_BacSi FOREIGN KEY (MaBacSi) REFERENCES dbo.BacSi(MaBacSi)
);
GO

CREATE TABLE dbo.HoSoBenhAn
(
    MaHoSo INT IDENTITY(1,1) PRIMARY KEY,
    MaBenhNhan INT NOT NULL,
    TomTatBenhLy NVARCHAR(MAX) NULL,
    ChanDoanChinh NVARCHAR(500) NULL,
    LichSuBenhLy NVARCHAR(MAX) NULL,
    TapTinDinhKem NVARCHAR(500) NULL,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    NguoiLap NVARCHAR(150) NULL,
    DaXoa BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_HoSo_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan)
);
GO

CREATE TABLE dbo.KhamBenh
(
    MaKham INT IDENTITY(1,1) PRIMARY KEY,
    MaHoSo INT NULL,
    MaBenhNhan INT NOT NULL,
    MaBacSi INT NULL,
    NgayKham DATETIME NOT NULL,
    ChanDoan NVARCHAR(1000) NULL,
    ChiDinhXN NVARCHAR(1000) NULL,
    ChiDinhCLS NVARCHAR(1000) NULL,
    GhiChu NVARCHAR(1000) NULL,
    NguoiThucHien NVARCHAR(150) NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    DaXoa BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Kham_HoSo FOREIGN KEY (MaHoSo) REFERENCES dbo.HoSoBenhAn(MaHoSo),
    CONSTRAINT FK_Kham_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan),
    CONSTRAINT FK_Kham_BacSi FOREIGN KEY (MaBacSi) REFERENCES dbo.BacSi(MaBacSi)
);
GO

CREATE TABLE dbo.DonThuoc
(
    MaDonThuoc INT IDENTITY(1,1) PRIMARY KEY,
    MaKham INT NULL,
    MaBacSi INT NULL,
    NgayKe DATETIME NOT NULL DEFAULT GETDATE(),
    GhiChu NVARCHAR(1000) NULL,
    NguoiKe NVARCHAR(150) NULL,
    DaXoa BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_DonThuoc_Kham FOREIGN KEY (MaKham) REFERENCES dbo.KhamBenh(MaKham),
    CONSTRAINT FK_DonThuoc_BacSi FOREIGN KEY (MaBacSi) REFERENCES dbo.BacSi(MaBacSi)
);
GO

CREATE TABLE dbo.ChiTietDonThuoc
(
    MaChiTietDon INT IDENTITY(1,1) PRIMARY KEY,
    MaDonThuoc INT NOT NULL,
    MaThuoc INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 1,
    CachDung NVARCHAR(500) NULL,
    DonGia FLOAT NULL,
    CONSTRAINT FK_ChiTietDonThuoc_DonThuoc FOREIGN KEY (MaDonThuoc) REFERENCES dbo.DonThuoc(MaDonThuoc),
    CONSTRAINT FK_ChiTietDonThuoc_Thuoc FOREIGN KEY (MaThuoc) REFERENCES dbo.Thuoc(MaThuoc)
);
GO

CREATE TABLE dbo.HoaDon
(
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaBenhNhan INT NULL,
    MaNguoiThu INT NULL,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien FLOAT NOT NULL DEFAULT 0,
    PhuongThucThanhToan NVARCHAR(50) NULL,
    TrangThai NVARCHAR(50) NULL,
    GhiChu NVARCHAR(500) NULL,
    CONSTRAINT FK_HoaDon_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan)
);
GO

CREATE TABLE dbo.ChiTietHoaDon
(
    MaChiTietHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon INT NOT NULL,
    MaDichVu NVARCHAR(100) NULL,
    MaThuoc INT NULL,
    SoLuong INT NULL,
    DonGia FLOAT NULL,
    CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES dbo.HoaDon(MaHoaDon),
    CONSTRAINT FK_ChiTietHoaDon_Thuoc FOREIGN KEY (MaThuoc) REFERENCES dbo.Thuoc(MaThuoc)
);
GO

CREATE TABLE dbo.ThongBao
(
    MaThongBao INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiKhoan INT NULL,
    TieuDe NVARCHAR(250) NOT NULL,
    NoiDung NVARCHAR(MAX) NULL,
    DaDoc BIT NOT NULL DEFAULT 0,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ThongBao_TaiKhoan FOREIGN KEY (MaTaiKhoan) REFERENCES dbo.TaiKhoan(MaTaiKhoan)
);
GO

CREATE TABLE dbo.TapTin
(
    MaTapTin INT IDENTITY(1,1) PRIMARY KEY,
    TenTapTin NVARCHAR(300) NOT NULL,
    DuongDan NVARCHAR(500) NOT NULL,
    KichThuoc BIGINT NULL,
    DinhDang NVARCHAR(50) NULL,
    MaHoSo INT NULL,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_TapTin_HoSo FOREIGN KEY (MaHoSo) REFERENCES dbo.HoSoBenhAn(MaHoSo)
);
GO

CREATE TABLE dbo.LichSuHoatDong
(
    MaLichSu INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiKhoan INT NULL,
    HanhDong NVARCHAR(250) NOT NULL,
    DongThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
    DiaChiIP NVARCHAR(50) NULL,
    ChiTiet NVARCHAR(MAX) NULL
);
GO

CREATE INDEX IDX_Thuoc_TenThuoc ON dbo.Thuoc(TenThuoc);
CREATE INDEX IDX_BenhNhan_Ten ON dbo.BenhNhan(HoTen);
CREATE INDEX IDX_LichHen_Ngay ON dbo.LichHen(NgayBatDau);
CREATE INDEX IDX_HoSoBenhAn_BenhNhan ON dbo.HoSoBenhAn(MaBenhNhan);
CREATE INDEX IDX_TaiKhoan_TenDangNhap ON dbo.TaiKhoan(TenDangNhap);
GO

-- Dữ liệu mẫu: Vai trò
INSERT INTO dbo.VaiTro (TenVaiTro, MoTa) VALUES 
('Admin','Quản trị hệ thống'),
('BacSi','Bác sĩ khám chữa'),
('NhanVien','Nhân viên lễ tân'),
('BenhNhan','Bệnh nhân');
GO

-- Dữ liệu mẫu: NguoiDung
INSERT INTO dbo.NguoiDung (HoTen, GioiTinh, NgaySinh, SoDienThoai, Email, DiaChi, LoaiNguoiDung)
VALUES
('Nguyen Van A','Nam','1980-05-12','0123456789','a@example.com','Hanoi','BacSi'),
('Tran Thi B','Nu','1990-03-21','0987654321','b@example.com','Hanoi','NhanVien'),
('Le Van C','Nam','1975-11-01','0912345678','c@example.com','Hanoi','Admin'),
('Pham Thi D','Nu','1995-07-15','0900111222','d@example.com','Hanoi','BenhNhan');
GO

-- Dữ liệu mẫu: TaiKhoan (MatKhau là placeholder, trong thực tế hash)
INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, MaNguoiDung, MaVaiTro, LoaiQuyen)
VALUES
('admin','admin123',3,1,'Admin'),
('bacsi_a','bacsi123',1,2,'BacSi'),
('nhanvien_b','nv123',2,3,'NhanVien'),
('benhnhan_d','bn123',4,4,'BenhNhan');
GO

-- Dữ liệu mẫu: BacSi
INSERT INTO dbo.BacSi (MaNguoiDung, ChuyenKhoa, BangCap, KinhNghiem, SoPhong)
VALUES
(1,'Nhi','Bác sĩ chuyên khoa nhi','10 năm','101');
GO

-- Dữ liệu mẫu: BenhNhan (thêm một vài bản ghi)
INSERT INTO dbo.BenhNhan (HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, CMTND_NV)
VALUES
('Pham Van E','1988-02-02','Nam','0977555444','e@example.com','Hanoi','123456789'),
('Ho Thi F','1992-09-09','Nu','0966333222','f@example.com','Hanoi','987654321');
GO

-- Dữ liệu mẫu: NhaCungCap
INSERT INTO dbo.NhaCungCap (TenNhaCungCap, DiaChi, SoDienThoai, Email, NguoiLienHe)
VALUES
('Cty Duoc A','Hanoi','0241234567','supplierA@example.com','Nguyen A'),
('Cty Duoc B','Hanoi','0247654321','supplierB@example.com','Tran B');
GO

-- Dữ liệu mẫu: Thuoc
INSERT INTO dbo.Thuoc (TenThuoc, MaNhaCungCap, DonViTinh, MoTa, Gia)
VALUES
('Paracetamol 500mg',1,'Viên','Hạ sốt, giảm đau',1000),
('Amoxicillin 500mg',2,'Viên','Kháng sinh phổ rộng',3000);
GO

-- Dữ liệu mẫu: TonKhoThuoc
INSERT INTO dbo.TonKhoThuoc (MaThuoc, SoLuong, MaKho)
VALUES
(1,100,'K1'),
(2,50,'K1');
GO

-- Dữ liệu mẫu: LichHen
INSERT INTO dbo.LichHen (MaBenhNhan, MaBacSi, NgayBatDau, GioHen, TrangThai, GhiChu, NguoiTao)
VALUES
(1,1,DATEADD(day,3,GETDATE()),'09:00','DaDat','Lịch hẹn khám nhi','nhanvien_b'),
(2,1,DATEADD(day,5,GETDATE()),'10:30','DaDat','Kiểm tra sức khỏe','nhanvien_b');
GO

-- Dữ liệu mẫu: HoSoBenhAn
INSERT INTO dbo.HoSoBenhAn (MaBenhNhan, TomTatBenhLy, ChanDoanChinh, LichSuBenhLy, NguoiLap)
VALUES
(1,'Sốt cao, ho','Viêm phổi','Tiền sử dị ứng penicillin','bacsi_a'),
(2,'Đau họng, sốt nhẹ','Viêm họng','Không có','bacsi_a');
GO

-- Dữ liệu mẫu: KhamBenh
INSERT INTO dbo.KhamBenh (MaHoSo, MaBenhNhan, MaBacSi, NgayKham, ChanDoan, ChiDinhXN, GhiChu, NguoiThucHien)
VALUES
(1,1,1,GETDATE(),'Viêm phổi','X-quang ngực','Cần theo dõi','bacsi_a');
GO

-- Dữ liệu mẫu: DonThuoc và ChiTietDonThuoc
INSERT INTO dbo.DonThuoc (MaKham, MaBacSi, GhiChu, NguoiKe)
VALUES
(1,1,'Uống sau ăn','bacsi_a');
GO

INSERT INTO dbo.ChiTietDonThuoc (MaDonThuoc, MaThuoc, SoLuong, CachDung, DonGia)
VALUES
(1,1,10,'Uống 1 viên x 2 lần/ngày',1000),
(1,2,14,'Uống 1 viên x 3 lần/ngày',3000);
GO

-- Dữ liệu mẫu: HoaDon và ChiTietHoaDon
INSERT INTO dbo.HoaDon (MaBenhNhan, MaNguoiThu, TongTien, PhuongThucThanhToan, TrangThai)
VALUES
(1,3, (10*1000 + 14*3000), 'Tiền mặt', 'Da thanh toan');
GO

INSERT INTO dbo.ChiTietHoaDon (MaHoaDon, MaThuoc, SoLuong, DonGia)
VALUES
(1,1,10,1000),
(1,2,14,3000);
GO

-- (file gốc có thể còn các INSERT/seed khác; mình giữ nguyên các seed chính đã có)
