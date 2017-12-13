CREATE PROCEDURE GetApartmentsForChoosedHotelName
	@city nvarchar(30),
	@hotel nvarchar(50)
AS BEGIN
	declare @city_id int;
	SET @city_id = (SELECT ID FROM CITY WHERE NAME = @city);	
	SELECT  c.NAME, 
			h.NAME, 
			h.STARS,
			a.CURRENT_COST,
			a.PLACES,
			a.FREE_PLACES,
			a.APARTMENTS_NUM,
			a.CLOSE_DATE
			FROM APARTMENTS[a] inner join HOTELS[h] on a.HOTEL_ID = h.ID 
							   inner join CITY[c] on c.ID = h.CITY_ID 
			WHERE h.CITY_ID = @city_id and h.NAME = @hotel;
END;