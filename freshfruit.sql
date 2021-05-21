
use master
GO

--DROP DATABASE BANHANG
/* ----------------------TAO CSDL---------------------------------*/
CREATE DATABASE Freshfruit
GO
/* ----------------------CHON CSDL--------------------------------*/
USE Freshfruit
GO
/* ----------------------TAO CAC TABLE----------------------------*/
	CREATE TABLE SANPHAM
	(
		MaSP	char(10)		NOT NULL,
		TenSP	nvarchar(20)			,
		DVTinh	nvarchar(10)			,
		DonGia	float				,
		GhiChu	char(50)		,
		NgayNH  datetime,
		NgayHH  datetime
	)
GO
	CREATE TABLE NHANVIEN
	(
		MaNV		char(10)				NOT NULL,
		TenNV		nvarchar(20)				,
		HoLot		nvarchar(50)				,
		Phai		bit							,
		DiaChi		nvarchar(max)				,
		DTNV		int							,
		NgaySinh	datetime						,
		NgayNV		datetime			
	)
GO
	CREATE TABLE HOADON
	(
		MaHD		char(10)			NOT NULL,
		MaKH		char(10)					,
		MaNV		char(10)					,
		NgayLapHD	datetime					,
		NgayGH		datetime					

	)
GO
	CREATE TABLE KHACHHANG
	(
		MaKH		char(10)				NOT NULL,
		TenKH		nvarchar(max)				,
		DiaChi		nvarchar(max)				,
		ThanhPho	nvarchar(max)				,
		DTKH		int							,
		NgayDH      datetime,
		GhiChu      char(50)

	)
GO
	CREATE TABLE CHITIETHD
	(
		MaHD	char(10)				NOT NULL,
		MaSP	char(10)				NOT NULL,
		SoLuong	int							
	)
GO
/* ----------------------TAO CAC KHOA CHINH------------------------*/
	ALTER TABLE SANPHAM ADD CONSTRAINT pk_SANPHAM
	PRIMARY KEY (MaSP)
	GO

	ALTER TABLE NHANVIEN ADD CONSTRAINT pk_NHANVIEN
	PRIMARY KEY (MaNV)
	GO

	ALTER TABLE KHACHHANG ADD CONSTRAINT pk_KHACHHANG
	PRIMARY KEY (MaKH)
	GO

	ALTER TABLE HOADON ADD CONSTRAINT pk_HOADON
	PRIMARY KEY (MaHD)
	GO

	ALTER TABLE CHITIETHD ADD CONSTRAINT pk_CHITIETHD
	PRIMARY KEY (MaHD,MaSP)
	GO
/* ----------------------TAO CAC KHOA NGOAI------------------------*/
	ALTER TABLE HOADON ADD CONSTRAINT pk_HOADON_KHACHHANG
	FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
	GO

	ALTER TABLE HOADON ADD CONSTRAINT pk_HOADON_NHANVIEN
	FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV)
	GO

	ALTER TABLE CHITIETHD ADD CONSTRAINT pk_CHITIETHD_HOADON
	FOREIGN KEY (MaHD) REFERENCES HOADON(MaHD)
	GO

	ALTER TABLE CHITIETHD ADD CONSTRAINT pk_CHITIETHD_SANPHAM
	FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
	GO

Create Table AUTH(
username nchar(50) not null,
password nchar(50) not null,)


/* ----------------------NHAP DU LIEU------------------------------*/
GO
INSERT INTO KHACHHANG (	 MaKH,		TenKH,DTKH,		DiaChi,					ThanhPho ) VALUES	('KH001', 'Trần AN B', 67891230,'Nguyen THi Minh Khai','TpHCM')
INSERT INTO KHACHHANG (	 MaKH,      TenKH,DTKH,     DiaChi,	                ThanhPho ) VALUES	('KH002','Lê Thị Nhi',27934560,'Pham Huu Chi','TpHCM')
INSERT INTO KHACHHANG (	 MaKH,		TenKH,DTKH,		DiaChi,					ThanhPho ) VALUES	('KH003','Bành Thu Hà',53987890,'Nguyen Pham Tuan','TpHCM')
INSERT INTO KHACHHANG (	 MaKH,		TenKH,DTKH,		DiaChi,					ThanhPho ) VALUES	('KH004','Vấn Nguyệt Anh',NULL,'Huyen Binh Chanh','TpHCM')
INSERT INTO KHACHHANG (	 MaKH,		TenKH,DTKH,		DiaChi,					ThanhPho ) VALUES	('KH005','Lung Thị Linh',1007890,'Landmark 81','TpHCM')
INSERT INTO KHACHHANG (	 MaKH,		TenKH,DTKH,		DiaChi,					ThanhPho ) VALUES	('KH006','Sinh Thị Tố Oanh',NULL,'Bitexco','TpHCM')
INSERT INTO KHACHHANG (	 MaKH,		TenKH,DTKH,		DiaChi,					ThanhPho ) VALUES	('KH007','Ngân Da Đen',76290868,'Thu Duc','TpHCM')
GO
INSERT INTO NHANVIEN(	 MaNV,		HoLot,				TenNV,		Phai,		NgaySinh,		NgayNV,			DTNV) VALUES	('1','Nguyễn Văn','Ảnh','TRUE','8/27/1999',	'2/15/2018',0001466)
INSERT INTO NHANVIEN(	 MaNV,		HoLot,				TenNV,		Phai,		NgaySinh,		NgayNV,			DTNV) VALUES	('2','Trần Thị','Thương','FALSE','12/30/1990',	'1/1/2018',	0200632)
INSERT INTO NHANVIEN(	 MaNV,		HoLot,				TenNV,		Phai,		NgaySinh,		NgayNV,			DTNV) VALUES	('3','Lê Thị','Mai Thanh Thảo','TRUE','4/26/1995',	'5/15/2018',    0067103)
INSERT INTO NHANVIEN(	 MaNV,		HoLot,				TenNV,		Phai,		NgaySinh,		NgayNV,			DTNV) VALUES	('4','Bùi Quang','Quỳnh Anh','FALSE','11/30/1991',	'7/23/2018',0056904)
INSERT INTO NHANVIEN(	 MaNV,		HoLot,				TenNV,		Phai,		NgaySinh,		NgayNV,			DTNV) VALUES    ('5','Trần Thu','Lỳ Hạ Vy','TRUE','10/22/1993',	'5/26/2018',0279005)
GO
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(1,		'Táo xanh TQ','kg',30000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(2,		'Táo xanh Mỹ','kg',40000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(3,		'Táo đỏ TQ','kg',30000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(4,		'Táo đỏ Mỹ','kg',35000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(5,		'Nho Mỹ','kg',	120000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(6,		'Nho Pháp','kg',150000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(7,		'Nho TQ','kg',80000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(8,		'Nho VN','kg',50000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(9,		'Cam Mỹ','kg',100000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(10,	'Cam xoàn',	'kg',40000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(11,	'Măng cụt','kg',50000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(12,	'Ổi','kg',15000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(13,	'Xoài Đài loan','kg',25000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(14,	'Xoài Cát','kg',50000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(15,	'Mãng cầu','kg',35000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(16,	'Nhãn da bò','kg',30000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(17,	'kiwi','kg',60000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(18,	'Dâu','kg',	120000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(19,	'Cherry','kg',200000)
INSERT INTO SANPHAM	(MaSP,	TenSP,				DVTinh,	DonGia) VALUES	(20,	'Sầu riêng','kg',90000)
GO
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190001,		'KH001',	1,		'01/05/19',	'01/10/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190002,		'KH003',	3,		'02/08/19',	'03/08/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190003,		'KH005',	5,		'03/25/19',	'03/30/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190004,		'KH004',	4,		'04/20/19',	'04/25/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190005,		'KH002',	3,		'05/05/19',	'04/05/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190006,		'KH003',	3,		'06/10/19',	'03/12/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190007,		'KH004',	5,		'07/01/19',	'07/10/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190008,		'KH005',	4,		'08/18/19',	'08/18/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190009,		'KH006',	4,		'09/01/19',	'09/05/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190010,		'KH005',	3,		'10/19/19',	'10/20/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190011,		'KH003',	3,		'11/22/19',	'11/22/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190012,		'KH007',	2,		'12/25/19',	NULL)
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190013,		'KH001',	4,		'01/12/19',	'01/20/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190014,		'KH002',	2,		'07/10/19',	'08/10/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190015,		'KH004',	5,		'08/10/19',	'08/12/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190016,		'KH006',	3,		'10/01/19',	'10/05/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190017,		'KH003',	4,		'12/01/19',	NULL)
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190018,		'KH005',	2,		'11/01/19',	'11/15/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190019,		'KH001',	4,		'03/01/19',	'03/15/19')
INSERT INTO HOADON(	  MaHD,			MaKH,		MaNV,	NgayLapHD,	NgayGH) VALUES  (20190020,		'KH008',	5,		'04/01/19',	'04/10/19')				
GO
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190001,		005	,		10	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190001,		007,		5	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190002,		003,		6	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190002,		009,		7	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190003,		011,		2	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190003,		012,		2	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190004,		005,		3	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190004,		014,		5	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190005,		002,		5	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190005,		017,		5	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20040006,		007,		15	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190006,		009,		5	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190007,		011,		2	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190007,		013,		3	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190008,		002,		10	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190008,		006,		4	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190009,		001,		9	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190009,		011,		4	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190010,		008,		15	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190010,		016,		10	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190011,		019,		10	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190012,		020,		8	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190013,		018,		5	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190014,		020,		12  )
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190015,		002,		4	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190016,		006,		4	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190016,		002,		12	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190017,		008,		5	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190018,		005,		10	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190019,		013,		1   )
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190019,		001,		11	)
INSERT INTO CHITIETHD(	 MaHD,			MaSP,		SoLuong) VALUES  (20190020,		010,		3	)

/* ----------------------QUERY------------------------------------*/


SELECT CONVERT(INT, MaSP ) FROM BANHANG.dbo.SANPHAM
Where CONVERT(INT, MaSP ) Between 1 and 10
order by CONVERT(INT, MaSP )

SELECT KH.MaKH, Sum(CTHD.SoLuong*SP.DonGia) as ThanhTien
from (((BANHANG.dbo.KHACHHANG KH JOIN BANHANG.dbo.HOADON HD ON KH.MaKH=HD.MaKH)
								 JOIN BANHANG.dbo.CHITIETHD CTHD ON HD.MaHD=CTHD.MaHD)
								 JOIN BANHANG.dbo.SANPHAM SP ON CTHD.MaSP= SP.MaSP)
								Group BY KH.MaKH

SELECT KH.MaKH, Sum(CTHD.SoLuong*SP.DonGia) as ThanhTien
from (((BANHANG.dbo.KHACHHANG KH JOIN BANHANG.dbo.HOADON HD ON KH.MaKH=HD.MaKH)
								 JOIN BANHANG.dbo.CHITIETHD CTHD ON HD.MaHD=CTHD.MaHD)
								 JOIN BANHANG.dbo.SANPHAM SP ON CTHD.MaSP= SP.MaSP)
								Group BY KH.MaKH
								PIVOT (Sum(CTHD.SoLuong*SP.DonGia) as ThanhTien in( Datepart(MONTH,HD.NgayLapHD)=1)


						