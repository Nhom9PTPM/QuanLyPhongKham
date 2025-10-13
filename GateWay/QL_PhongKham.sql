
IF EXISTS(SELECT * FROM sys.databases WHERE name = N'QL_PhongKham')
BEGIN
    ALTER DATABASE [QL_PhongKham] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [QL_PhongKham];
END
CREATE DATABASE [QL_PhongKham];
GO
USE [QL_PhongKham];
GO


CREATE TABLE VaiTro (
    MaVaiTro INT IDENTITY(1,1) PRIMARY KEY,
    TenVaiTro NVARCHAR(100) NOT NULL, 
    MoTa NVARCHAR(500) NULL
);
GO


CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(100) NOT NULL UNIQUE,
    MatKhauHash NVARCHAR(256) NOT NULL,
    Email NVARCHAR(150) NULL,
    MaVaiTro INT NOT NULL,
    TrangThai BIT NOT NULL DEFAULT 1,
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    NguoiTao NVARCHAR(150) NULL,
    DaXoa BIT NOT NULL DEFAULT 0,
    RowVersion ROWVERSION
);
ALTER TABLE TaiKhoan ADD CONSTRAINT FK_TaiKhoan_VaiTro FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro);
GO


CREATE TABLE BenhNhan (
    MaBenhNhan INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(250) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(20) NULL,
    SoDienThoai VARCHAR(20) NULL,
    Email NVARCHAR(150) NULL,
    DiaChi NVARCHAR(500) NULL,
    CMTND_NV VARCHAR(50) NULL, 
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    NguoiTao NVARCHAR(150) NULL,
    DaXoa BIT NOT NULL DEFAULT 0,
    RowVersion ROWVERSION
);
GO

CREATE TABLE LichHen (
    MaLichHen INT IDENTITY(1,1) PRIMARY KEY,
    MaBenhNhan INT NOT NULL,
    MaBacSi INT NULL, 
    NgayBatDau DATETIME2 NOT NULL,
    NgayKetThuc DATETIME2 NULL,
    GhiChu NVARCHAR(1000) NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT 'DaDat', 
    KieuHen NVARCHAR(50) NULL, 
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    NguoiTao NVARCHAR(150) NULL,
    DaXoa BIT NOT NULL DEFAULT 0,
    RowVersion ROWVERSION
);
ALTER TABLE LichHen ADD CONSTRAINT FK_LichHen_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan(MaBenhNhan);
ALTER TABLE LichHen ADD CONSTRAINT FK_LichHen_BacSi FOREIGN KEY (MaBacSi) REFERENCES TaiKhoan(MaTaiKhoan);
GO

CREATE TABLE HoSoBenhAn (
    MaHoSo INT IDENTITY(1,1) PRIMARY KEY,
    MaBenhNhan INT NOT NULL,
    NgayLap DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    TomTatBenhLy NVARCHAR(MAX) NULL,
    ChanDoanChinh NVARCHAR(500) NULL,
    LichSuBenhLy NVARCHAR(MAX) NULL,
    TapTinDinhKem NVARCHAR(1000) NULL, 
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    NguoiTao NVARCHAR(150) NULL,
    DaXoa BIT NOT NULL DEFAULT 0,
    RowVersion ROWVERSION
);
ALTER TABLE HoSoBenhAn ADD CONSTRAINT FK_HoSoBenhAn_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan(MaBenhNhan);
GO


CREATE TABLE KhamBenh (
    MaKham INT IDENTITY(1,1) PRIMARY KEY,
    MaHoSo INT NULL,
    MaBenhNhan INT NOT NULL,
    MaBacSi INT NULL,
    NgayKham DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    ChanDoan NVARCHAR(1000) NULL,
    ChiDinhXN NVARCHAR(1000) NULL, 
    ChiDinhCLS NVARCHAR(1000) NULL, 
    GhiChu NVARCHAR(2000) NULL,
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    NguoiTao NVARCHAR(150) NULL,
    DaXoa BIT NOT NULL DEFAULT 0,
    RowVersion ROWVERSION
);
ALTER TABLE KhamBenh ADD CONSTRAINT FK_KhamBenh_HoSo FOREIGN KEY (MaHoSo) REFERENCES HoSoBenhAn(MaHoSo);
ALTER TABLE KhamBenh ADD CONSTRAINT FK_KhamBenh_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan(MaBenhNhan);
ALTER TABLE KhamBenh ADD CONSTRAINT FK_KhamBenh_BacSi FOREIGN KEY (MaBacSi) REFERENCES TaiKhoan(MaTaiKhoan);
GO


CREATE TABLE ChanDoan (
    MaChanDoan INT IDENTITY(1,1) PRIMARY KEY,
    MaKham INT NOT NULL,
    TenChanDoan NVARCHAR(500) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    NguoiTao NVARCHAR(150) NULL,
    RowVersion ROWVERSION
);
ALTER TABLE ChanDoan ADD CONSTRAINT FK_ChanDoan_Kham FOREIGN KEY (MaKham) REFERENCES KhamBenh(MaKham);
GO


CREATE TABLE Thuoc (
    MaThuoc INT IDENTITY(1,1) PRIMARY KEY,
    TenThuoc NVARCHAR(250) NOT NULL,
    DonViTinh NVARCHAR(50) NULL,
    MoTa NVARCHAR(1000) NULL,
    TrangThai BIT NOT NULL DEFAULT 1,
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    RowVersion ROWVERSION
);
GO


CREATE TABLE DonThuoc (
    MaDonThuoc INT IDENTITY(1,1) PRIMARY KEY,
    MaKham INT NOT NULL,
    MaBacSi INT NULL,
    NgayKe DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    GhiChu NVARCHAR(1000) NULL,
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    NguoiTao NVARCHAR(150) NULL,
    DaXoa BIT NOT NULL DEFAULT 0,
    RowVersion ROWVERSION
);
ALTER TABLE DonThuoc ADD CONSTRAINT FK_DonThuoc_Kham FOREIGN KEY (MaKham) REFERENCES KhamBenh(MaKham);
ALTER TABLE DonThuoc ADD CONSTRAINT FK_DonThuoc_BacSi FOREIGN KEY (MaBacSi) REFERENCES TaiKhoan(MaTaiKhoan);
GO

CREATE TABLE ChiTietDonThuoc (
    MaChiTietDon INT IDENTITY(1,1) PRIMARY KEY,
    MaDonThuoc INT NOT NULL,
    MaThuoc INT NOT NULL,
    SoLuong INT NOT NULL,
    CachDung NVARCHAR(500) NULL,
    DonGia FLOAT NULL,
    RowVersion ROWVERSION
);
ALTER TABLE ChiTietDonThuoc ADD CONSTRAINT FK_CTDonThuoc_DonThuoc FOREIGN KEY (MaDonThuoc) REFERENCES DonThuoc(MaDonThuoc);
ALTER TABLE ChiTietDonThuoc ADD CONSTRAINT FK_CTDonThuoc_Thuoc FOREIGN KEY (MaThuoc) REFERENCES Thuoc(MaThuoc);
GO

CREATE TABLE TonKhoThuoc (
    MaTonKho INT IDENTITY(1,1) PRIMARY KEY,
    MaThuoc INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 0,
    MaKho NVARCHAR(100) NULL,
    NgayCapNhat DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    RowVersion ROWVERSION
);
ALTER TABLE TonKhoThuoc ADD CONSTRAINT FK_TonKhoThuoc_Thuoc FOREIGN KEY (MaThuoc) REFERENCES Thuoc(MaThuoc);
GO

CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaBenhNhan INT NULL,
    MaNguoiThu INT NULL, 
    NgayLap DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    TongTien FLOAT NOT NULL DEFAULT 0,
    PhuongThucThanhToan NVARCHAR(50) NULL,
    TrangThai NVARCHAR(50) NULL, 
    GhiChu NVARCHAR(1000) NULL,
    RowVersion ROWVERSION
);
ALTER TABLE HoaDon ADD CONSTRAINT FK_HoaDon_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan(MaBenhNhan);
ALTER TABLE HoaDon ADD CONSTRAINT FK_HoaDon_NguoiThu FOREIGN KEY (MaNguoiThu) REFERENCES TaiKhoan(MaTaiKhoan);
GO

CREATE TABLE ChiTietHoaDon (
    MaChiTietHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon INT NOT NULL,
    MaDichVu NVARCHAR(250) NULL, 
    MaThuoc INT NULL,
    SoLuong INT NULL,
    DonGia FLOAT NULL,
    ThanhTien AS (ISNULL(SoLuong,0) * ISNULL(DonGia,0)),
    RowVersion ROWVERSION
);
ALTER TABLE ChiTietHoaDon ADD CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon);
ALTER TABLE ChiTietHoaDon ADD CONSTRAINT FK_ChiTietHoaDon_Thuoc FOREIGN KEY (MaThuoc) REFERENCES Thuoc(MaThuoc);
GO

CREATE TABLE LichSuHoatDong (
    MaLichSu INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiKhoan INT NULL,
    HanhDong NVARCHAR(500) NOT NULL,
    DongThoiGian DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    DiaChiIP NVARCHAR(50) NULL,
    ChiTiet NVARCHAR(MAX) NULL
);
ALTER TABLE LichSuHoatDong ADD CONSTRAINT FK_LichSu_TaiKhoan FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan);
GO

CREATE TABLE ThongBao (
    MaThongBao INT IDENTITY(1,1) PRIMARY KEY,
    MaTaiKhoan INT NULL, -- nguoi nhan
    TieuDe NVARCHAR(250) NOT NULL,
    NoiDung NVARCHAR(MAX) NULL,
    DaDoc BIT NOT NULL DEFAULT 0,
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
ALTER TABLE ThongBao ADD CONSTRAINT FK_ThongBao_TaiKhoan FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan);
GO

CREATE TABLE TapTin (
    MaTapTin INT IDENTITY(1,1) PRIMARY KEY,
    TenTapTin NVARCHAR(250) NOT NULL,
    DuongDan NVARCHAR(1000) NOT NULL,
    KichThuoc BIGINT NULL,
    DinhDang NVARCHAR(50) NULL,
    MaHoSo INT NULL,
    NgayTao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
ALTER TABLE TapTin ADD CONSTRAINT FK_TapTin_HoSo FOREIGN KEY (MaHoSo) REFERENCES HoSoBenhAn(MaHoSo);
GO


CREATE NONCLUSTERED INDEX IX_LichHen_MaBenhNhan_NgayBatDau ON LichHen(MaBenhNhan, NgayBatDau);
CREATE NONCLUSTERED INDEX IX_BenhNhan_Ten ON BenhNhan(HoTen);
GO


INSERT INTO VaiTro (TenVaiTro, MoTa) VALUES
('Admin','Quan tri he thong'),
('Letan','Tiep nhan lich hen va thanh toan'),
('BacSi','Bac si kham benh'),
('DuocSi','Quan ly nha thuoc'),
('BenhNhan','Benh nhan');

INSERT INTO TaiKhoan (TenDangNhap, MatKhauHash, Email, MaVaiTro, NguoiTao)
VALUES
('admin', 'hash_password_admin', 'admin@phongkham.local', 1, 'system'),
('letan1', 'hash_pw_letan', 'letan@phongkham.local', 2, 'system'),
('bacsi1', 'hash_pw_bacsi', 'bacsi@phongkham.local', 3, 'system'),
('duocsi1', 'hash_pw_duocsi', 'duocsi@phongkham.local', 4, 'system');

INSERT INTO BenhNhan (HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, CMTND_NV, NguoiTao)
VALUES
('Nguyen Van A', '1985-03-10', 'Nam', '0909123456', 'nv.a@example.com', '123 Duong A, Q1', '012345678', 'letan1'),
('Tran Thi B', '1990-07-22', 'Nu', '0912345678', 'tt.b@example.com', '456 Duong B, Q2', '987654321', 'letan1');


INSERT INTO Thuoc (TenThuoc, DonViTinh, MoTa) VALUES
('Paracetamol 500mg', 'Vien', 'Giam dau ha sot'),
('Amoxicillin 500mg', 'Vien', 'Khang sinh');


INSERT INTO LichHen (MaBenhNhan, MaBacSi, NgayBatDau, GhiChu, NguoiTao)
VALUES
(1, 3, '2025-10-20 09:00', 'Kham tong quat', 'letan1'),
(2, 3, '2025-10-21 10:30', 'Kham tai', 'letan1');


INSERT INTO HoSoBenhAn (MaBenhNhan, TomTatBenhLy, LichSuBenhLy, NguoiTao)
VALUES (1, 'Tieu su: Tien su hen suyt', 'Khong co benh man tinh', 'bacsi1');

INSERT INTO KhamBenh (MaHoSo, MaBenhNhan, MaBacSi, ChanDoan, ChiDinhXN, GhiChu, NguoiTao)
VALUES (1, 1, 3, 'Viem hong', 'XN mau', 'Bao ve the luc', 'bacsi1');

INSERT INTO DonThuoc (MaKham, MaBacSi, GhiChu, NguoiTao)
VALUES (1, 3, 'Uong sau an', 'bacsi1');

INSERT INTO ChiTietDonThuoc (MaDonThuoc, MaThuoc, SoLuong, CachDung, DonGia)
VALUES (1, 1, 10, 'Uong 1 vien/sau an 8h', 1000),
       (1, 2, 20, 'Uong 1 vien/lan, 3 lan/ngay', 2000);


INSERT INTO HoaDon (MaBenhNhan, MaNguoiThu, TongTien, PhuongThucThanhToan, TrangThai)
VALUES (1, 2, 30000, 'TienMat', 'DaThanhToan');


