CREATE TABLE CITY(
	ID int primary key IDENTITY(1, 1),
	NAME nvarchar(30));

CREATE TABLE HOTELS(
	ID int primary key IDENTITY(1, 1),
	HOTEL_PASSWORD nvarchar(32),
	NAME nvarchar(50),
	STARS int,
	CITY_ID int,
	foreign key (CITY_ID) references CITY(ID));

CREATE TABLE APARTMENTS(
	ID int IDENTITY(1, 1),
	CURRENT_COST int,
	PLACES int,
	FREE_PLACES int,
	HOTEL_ID int,
	APARTMENTS_NUM int,
	CLOSE_DATE date,
	primary key(ID),
	foreign key(HOTEL_ID) references HOTELS(ID));

CREATE TABLE BOOKING(
	ID int identity(1,1),
	RESERVATION_DATE date,
	primary key(ID));

CREATE TABLE USERS(
	ID int IDENTITY(1,1),
	NAME nvarchar(20),
	SURNAME nvarchar(20),
	PASSPORT_NUM nvarchar(20),
	BOOKING_ID int,
	primary key(ID),
	foreign key (BOOKING_ID) references BOOKING(ID));

CREATE TABLE APARTMENT_LIST(
	ID int IDENTITY(1,1),
	APARTMENTS_ID int,
	BOOKING_ID int,
	DEFAULT_COST int,
	ARRIVIG_DATE date,
	EVICTION_DATE date,
	IS_EARLY bit,
	IS_DOSEAGE bit,
	RESERVED_PLACES int,
	primary key(ID),
	foreign key(APARTMENTS_ID) references APARTMENTS(ID),
	foreign key(BOOKING_ID) references BOOKING(ID));


	--DROP TABLES--
	drop table APARTMENT_LIST;
	drop table APARTMENTS;
	drop table HOTELS;
	drop table CITY;
	drop table USERS;
	drop table BOOKING;