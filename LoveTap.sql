CREATE DATABASE LOVETAP


SELECT CONVERT (date, GETDATE()) -- Without hours, minutes and seconds.

SET DATEFORMAT DMY

CREATE TABLE NHANVIEN (
	MANV CHAR(65) PRIMARY KEY,
	HOTEN NVARCHAR(255),
	NTNS SMALLDATETIME,
	SDT VARCHAR(30),
	DIACHI NVARCHAR(255),
	HESOLUONG FLOAT,
	LUONGCB FLOAT,
	MACN CHAR(65),
	VAITRO BIT, 
	EMAIL NVARCHAR(255)
)

CREATE TABLE CHINHANH  (
	MACN CHAR(65) PRIMARY KEY,
	TENCN NVARCHAR(255),
	DIACHI NVARCHAR(255),
	MAADMIN CHAR(65) FOREIGN KEY REFERENCES NHANVIEN(MANV),
)

CREATE TABLE KHACHHANG  (
	HOTEN NVARCHAR(255),
	SDT CHAR(30) PRIMARY KEY,
	DIACHI NVARCHAR(255),
	NGDK SMALLDATETIME,
	DOANHSO FLOAT,
	MANV CHAR(65) FOREIGN KEY REFERENCES NHANVIEN(MANV),
	NGSINH SMALLDATETIME,
)

ALTER TABLE KHACHHANG ALTER COLUMN NGDK DATE
SELECT * FROM KHACHHANG
CREATE TABLE HOADON (
	MAHD CHAR(65) PRIMARY KEY,
	NGMUA SMALLDATETIME,
	TONGTIEN FLOAT,
	LOAIHD BIT,
	MANV CHAR(65) FOREIGN KEY REFERENCES NHANVIEN(MANV),
	SDT CHAR(30) FOREIGN KEY REFERENCES KHACHHANG(SDT),
)

CREATE TABLE DANHMUC (
	MADM CHAR(65) PRIMARY KEY,
	TENDM NVARCHAR(255),
)

CREATE TABLE SANPHAM (
	MASP CHAR(65) PRIMARY KEY,
	TEN NVARCHAR(255),
	GIA FLOAT,
	NUOCSX NVARCHAR(255),
	CHITIET NVARCHAR(255),
	MADM CHAR(65) FOREIGN KEY REFERENCES DANHMUC(MADM),
)

CREATE TABLE CTHD (
	MAHD CHAR(65) FOREIGN KEY REFERENCES HOADON (MAHD),
	MASP CHAR(65) FOREIGN KEY REFERENCES SANPHAM (MASP),
	SOLUONG INT,
	CONSTRAINT PK_CTHD PRIMARY KEY (MAHD, MASP)
)

CREATE TABLE KHO (
	MAKHO CHAR(65) PRIMARY KEY,
	TENKHO NVARCHAR(255),
	DIACHI NVARCHAR(255),
	DIENTICH FLOAT,
	MANQL CHAR(65) FOREIGN KEY REFERENCES NHANVIEN(MANV),
)

CREATE TABLE TONKHO (
	MAKHO CHAR(65) FOREIGN KEY REFERENCES KHO (MAKHO),
	MASP CHAR(65) FOREIGN KEY REFERENCES SANPHAM (MASP),
	SOLUONG INT,
	CONSTRAINT PK_TONKHO PRIMARY KEY (MAKHO, MASP)
)

CREATE TABLE BAOHANH (
	SDT CHAR(30) FOREIGN KEY REFERENCES KHACHHANG (SDT),
	MASP CHAR(65) FOREIGN KEY REFERENCES SANPHAM (MASP),
	CONSTRAINT PK_BAOHANH PRIMARY KEY (SDT, MASP)
)

INSERT INTO NHANVIEN VALUES('NV01',N'Đỗ Phùng Gia Bảo','22/12/1999','0909657243',N'611 Điện Biên Phủ, Quận 3','0.6','6000000','CN01','1','baodpg@gmail.com')
INSERT INTO NHANVIEN VALUES('NV02',N'Nguyễn Hoàng Xuân Thảo','22/12/1999','0909657244',N'Hẻm 404 Nguyễn Đình Chiểu, Q3 ','0.3','6000000','CN02','0','thaonhx@gmail.com')
INSERT INTO NHANVIEN VALUES('NV03',N'Huỳnh Thế Hào','22/12/1999','0909657245',N'Hẻm 287, 289 Nguyễn Đình Chiểu ','0.9','8000000','CN03','0','haoht@gmail.com')
INSERT INTO NHANVIEN VALUES('NV04',N'Hoàng Đình Hiếu','22/12/1999','0909657246',N'209/43 Tôn Thất Thuyết ','0.3','6000000','CN04','0','hieuhd@gmail.com')
INSERT INTO NHANVIEN VALUES('NV05',N'Phan Hoàng Tuấn','22/12/1999','0909657247',N'229 đường 48, phường 2, Quận 4 ','0.6','12000000','CN08','1','tuanph@gmail.com')
INSERT INTO NHANVIEN VALUES('NV06',N'Lê Thị Thu Hằng','22/12/1999','0909657248',N'Số 01 Lô B2 chung cư phường 3, Quận 4 ','0.8','6000000','CN01','0','hangltt@gmail.com')
INSERT INTO NHANVIEN VALUES('NV07',N'Ngô Minh Phú','22/12/1999','0909657249',N'04 Phan Phú Tiên, phường 10, quận 5 ','0.4','7500000','CN07','0','phunm@gmail.com')
INSERT INTO NHANVIEN VALUES('NV08',N'Trần Bình Luật','22/12/1999','0909657250',N'Chung cư bellaza, đường số 2 7 Phú Mỹ','0.9','12000000','CN08','0','luattb@gmail.com')
INSERT INTO NHANVIEN VALUES('NV09',N'Dương Minh Lượng','22/12/1999','0909657251',N'Hẻm 280 Phạm Thế Hiển, P3 ','0.2','6000000','CN02','0','luongdm@gmail.com')
INSERT INTO NHANVIEN VALUES('NV10',N'Huỳnh Ngọc Quân','22/12/1999','0909657252',N'Số 67 đường 53 phường Tân Quy, Quận 7','0.7','7500000','CN10','1','quanhn@gmail.com')
SELECT * FROM NHANVIEN
DELETE FROM NHANVIEN

INSERT INTO CHINHANH VALUES('CN001',N'LoveTap chi nhánh Đăk Lăk',N'75 Hàn Thuyên, TT EaDrang, Đăk Lăk','NV01')	
INSERT INTO CHINHANH VALUES('CN002',N'LoveTap chi nhánh Bình Dương',N'77 Bình Thung, Bình An, Dĩ An, Binh Dương','NV01')	
INSERT INTO CHINHANH VALUES('CN003',N'LoveTap chi nhánh TP. Hồ Chí Minh',N'88 Đường Số 8, Bình Hưng, Bình Chánh','NV01')	
INSERT INTO CHINHANH VALUES('CN004',N'LoveTap chi nhánh Bình Phước',N'27 Nơ Trang Long, Đồng Xoài, Bình Phước','NV01')	
INSERT INTO CHINHANH VALUES('CN005',N'LoveTap chi nhánh TP. Hồ Chí Minh',N'117/80 Hồ Văn Long; CC Lê Thành; P. Tân Tạo; Q. Bình Tân','NV10')	
INSERT INTO CHINHANH VALUES('CN006',N'LoveTap chi nhánh TP. Hồ Chí Minh',N'26 Trần Quý Cáp P11 Q Bình Thạnh','NV10')	
INSERT INTO CHINHANH VALUES('CN007',N'LoveTap chi nhánh Bình Dương',N'12 Nguyễn Văn Việt, Thuận An, Binh Dương','NV10')	
INSERT INTO CHINHANH VALUES('CN008',N'LoveTap chi nhánh Đăk Lăk',N'Km72, EaNam, EaHLeo, Đăk Lăk','NV05')	
INSERT INTO CHINHANH VALUES('CN009',N'LoveTap chi nhánh Đăk Nông',N'123 Nguyễn Du, TP Gia Nghĩa, Đăk Nông','NV05')	
INSERT INTO CHINHANH VALUES('CN010',N'LoveTap chi nhánh Đăk Lăk',N'Thôn 2A, EaSir, EaHLeo, Đăk Lăk','NV05')

SELECT * FROM CHINHANH
DELETE FROM CHINHANH

INSERT INTO KHACHHANG VALUES(N'Nguyễn Hoàng Duy','0908200585',N'Hẻm 133 Hòa Hưng, phường 12,quận 10 ','20/11/2021','19350000', 'NV01', '5/5/1999')
INSERT INTO KHACHHANG VALUES(N'Lê Đăng Dũng','0908200586',N'Tổ 7, KP 1A, Đông Hưng Thuận 12 Đông Hưng Thuận','20/8/2021','21100000', 'NV02', '15/9/2006')
INSERT INTO KHACHHANG VALUES(N'Tạ Việt Hoàng','0908200587',N'Hẻm 392 Cao Thắng, phường 12, quận 10 ','8/8/2021','35350000', 'NV03', '6/7/2003')
INSERT INTO KHACHHANG VALUES(N'Nguyễn Hoàng Quốc Ấn','0908200588',N'Tổ 7, KP 1A, Đông Hưng Thuận 12 Đông Hưng Thuận','19/2/2022','37100000', 'NV04', '5/9/2004')
INSERT INTO KHACHHANG VALUES(N'Lê Kim Danh','0908200589',N'Tổ 10, KP2 Đông Hưng Thuận 12 Đông Hưng Thuận','15/5/2021','450000', 'NV05', '5/9/2005')
INSERT INTO KHACHHANG VALUES(N'Tô Thị Mỹ Âu','0908200590',N'Hẻm 481 ( KP4, Đông Hưng Thuận) 12 Đông Hưng Thuận','1/5/2022','100000', 'NV06', '3/4/1998')
INSERT INTO KHACHHANG VALUES(N'Nguyễn Hoàng Kim Ngân','0908200591',N'523/28/2 Lê Văn Khương, KP 5,Hiệp Thành 12 Hiệp Thành','30/7/2021','350000', 'NV07', '2/6/2001')
INSERT INTO KHACHHANG VALUES(N'Tô Trọng Nghĩa','0908200592',N'P404 số 60, tổ 6, KP7 THT 12 Tân Hưng Thuận','11/12/2021','18450000', 'NV08', '18/1/2002')
INSERT INTO KHACHHANG VALUES(N'Trần Nguyễn Đức Huy','0908200593',N'Chung cư Hưng Ngân (48A đường Dương Thị Mười, phường Tân Chánh Hiệp, quận 12)','3/7/2021','56000000', 'NV09', '17/7/2006')
INSERT INTO KHACHHANG VALUES(N'Đoàn Gia Thịnh','0908200594',N'Chung cư Topaz Home 102 Phan Văn Hớn, Tân Thới Nhất, Quận 12','19/8/2022','450000', 'NV10', '5/7/1999')

SELECT * FROM KHACHHANG
DELETE FROM KHACHHANG

INSERT INTO HOADON VALUES('HD0001','19/10/2022','19350000','1','NV01','0908200585')
INSERT INTO HOADON VALUES('HD0002','20/10/2022','21100000','1','NV02','0908200586')
INSERT INTO HOADON VALUES('HD0003','21/10/2022','35350000','1','NV02','0908200587')
INSERT INTO HOADON VALUES('HD0004','22/10/2022','37100000','1','NV01','0908200588')
INSERT INTO HOADON VALUES('HD0005','23/10/2022','450000','1','NV10','0908200589')
INSERT INTO HOADON VALUES('HD0006','24/10/2022','100000','1','NV10','0908200590')
INSERT INTO HOADON VALUES('HD0007','25/10/2022','350000','1','NV02','0908200591')
INSERT INTO HOADON VALUES('HD0008','26/10/2022','18450000','1','NV01','0908200592')
INSERT INTO HOADON VALUES('HD0009','27/10/2022','56000000','0','NV10','0908200593')
INSERT INTO HOADON VALUES('HD0010','28/10/2022','450000','0','NV02','0908200594')

SELECT * FROM HOADON
DELETE FROM HOADON

INSERT INTO DANHMUC VALUES('DM01',N'Chuột - Lót chuột')
INSERT INTO DANHMUC VALUES('DM02',N'Bàn phím')
INSERT INTO DANHMUC VALUES('DM03',N'Màn hình')
INSERT INTO DANHMUC VALUES('DM04',N'Linh kiện ')
INSERT INTO DANHMUC VALUES('DM05',N'Laptop')
INSERT INTO DANHMUC VALUES('DM06',N'Card màn hình')
INSERT INTO DANHMUC VALUES('DM07',N'Tai nghe')

SELECT * FROM DANHMUC
DELETE FROM DANHMUC

INSERT INTO SANPHAM VALUES('SP001',N'Laptop Gaming ASUS','19000000','China','2.2kg, 16GB RAM','DM01')
INSERT INTO SANPHAM VALUES('SP002',N'Laptop Vivobook','20999000','USA','1.5kg, 8GB RAM','DM01')
INSERT INTO SANPHAM VALUES('SP003',N'Laptop Macbook','21000000','Vietnam','2.2kg, 8GB RAM','DM01')
INSERT INTO SANPHAM VALUES('SP004',N'Laptop HP','35000000','Korea','2.2kg, 8GB RAM','DM01')
INSERT INTO SANPHAM VALUES('SP005',N'Laptop Dell','37000000','Singapore','2.2kg, 16GB RAM','DM01')
INSERT INTO SANPHAM VALUES('SP006',N'Laptop MSI','56000000','France','2.2kg, 16GB RAM','DM01')
INSERT INTO SANPHAM VALUES('SP007',N'Laptop Acer','15000000','USA','2.2kg, 16GB RAM','DM01')
INSERT INTO SANPHAM VALUES('SP008',N'Pad chuột đen','100000','Vietnam','2.2kg, 16GB RAM','DM03')
INSERT INTO SANPHAM VALUES('SP009',N'Chuột không dây Logitech','350000','China ','2.2kg, 16GB RAM','DM02')
INSERT INTO SANPHAM VALUES('SP010',N'Laptop Samsung','18000000','Korea','2.2kg, 16GB RAM','DM01')

SELECT * FROM SANPHAM
DELETE FROM SANPHAM

INSERT INTO KHO VALUES('MK01',N'Kho LoveTap chi nhánh Đăk Lăk',N'123 Đắk Lắk','7000','NV06')	
INSERT INTO KHO VALUES('MK02',N'Kho LoveTap chi nhánh Bình Dương',N'456 Dĩ An, Bình Dương','5000','NV04')	
INSERT INTO KHO VALUES('MK03',N'Kho LoveTap chi nhánh Thủ Đức',N'69 Thủ Đức, TPHCM','10000','NV02')	
INSERT INTO KHO VALUES('MK04',N'Kho LoveTap chi nhánh Bình Phước',N'96 Bình Phước','9000','NV06')	
INSERT INTO KHO VALUES('MK05',N'Kho LoveTap chi nhánh Quận 1',N'37 Quận 1, TPHCM','6000','NV02')	
INSERT INTO KHO VALUES('MK06',N'Kho LoveTap chi nhánh Bình Thạnh',N'547 Quận Bình Thạnh, TPHCM','5500','NV02')	
INSERT INTO KHO VALUES('MK07',N'Kho LoveTap chi nhánh Bình Dương',N'72 Bình Dương','6800','NV04')	
INSERT INTO KHO VALUES('MK08',N'Kho LoveTap chi nhánh Đăk Lăk',N'121 Đắk Lắk','9800','NV06')	
INSERT INTO KHO VALUES('MK09',N'Kho LoveTap chi nhánh Đăk Nông',N'92 Đắk Nông','7000','NV06')	
INSERT INTO KHO VALUES('MK10',N'Kho LoveTap chi nhánh Đăk Lăk',N'57 Đắk Lắk','7500','NV06')	

SELECT * FROM KHO
DELETE FROM KHO

INSERT INTO CTHD VALUES('HD0001','SP001','1')
INSERT INTO CTHD VALUES('HD0001','SP009','1')
INSERT INTO CTHD VALUES('HD0002','SP003','1')
INSERT INTO CTHD VALUES('HD0002','SP008','1')
INSERT INTO CTHD VALUES('HD0003','SP004','1')
INSERT INTO CTHD VALUES('HD0003','SP009','1')
INSERT INTO CTHD VALUES('HD0004','SP005','1')
INSERT INTO CTHD VALUES('HD0004','SP008','1')
INSERT INTO CTHD VALUES('HD0005','SP008','1')
INSERT INTO CTHD VALUES('HD0005','SP009','1')
INSERT INTO CTHD VALUES('HD0006','SP008','1')
INSERT INTO CTHD VALUES('HD0007','SP009','1')
INSERT INTO CTHD VALUES('HD0008','SP008','1')
INSERT INTO CTHD VALUES('HD0008','SP009','1')
INSERT INTO CTHD VALUES('HD0008','SP010','1')
INSERT INTO CTHD VALUES('HD0009','SP006','1')
INSERT INTO CTHD VALUES('HD0010','SP008','1')
INSERT INTO CTHD VALUES('HD0010','SP009','1')

SELECT * FROM CTHD
DELETE FROM CTHD

INSERT INTO TONKHO VALUES('MK01','SP001','50')
INSERT INTO TONKHO VALUES('MK02','SP002','40')
INSERT INTO TONKHO VALUES('MK03','SP003','45')
INSERT INTO TONKHO VALUES('MK04','SP004','55')
INSERT INTO TONKHO VALUES('MK05','SP005','100')
INSERT INTO TONKHO VALUES('MK06','SP006','80')
INSERT INTO TONKHO VALUES('MK07','SP007','76')
INSERT INTO TONKHO VALUES('MK08','SP008','200')
INSERT INTO TONKHO VALUES('MK09','SP009','300')
INSERT INTO TONKHO VALUES('MK10','SP010','40')

SELECT * FROM TONKHO
DELETE FROM TONKHO

INSERT INTO BAOHANH VALUES('0908200585','SP001')
INSERT INTO BAOHANH VALUES('0908200586','SP002')
INSERT INTO BAOHANH VALUES('0908200587','SP003')
INSERT INTO BAOHANH VALUES('0908200588','SP004')
INSERT INTO BAOHANH VALUES('0908200589','SP005')
INSERT INTO BAOHANH VALUES('0908200590','SP006')
INSERT INTO BAOHANH VALUES('0908200591','SP007')
INSERT INTO BAOHANH VALUES('0908200592','SP007')
INSERT INTO BAOHANH VALUES('0908200593','SP001')
INSERT INTO BAOHANH VALUES('0908200594','SP010')

SELECT * FROM BAOHANH
DELETE FROM BAOHANH
--TRG01: Ngày đăng ký của khách hàng hơn ngày sinh khách hàng
--TRG02: Doanh số của khách hàng bằng tổng tất cả tiền hóa đơn mà khách hàng đó mua
--TRG03: Tổng tiền của một hóa đơn bằng tổng số lượng * giá tiền
---------------------------------------------------------------------
CREATE TRIGGER TRG01_INS_KHACHHANG ON KHACHHANG
FOR INSERT
AS
BEGIN
		IF( EXISTS (
				SELECT *
				FROM inserted
				WHERE NGDK < NGSINH))
				BEGIN
					PRINT 'TRG01 INSERT KH ERROR...'
					ROLLBACK TRAN
				END
		ELSE
			PRINT 'TRG01 INSERT KH SUCCESFULLY'
END

DROP TRIGGER TRG01_INS_KHACHHANG

INSERT INTO KHACHHANG VALUES(N'Trần Phúc','0908200595',N'Chung cư Topaz Home 115 Phan Văn Hớn, Tân Thới Nhất, Quận 12','19/8/2020','450000', 'NV02', '5/7/1998')

-------------------------------------------------------------
CREATE TRIGGER TRG01_UPD_KHACHHANG ON KHACHHANG --BỊ LỖI
FOR UPDATE
AS
BEGIN
	IF(EXISTS (
		SELECT * 
		FROM inserted
		WHERE NGDK < (SELECT NGSINH FROM deleted)))
		BEGIN
			PRINT 'TRG01 UPDATE KH ERROR...'
			ROLLBACK TRAN
		END
	ELSE
		BEGIN
		UPDATE KHACHHANG
		SET NGDK = (SELECT NGDK FROM inserted), NGSINH = (SELECT NGSINH FROM inserted)
		WHERE SDT IN (SELECT SDT FROM inserted)
		PRINT 'TRG01 UPDATE KH SUCESSFULLY'
		END
END
DROP TRIGGER TRG01_UPD_KHACHHANG

SELECT * FROM KHACHHANG

UPDATE KHACHHANG
SET NGDK = '22/11/1994'
WHERE HOTEN = '0908200597'
------------------------------------------------------------------
CREATE TRIGGER TRG02_INS_KHACHHANG ON KHACHHANG
FOR INSERT 
AS
BEGIN
	IF (EXISTS (
		SELECT *
		FROM inserted
		WHERE DOANHSO <> 0))
		BEGIN
			PRINT 'TRG02 INSERT KH ERROR...'
			UPDATE KHACHHANG
			SET DOANHSO = 0
			WHERE SDT IN (SELECT SDT FROM inserted)
			PRINT 'TRG02 DA CAP NHAT DOANHSO = 0'
		END
	ELSE 
		PRINT 'TRG02 INSERT SUCESSFULLY'
END

DROP TRIGGER TRG02_INS_KHACHHANG

SELECT * FROM KHACHHANG

INSERT INTO KHACHHANG VALUES(N'Trần Đình Tâm','0908200597',N'Hẻm 30 Điện Biên Phủ, phường 6, Quận 3','20/11/2021','200', 'NV01', '5/6/1998')
INSERT INTO KHACHHANG VALUES(N'Nguyễn Xuân Tài','0908200598',N'Hẻm 45 Điện Biên Phủ, phường 6, Quận 3','18/10/2019','200', 'NV03', '5/6/1996')

DELETE FROM KHACHHANG WHERE SDT = '0908200598'

---------------------------------------------------------
CREATE TRIGGER TRG02_UPD_KHACHHANG ON KHACHHANG  --CONFLICT TRG03
FOR UPDATE
AS
BEGIN
	IF (EXISTS(
		SELECT * 
		FROM inserted
		WHERE DOANHSO <> (SELECT DOANHSO FROM deleted)))
		BEGIN
			PRINT'TRG02 UPDATE KH ERROR...'
			ROLLBACK TRAN
		END
	ELSE
		PRINT 'TRG02 UPDATE KH SUCESSFULLY'
END

DROP TRIGGER TRG02_UPD_KHACHHANG

SELECT * FROM KHACHHANG
UPDATE KHACHHANG
SET DOANHSO = 2000
WHERE HOTEN = 'Nguyễn Hoàng Duy'
--------------------------------------------------------------------
--C1:
CREATE TRIGGER TRG02_IUD_HOADON ON HOADON
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	UPDATE KHACHHANG
	SET DOANHSO = (
		SELECT  SUM (TONGTIEN)
		FROM HOADON
		WHERE HOADON.SDT = KHACHHANG.SDT
		)
	WHERE SDT IN (SELECT SDT FROM inserted)
	OR SDT IN (SELECT SDT FROM deleted)
	PRINT 'DA UPDATE DOANHSO THANH CONG'
END

DROP TRIGGER TRG02_IUD_HOADON

SELECT * FROM HOADON
SELECT *FROM KHACHHANG
UPDATE HOADON
SET TONGTIEN = 1
WHERE MAHD = 'HD001'
--C2:
--INSERT:
--CREATE TRIGGER TRG02_INS_HOADON ON HOADON
--AFTER INSERT 
--AS
--BEGIN
--		UPDATE KHACHHANG
--		SET DOANHSO = DOANHSO + (SELECT TONGTIEN FROM inserted WHERE SDT = KHACHHANG.SDT)
--		FROM KHACHHANG JOIN inserted ON KHACHHANG.SDT = inserted.SDT
--END
--GO
--DROP TRIGGER TRG02_INS_HOADON
SELECT * FROM KHACHHANG
SELECT * FROM HOADON
DELETE FROM HOADON WHERE SDT = '0908200596' OR SDT = '0908200597'

-----------------------------------------------------------
CREATE TRIGGER TRG03_INS_HOADON ON HOADON
FOR INSERT 
AS
BEGIN
	IF(EXISTS (
		SELECT * 
		FROM inserted
		WHERE TONGTIEN <> 0))
		BEGIN
			PRINT 'TRG03 INSERT HD ERROR...'
			ROLLBACK TRAN
		END
	ELSE
		PRINT 'TRG03 INSERT HD SUCESSFULLY'
END

DROP TRIGGER TRG03_INS_HOADON

INSERT INTO HOADON VALUES('HD0011','28/10/2022','450000','0','NV03','0908200596')
--------------------------------------------------------------------------
CREATE TRIGGER TRG03_UPD_HOADON ON HOADON
FOR UPDATE
AS
BEGIN
	IF (EXISTS (
		SELECT inserted.MAHD
		FROM inserted INNER JOIN CTHD ON CTHD.MAHD = inserted.MAHD INNER JOIN SANPHAM ON  SANPHAM.MASP = CTHD.MASP
		GROUP BY inserted.MAHD
		HAVING SUM ( SOLUONG *GIA ) <> (SELECT TONGTIEN FROM inserted)))
		BEGIN
			PRINT 'TRG03 UPDATE HD ERROR...'
			ROLLBACK TRAN
		END
	ELSE
		PRINT 'TRG03 UPDATE HD SUCESSFULLY'
END

DROP TRIGGER TRG03_UPD_HOADON

UPDATE HOADON
SET TONGTIEN = 0
WHERE MAHD = 'HD0010'
--------------------------------------------------------------------------
CREATE TRIGGER TRG03_IUD_CTHD ON CTHD
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	UPDATE HOADON
	SET TONGTIEN = (
		SELECT SUM (SOLUONG * GIA)
		FROM CTHD INNER JOIN SANPHAM ON CTHD.MASP = SANPHAM.MASP
		WHERE HOADON.MAHD = CTHD.MAHD
		GROUP BY MAHD)
	WHERE MAHD IN (SELECT MAHD FROM inserted)
	OR MAHD IN (SELECT MAHD FROM deleted)
	PRINT 'DA UPDATE TONGTIEN THANH CONG'
END

DROP TRIGGER TRG03_IUD_CTHD

-------------------------------------------------------
CREATE TRIGGER TRG03_UPD_SANPHAM ON SANPHAM
FOR UPDATE
AS
BEGIN
	UPDATE HOADON
	SET TONGTIEN = (
		SELECT SUM (SOLUONG * GIA)
		FROM CTHD INNER JOIN SANPHAM ON CTHD.MASP = SANPHAM.MASP
		WHERE HOADON.MAHD = CTHD.MAHD
		GROUP BY MAHD)
	FROM HOADON INNER JOIN CTHD ON HOADON.MAHD = CTHD.MAHD INNER JOIN inserted ON CTHD.MASP = inserted.MASP
	PRINT 'DA UPDATE TONGTIEN THANH CONG'
END

DROP TRIGGER TRG03_UPD_SANPHAM
 UPDATE SANPHAM
 SET GIA = 21000000
 WHERE MASP = 'SP001'

 SELECT * FROM SANPHAM
 SELECT * FROM KHACHHANG
 SELECT * FROM HOADON
--------------------------------------------------------
--TRG04: Khi thêm một chi tiết hóa đơn thì số lượng hàng tồn kho sẽ giảm 
CREATE TRIGGER TRG04_INS_CTHD ON CTHD
FOR INSERT
AS
BEGIN
	UPDATE TONKHO
	SET SOLUONG = SOLUONG - (SELECT SOLUONG FROM inserted)
	WHERE MASP IN (SELECT MASP FROM inserted)
END

DROP TRIGGER TRG04_INS_CTHD

------------------------------------------------------------------
CREATE TRIGGER TRG04_DEL_CTHD ON CTHD
FOR DELETE
AS
BEGIN
	UPDATE TONKHO
	SET SOLUONG = SOLUONG + (SELECT SOLUONG FROM deleted)
	WHERE MASP IN (SELECT MASP FROM deleted)
END

DROP TRIGGER TRG04_DEL_CTHD

---------------------------------------------------------------
CREATE TRIGGER TRG04_UPD_CTHD ON CTHD
FOR UPDATE
AS
BEGIN
	UPDATE TONKHO
	SET SOLUONG = SOLUONG + (SELECT SOLUONG FROM deleted) - (SELECT SOLUONG FROM inserted)
	WHERE MASP IN (SELECT MASP FROM inserted)
END

DROP TRIGGER TRG04_UPD_CTHD

--------------------------------------------------
--THÊM

CREATE TABLE LOGIN
(
	ID CHAR(255) Primary Key,
	USERNAME VARCHAR(255),
	USERPASS VARCHAR(255),
)


INSERT INTO LOGIN VALUES ('01','admin','db69fc039dcbd2962cb4d28f5891aae1')
INSERT INTO LOGIN VALUES ('02','staff','978aae9bb6bee8fb75de3e4830a1be46')



SELECT * FROM LOGIN
