CREATE DATABASE OnlineHelpDesk;
GO

USE OnlineHelpDesk;
GO

-- 1. Tạo bảng
CREATE TABLE DoUuTien (
    madouutien INT PRIMARY KEY,
    tendouutien NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE NhanVien (
    username NVARCHAR(50) PRIMARY KEY,
    password NVARCHAR(255) NOT NULL,
    hoten NVARCHAR(255) NOT NULL,
    ngaysinh DATE,
    kichhoat BIT DEFAULT 1,
    hinhanh NVARCHAR(255),
    quyen INT CHECK (quyen IN (1, 2, 3)) NOT NULL
);
GO

CREATE TABLE YeuCau (
    mayeucau INT IDENTITY(1,1)  PRIMARY KEY,
    tieude NVARCHAR(255) NOT NULL,
    noidung TEXT NOT NULL,
    ngaygui DATE NOT NULL,
    madouutien INT,
    manv_gui NVARCHAR(50),
    manv_xuly NVARCHAR(50),
    FOREIGN KEY (madouutien) REFERENCES DoUuTien(madouutien),
    FOREIGN KEY (manv_gui) REFERENCES NhanVien(username),
    FOREIGN KEY (manv_xuly) REFERENCES NhanVien(username)
);
GO

-- 2. Thêm dữ liệu NhanVien
INSERT INTO NhanVien (username, password, hoten, ngaysinh, kichhoat, hinhanh, quyen)
VALUES 
(N'admin', N'123456', N'Quản trị hệ thống', '1990-01-01', 1, NULL, 3),
(N'nhanvien1', N'abc123', N'Nguyễn Văn A', '1995-05-15', 1, NULL, 1),
(N'support1', N'supportpwd', N'Lê Thị B', '1992-08-20', 1, NULL, 2);
GO

-- 3. Thêm dữ liệu DoUuTien
INSERT INTO DoUuTien (madouutien, tendouutien)
VALUES 
(1, N'Khẩn cấp'),
(2, N'Bình thường'),
(3, N'Thấp');
GO

-- Thêm yêu cầu do nhân viên 'nhanvien1' gửi
INSERT INTO YeuCau ( tieude, noidung, ngaygui, madouutien, manv_gui, manv_xuly)
VALUES 
(N'Lỗi đăng nhập hệ thống', N'Không thể đăng nhập vào hệ thống từ sáng nay.', '2025-05-01', 1, 'nhanvien1', NULL),
(N'Máy in không hoạt động', N'Máy in phòng kế toán không in được tài liệu.', '2025-05-03', 2, 'nhanvien1', 'support1'),
(N'Cập nhật phần mềm', N'Yêu cầu cập nhật phần mềm Zoom lên phiên bản mới nhất.', '2025-05-04', 3, 'nhanvien1', NULL),
(N'Lỗi mạng nội bộ', N'Không truy cập được file server nội bộ.', '2025-05-05', 1, 'nhanvien1', NULL),
(N'Không đăng nhập được Email', N'Mật khẩu Email không còn hoạt động.', '2025-05-06', 2, 'nhanvien1', 'support1');

