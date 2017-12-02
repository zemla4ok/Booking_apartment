CREATE PROCEDURE GetUniqPlacesByCityAndStars
	@city nvarchar(30),
	@stars nvarchar(50)
AS BEGIN
	declare @city_id int;
	SET @city_id = (SELECT ID FROM CITY WHERE NAME = @city);	

	SELECT  distinct a.PLACES
			FROM APARTMENTS[a] inner join HOTELS[h] on a.HOTEL_ID = h.ID 
							   inner join CITY[c] on c.ID = h.CITY_ID 
			WHERE h.CITY_ID = @city_id and h.STARS = @stars and a.IS_CLOSE = 0;
END;

drop proc GetUniqPlacesByCityAndStars