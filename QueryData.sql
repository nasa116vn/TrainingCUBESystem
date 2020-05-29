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

drop table SVR_STORE
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


select* from SVR_STORE

