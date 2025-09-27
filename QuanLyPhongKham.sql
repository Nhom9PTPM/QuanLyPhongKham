CREATE DATABASE QuanLyPhongKham;
GO

USE QuanLyPhongKham;
GO

-- 1. Bảng lưu thông tin bệnh nhân
CREATE TABLE BenhNhan (
    maBenhNhan INT PRIMARY KEY IDENTITY(1,1),
    hoTen NVARCHAR(100) NOT NULL,
    ngaySinh DATE,
    gioiTinh NVARCHAR(10),
    diaChi NVARCHAR(200),
    soDienThoai NVARCHAR(15),
    email NVARCHAR(100),
    tienSuBenh NVARCHAR(MAX)
);

-- 2. Bảng lưu thông tin bác sĩ
CREATE TABLE BacSi (
    maBacSi INT PRIMARY KEY IDENTITY(1,1),
    hoTen NVARCHAR(100) NOT NULL,
    chuyenKhoa NVARCHAR(100),
    soDienThoai NVARCHAR(15),
    email NVARCHAR(100)
);

-- 3. Bảng lịch hẹn
CREATE TABLE LichHen (
    maLichHen INT PRIMARY KEY IDENTITY(1,1),
    maBenhNhan INT NOT NULL,
    maBacSi INT NOT NULL,
    ngayHen DATE NOT NULL,
    gioHen TIME NOT NULL,
    trangThai NVARCHAR(50),
    FOREIGN KEY (maBenhNhan) REFERENCES BenhNhan(maBenhNhan),
    FOREIGN KEY (maBacSi) REFERENCES BacSi(maBacSi)
);

-- 4. Bảng nhắc lịch (do lễ tân thực hiện)
CREATE TABLE NhacLich (
    maNhacLich INT PRIMARY KEY IDENTITY(1,1),
    maLichHen INT NOT NULL,
    ngayGioGui DATETIME,
    hinhThuc NVARCHAR(50), -- Điện thoại, SMS, Email
    ghiChu NVARCHAR(200),
    FOREIGN KEY (maLichHen) REFERENCES LichHen(maLichHen)
);

-- 5. Bảng hồ sơ bệnh án
CREATE TABLE HoSoBenhAn (
    maHoSo INT PRIMARY KEY IDENTITY(1,1),
    maBenhNhan INT NOT NULL,
    maBacSi INT NOT NULL,
    ngayKham DATE NOT NULL,
    chuanDoanChinh NVARCHAR(255),
    ghiChu NVARCHAR(255),
    FOREIGN KEY (maBenhNhan) REFERENCES BenhNhan(maBenhNhan),
    FOREIGN KEY (maBacSi) REFERENCES BacSi(maBacSi)
);

-- 6. Bảng chẩn đoán
CREATE TABLE ChanDoan (
    maChanDoan INT PRIMARY KEY IDENTITY(1,1),
    maHoSo INT NOT NULL,
    moTa NVARCHAR(255),
    loai NVARCHAR(50), -- Chính / Phụ
    FOREIGN KEY (maHoSo) REFERENCES HoSoBenhAn(maHoSo)
);

-- 7. Bảng kết quả cận lâm sàng
CREATE TABLE CanLamSang (
    maCLS INT PRIMARY KEY IDENTITY(1,1),
    maHoSo INT NOT NULL,
    tenXetNghiem NVARCHAR(100),
    fileDinhKem NVARCHAR(255), -- đường dẫn file
    ngayTao DATE,
    FOREIGN KEY (maHoSo) REFERENCES HoSoBenhAn(maHoSo)
);

-- 8. Bảng thuốc
CREATE TABLE Thuoc (
    maThuoc INT PRIMARY KEY IDENTITY(1,1),
    tenThuoc NVARCHAR(100),
    donVi NVARCHAR(50),
    hamLuong NVARCHAR(50),
    congDung NVARCHAR(255),
    gia DECIMAL(12,2)
);

-- 9. Bảng tồn kho thuốc
CREATE TABLE TonKhoThuoc (
    maThuoc INT NOT NULL,
    soLuong INT,
    hanSuDung DATE,
    PRIMARY KEY (maThuoc, hanSuDung),
    FOREIGN KEY (maThuoc) REFERENCES Thuoc(maThuoc)
);

-- 10. Bảng nhà cung cấp thuốc
CREATE TABLE NhaCungCap (
    maNCC INT PRIMARY KEY IDENTITY(1,1),
    tenNCC NVARCHAR(100),
    diaChi NVARCHAR(200),
    soDienThoai NVARCHAR(15)
);

-- 11. Bảng đơn thuốc
CREATE TABLE DonThuoc (
    maDonThuoc INT PRIMARY KEY IDENTITY(1,1),
    maHoSo INT NOT NULL,
    ngayKe DATE,
    ghiChu NVARCHAR(255),
    FOREIGN KEY (maHoSo) REFERENCES HoSoBenhAn(maHoSo)
);

-- 12. Bảng chi tiết đơn thuốc
CREATE TABLE ChiTietDonThuoc (
    maDonThuoc INT NOT NULL,
    maThuoc INT NOT NULL,
    soLuong INT,
    lieuDung NVARCHAR(200),
    PRIMARY KEY (maDonThuoc, maThuoc),
    FOREIGN KEY (maDonThuoc) REFERENCES DonThuoc(maDonThuoc),
    FOREIGN KEY (maThuoc) REFERENCES Thuoc(maThuoc)
);

-- 13. Bảng hóa đơn
CREATE TABLE HoaDon (
    maHoaDon INT PRIMARY KEY IDENTITY(1,1),
    maHoSo INT NOT NULL,
    soTien DECIMAL(12,2),
    ngayLap DATE,
    hinhThucThanhToan NVARCHAR(50),
    trangThai NVARCHAR(50),
    FOREIGN KEY (maHoSo) REFERENCES HoSoBenhAn(maHoSo)
);

-- 14. Bảng tài khoản hệ thống
CREATE TABLE TaiKhoan (
    maTaiKhoan INT PRIMARY KEY IDENTITY(1,1),
    tenDangNhap VARCHAR(50) UNIQUE,
    matKhau NVARCHAR(255),
    vaiTro NVARCHAR(20), -- Admin, BacSi, DuocSi, LeTan, BenhNhan
    lanDangNhapCuoi DATETIME
);


USE QuanLyPhongKham;
GO

-- 1. Bệnh nhân
INSERT INTO BenhNhan (hoTen, ngaySinh, gioiTinh, diaChi, soDienThoai, email, tienSuBenh) VALUES
(N'Nguyễn Văn A', '1990-05-12', N'Nam', N'Hà Nội', '0912345678', 'a@gmail.com', N'Tiểu đường'),
(N'Trần Thị B', '1985-10-20', N'Nữ', N'Hải Phòng', '0987654321', 'b@gmail.com', N'Huyết áp cao');

-- 2. Bác sĩ
INSERT INTO BacSi (hoTen, chuyenKhoa, soDienThoai, email) VALUES
(N'BS. Phạm Văn C', N'Nội tổng quát', '0901122334', 'bs.c@gmail.com'),
(N'BS. Lê Thị D', N'Nhi khoa', '0911222333', 'bs.d@gmail.com');

-- 3. Lịch hẹn
INSERT INTO LichHen (maBenhNhan, maBacSi, ngayHen, gioHen, trangThai) VALUES
(1, 1, '2025-09-25', '08:00', N'Đã đặt'),
(2, 2, '2025-09-26', '09:30', N'Chờ xác nhận');

-- 4. Nhắc lịch
INSERT INTO NhacLich (maLichHen, ngayGioGui, hinhThuc, ghiChu) VALUES
(1, '2025-09-24 10:00:00', N'SMS', N'Nhắc lịch khám ngày mai'),
(2, '2025-09-25 15:00:00', N'Email', N'Nhắc lịch khám');

-- 5. Hồ sơ bệnh án
INSERT INTO HoSoBenhAn (maBenhNhan, maBacSi, ngayKham, chuanDoanChinh, ghiChu) VALUES
(1, 1, '2025-09-20', N'Viêm họng cấp', N'Sốt nhẹ, ho nhiều'),
(2, 2, '2025-09-21', N'Sốt virus', N'Theo dõi thêm 2 ngày');

-- 6. Chẩn đoán
INSERT INTO ChanDoan (maHoSo, moTa, loai) VALUES
(1, N'Viêm họng', N'Chính'),
(1, N'Sốt nhẹ', N'Phụ'),
(2, N'Sốt virus', N'Chính');

-- 7. Cận lâm sàng
INSERT INTO CanLamSang (maHoSo, tenXetNghiem, fileDinhKem, ngayTao) VALUES
(1, N'Xét nghiệm máu', '/files/xn1.pdf', '2025-09-20'),
(2, N'X-quang phổi', '/files/xq2.jpg', '2025-09-21');

-- 8. Thuốc
INSERT INTO Thuoc (tenThuoc, donVi, hamLuong, congDung, gia) VALUES
(N'Paracetamol', N'Viên', N'500mg', N'Hạ sốt, giảm đau', 2000),
(N'Amoxicillin', N'Viên', N'500mg', N'Kháng sinh', 5000);

-- 9. Tồn kho thuốc
INSERT INTO TonKhoThuoc (maThuoc, soLuong, hanSuDung) VALUES
(1, 100, '2026-12-31'),
(2, 50, '2025-06-30');

-- 10. Nhà cung cấp
INSERT INTO NhaCungCap (tenNCC, diaChi, soDienThoai) VALUES
(N'Công ty Dược Việt Nam', N'Hà Nội', '0241234567'),
(N'Nhà thuốc Trung ương', N'Hồ Chí Minh', '0287654321');

-- 11. Đơn thuốc
INSERT INTO DonThuoc (maHoSo, ngayKe, ghiChu) VALUES
(1, '2025-09-20', N'Điều trị viêm họng'),
(2, '2025-09-21', N'Hạ sốt và nghỉ ngơi');

-- 12. Chi tiết đơn thuốc
INSERT INTO ChiTietDonThuoc (maDonThuoc, maThuoc, soLuong, lieuDung) VALUES
(1, 1, 10, N'Uống ngày 2 lần sau ăn'),
(1, 2, 7, N'Uống ngày 3 lần'),
(2, 1, 5, N'Uống khi sốt trên 38 độ');

-- 13. Hóa đơn
INSERT INTO HoaDon (maHoSo, soTien, ngayLap, hinhThucThanhToan, trangThai) VALUES
(1, 150000, '2025-09-20', N'Tiền mặt', N'Đã thanh toán'),
(2, 250000, '2025-09-21', N'Chuyển khoản', N'Chưa thanh toán');

-- 14. Tài khoản
INSERT INTO TaiKhoan (tenDangNhap, matKhau, vaiTro, lanDangNhapCuoi) VALUES
('admin', '123456', N'Admin', NULL),
('bacsi1', '123456', N'BacSi', NULL),
('duocsi1', '123456', N'DuocSi', NULL),
('letan1', '123456', N'LeTan', NULL),
('benhnhan1', '123456', N'BenhNhan', NULL);
