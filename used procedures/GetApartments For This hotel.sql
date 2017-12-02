CREATE PROCEDURE	GetApartmentsForThisHotel
	@city nvarchar(30),
	@hotel nvarchar(50)
AS BEGIN
	declare @city_id int;
	set @city_id = (SELECT ID FROM CITY WHERE NAME like @city);

	declare @hotel_id int;
	set @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id);
	
	SELECT 
		CURRENT_COST,
		PLACES,
		FREE_PlACES,
		APARTMENTS_NUM,
		IS_CLOSE
	 FROM APARTMENTS WHERE HOTEL_ID = @hotel_id;
END;

DROP PROCEDURE GetApartmentsForThisHotel