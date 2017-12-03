CREATE PROCEDURE GetBookingListForCurrentHotel
	@city nvarchar(30),
	@hotel nvarchar(50)
AS BEGIN
	declare @city_id int;
	set @city_id = (SELECT ID FROM CITY WHERE NAME like @city);

	declare @hotel_id int;
	set @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id);	

	SELECT a.APARTMENTS_NUM,
		   al.DEFAULT_COST,
		   al.RESERVED_PLACES,
		   al.ARRIVIG_DATE,
		   al.EVICTION_DATE,
		   al.RESERVATION_DATE,
		   al.IS_EARLY,
		   al.ID
		   FROM APARTMENT_LIST [al] inner join APARTMENTS [a] on al.APARTMENTS_ID = a.ID WHERE a.HOTEL_ID = 1;								
END;

drop proc GetBookingListForCurrentHotel
