create database PREMIUM_CINEMA_DATABASE

use PREMIUM_CINEMA_DATABASE

create table USERS (
	ID uniqueidentifier constraint PK_dbo_USERS primary key,
	NAME nvarchar(64) not null,
	SURNAME nvarchar(64) not null,
	LOGIN nvarchar(64) constraint UQ_dbo_USERS_LOGIN unique not null, 
	PASSWORD varchar(64) not null,
	ROLE int not null,
	EMAIL varchar(64) constraint UQ_dbo_USERS_EMAIL unique not null,
	PHONE varchar(16) constraint UQ_dbo_USERS_PHONE unique not null 
)

create table MOVIE (
	ID uniqueidentifier constraint PK_dbo_MOVIE primary key,
	TITLE nvarchar(128) not null,
	DIRECTOR nvarchar(128) not null,
	GENRE nvarchar(64) not null,
	DURATION int not null,
	RATING real not null,
	PHOTO varbinary(max) not null
)

create table SCHEDULE (
	ID uniqueidentifier constraint PK_dbo_SCHEDULE primary key,
	ID_MOVIE uniqueidentifier constraint FK_dbo_MOVIE foreign key references MOVIE(ID) not null,
	AVIABLE_SEATS int not null,
	DATETIME datetime2(7) not null
)

create table ORDERS (
	ID uniqueidentifier constraint PK_dbo_ORDERS primary key,
	ID_USER uniqueidentifier constraint FK_dbo_ORDERS_dbo_USERS foreign key references USERS(ID) not null,
	ID_SCHEDULE uniqueidentifier constraint FK_dbo_ORDERS_dbo_SCHEDULE foreign key references SCHEDULE(ID) not null,
	NUMBER_OF_SEATS int not null,
	ORDER_STATUS varchar(32) not null
)









