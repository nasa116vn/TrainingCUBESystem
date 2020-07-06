create database Webmvc

Create table SVR_COMPANY(
COMPANY_ID int Primary key,
COMPANY_CD varchar(10),
COMPANY_NAME varchar(100),
INSERT_DATE datetime,
INSERT_USER varchar(20),
UPDATE_DATE datetime,
UPDATE_USER varchar(20),
DEL_FLAG int
)

Create table SVR_BUMON(
BUMON_ID int Primary key,
BUMON_CD varchar(10),
BUMON_NAME varchar(100),
COMPANY_ID int foreign key references SVR_COMPANY(COMPANY_ID),
INSERT_DATE datetime,
INSERT_USER varchar(20),
UPDATE_DATE datetime,
UPDATE_USER varchar(20),
DEL_FLAG int
) 
Create table SVR_USER(
USERNAME varchar(20) primary key,
PASSWORD varchar(100),
COMPANY_ID int foreign key references SVR_COMPANY(COMPANY_ID),
BUMON_ID int foreign key references SVR_BUMON(BUMON_ID),
INSERT_DATE datetime,
INSERT_USER varchar(20),
UPDATE_DATE datetime,
UPDATE_USER varchar(20),
DEL_FLAG int
)

Create table SVR_DEVICE(
IMEI varchar(15) primary key,
TOKEN varchar(100),
MODEL varchar(100),
MAKER varchar(100),
INSERT_DATE datetime,
INSERT_USER varchar(20),
UPDATE_DATE datetime,
UPDATE_USER varchar(20),
DEL_FLAG int
)

Create table SVR_STORE(
STORE_CD varchar(10) primary key,
STORE_NAME varchar(100),
INSERT_DATE datetime,
INSERT_USER varchar(20),
UPDATE_DATE datetime,
UPDATE_USER varchar(20),
DEL_FLAG int
)

Create table SVR_VIEW(
LOG_ID int primary key,
IMEI varchar(15) foreign key references SVR_DEVICE(IMEI),
BUMON_ID int foreign key references SVR_BUMON(BUMON_ID),
STORE_CD varchar(10) foreign key references SVR_STORE(STORE_CD),
PRODUCT_ID int,
VIEW_DATE varchar(10),
GENDER numeric,
AGE numeric,
VIEWS numeric,
INSERT_DATE datetime,
UPDATE_DATE datetime

)

Insert into SVR_VIEW values(1,'123',1,'1002',123,'2019-12-29',null,null,1,'2019-12-29','2019-12-29'),
(2,'124',1,'1002',123,'2019-12-29',null,null,1,'2019-12-29','2019-12-29'),
(3,'123',2,'1002',123,'2019-12-29',null,null,1,'2019-12-29','2019-12-29'),
(4,'123',3,'1001',123,'2019-12-29',null,null,1,'2019-12-29','2019-12-29'),
(5,'125',1,'1002',123,'2019-12-29',null,null,1,'2019-12-29','2019-12-29'),
(6,'126',2,'1003',123,'2019-12-29',null,null,1,'2019-12-29','2019-12-29')

Insert into SVR_BUMON values(1,'abc','SOFTLINE A',1,null,null,null,null,null)
Insert into SVR_BUMON values(2,'abcs','SOFTLINE B',1,null,null,null,null,null)
Insert into SVR_BUMON values(3,'abcd','SOFTLINE C',1,null,null,null,null,null)

Insert into SVR_COMPANY values(1,'1123','asdas',null,null,null,null,null)

Insert into SVR_STORE VALUES('1001','AEON CELADON','2019-05-25','admin',null,null,null),
('1002','AEON CANARY','2019-05-25','admin',null,null,null),
('1003','AEON LONG BIEN','2019-05-25','admin',null,null,null),
('1004','AEON BINH TAN','2019-05-25','admin',null,null,null)

Insert into SVR_DEVICE VALUES ('123','','vivi s5','Vivo','2019-12-30','admin','2019-12-30','admin',null),
('124','','vivi s5','Vivo','2019-12-30','admin','2019-12-30','admin',null),
('125','','VS3','VinaPhone','2019-12-30','admin','2019-12-30','admin',null),
('126','','Galaxy Tab A 2016','SamSung','2019-12-30','admin','2019-12-30','admin',null),
('127','','Xperia XZS','Sony','2019-12-30','admin','2019-12-30','admin',null),
('128','','vivi s5','Vivo','2019-12-30','admin','2019-12-30','admin',null)

Insert into SVR_USER values ('admin','123',null,null,null,null,null,null,null)
insert into SVR_USER values ('thuanphat','123456',null,null,null,null,null,null,null)

select* from SVR_STORE
select* from SVR_BUMON
select * from SVR_DEVICE

select d.IMEI,TOKEN, MODEL, MAKER, b.BUMON_NAME, t.STORE_NAME, d.INSERT_DATE, d.INSERT_USER, d.UPDATE_USER, d.UPDATE_DATE,d.DEL_FLAG 
from SVR_DEVICE d join SVR_VIEW v on d.IMEI = v.IMEI join SVR_BUMON b on b.BUMON_ID = v.BUMON_ID join SVR_STORE t on t.STORE_CD = v.STORE_CD
