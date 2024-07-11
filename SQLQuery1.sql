﻿CREATE TABLE Users
(
  maUser INT IDENTITY (1, 1) NOT NULL,
  SDT VARCHAR(15) NOT NULL,
  IsAdmin BIT NOT NULL,
  TenTK NVARCHAR(100) NOT NULL,
  Pass VARCHAR(20) NOT NULL,
  Email VARCHAR(100) NOT NULL,
  PRIMARY KEY (maUser)
);

CREATE TABLE ThuongHieuSP
(
  MaTH CHAR(5) NOT NULL,
  TenTH NVARCHAR(50) NOT NULL,
  Logo VARCHAR(100) NOT NULL,
  PRIMARY KEY (MaTH)
);

CREATE TABLE Category
(
  CatID CHAR(5) NOT NULL,
  NameCate NVARCHAR(100) NOT NULL,
  PRIMARY KEY (CatID)
);

CREATE TABLE KichCo
(
  maSize CHAR(5) NOT NULL,
  size INT NOT NULL,
  PRIMARY KEY (maSize)
);

CREATE TABLE DanhGia
(
  maDanhgia INT IDENTITY (1, 1) NOT NULL,
  diemDanhgia INT NULL,
  comment NVARCHAR(100) NULL,
  maUser INT NOT NULL,
  PRIMARY KEY (maDanhgia),
  FOREIGN KEY (maUser) REFERENCES Users(maUser)
);

CREATE TABLE SanPham
(
  maSP CHAR(5) NOT NULL,
  TinhTrangSP NVARCHAR(20) NOT NULL,
  MauSac NVARCHAR(100) NOT NULL,
  HinhAnh1 NVARCHAR(MAX) NOT NULL,
  MoTaSP NVARCHAR(MAX) NOT NULL,
  GiaGoc Float NOT NULL,
  SLBan INT NOT NULL,
  HinhAnh2 NVARCHAR(MAX) NOT NULL,
  HinhAnh3 NVARCHAR(MAX) NOT NULL,
  HinhAnh4 NVARCHAR(MAX) NOT NULL,
  GiaGiam FLOAT  NULL,
  MaTH CHAR(5) NOT NULL,
  CatID CHAR(5) NOT NULL,
  maDanhgia INT NOT NULL,
  maSize CHAR(5) NOT NULL,
  PRIMARY KEY (maSP),
  FOREIGN KEY (MaTH) REFERENCES ThuongHieuSP(MaTH),
  FOREIGN KEY (CatID) REFERENCES Category(CatID),
  FOREIGN KEY (maDanhgia) REFERENCES DanhGia(maDanhgia),
  FOREIGN KEY (maSize) REFERENCES KichCo(maSize)
);

CREATE TABLE SPYeuThich
(
/*Tu Dong ghi maSPYT*/
  maSPYT INT IDENTITY (1, 1) Not null,
  maSP CHAR(5) NOT NULL,
  maUser INT NOT NULL,
  PRIMARY KEY (maSPYT),
  FOREIGN KEY (maSP) REFERENCES SanPham(maSP),
  FOREIGN KEY (maUser) REFERENCES Users(maUser)
);

CREATE TABLE HoaDon
(
  maHoaDon CHAR(5) NOT NULL,
  SoLuong INT NOT NULL,
  NgayMua DATE NOT NULL,
  TongTien FLOAT NOT NULL,
  maSP CHAR(5) NOT NULL,
  maUser INT NOT NULL,
  PRIMARY KEY (maHoaDon),
  FOREIGN KEY (maSP) REFERENCES SanPham(maSP),
  FOREIGN KEY (maUser) REFERENCES Users(maUser)
);

CREATE TABLE ChiTietHoaDon
(
  maCTHD CHAR(5) NOT NULL,
  NgayMua DATE NOT NULL,
  SDTKH VARCHAR(15) NOT NULL,
  DCGiaoHang VARCHAR(100) NOT NULL,
  TongTien FLOAT NOT NULL,
  DVT VARCHAR NOT NULL,
  TienGiam FLOAT NOT NULL,
  TrangThaiDH VARCHAR NOT NULL,
  tgGiaoHang VARCHAR NOT NULL,
  PhiVanChuyen FLOAT NOT NULL,
  maSP CHAR(5) NOT NULL,
  maHoaDon CHAR(5) NOT NULL,
  PRIMARY KEY (maCTHD),
  FOREIGN KEY (maSP) REFERENCES SanPham(maSP),
  FOREIGN KEY (maHoaDon) REFERENCES HoaDon(maHoaDon)
);