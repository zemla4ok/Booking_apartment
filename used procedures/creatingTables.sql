CREATE TABLE CITY(
	ID int primary key IDENTITY(1, 1),
	NAME nvarchar(30) unique not null);

CREATE TABLE HOTELS(
	ID int primary key IDENTITY(1, 1),
	HOTEL_PASSWORD nvarchar(32) not null,
	NAME nvarchar(50) not null,
	STARS int not null,
	CITY_ID int not null,
	foreign key (CITY_ID) references CITY(ID));

CREATE TABLE APARTMENTS(
	ID int IDENTITY(1, 1),
	CURRENT_COST int not null,
	PLACES int not null,
	FREE_PLACES int not null,
	HOTEL_ID int not null,
	APARTMENTS_NUM int not null,
	CLOSE_DATE date not null,
	primary key(ID),
	foreign key(HOTEL_ID) references HOTELS(ID));

CREATE TABLE USERS(
	ID int IDENTITY(1,1),
	NAME nvarchar(20) not null,
	SURNAME nvarchar(20) not null,
	PASSPORT_NUM nvarchar(20) unique not null,
	primary key(ID));

CREATE TABLE APARTMENT_LIST(
	ID int IDENTITY(1,1),
	APARTMENTS_ID int not null,
	USERS_ID int not null,
	DEFAULT_COST int not null,
	ARRIVIG_DATE date not null,
	EVICTION_DATE date not null, 
	IS_EARLY bit not null,
	IS_DOSEAGE bit not null,
	RESERVED_PLACES int not null,
	RESERVATION_DATE date not null,
	primary key(ID),
	foreign key(APARTMENTS_ID) references APARTMENTS(ID),
	foreign key(USERS_ID) references USERS(ID));


	--DROP TABLES--
	drop table APARTMENT_LIST;
	drop table APARTMENTS;
	drop table HOTELS;
	drop table CITY;
	drop table USERS;
