CREATE PROCEDURE GetHotelsForChosedCity
	@city nvarchar(30)
AS BEGIN
	declare @city_id int;
	SET @city_id = (SELECT ID FROM CITY WHERE NAME = @city);
	SELECT  c.NAME, 
			h.NAME, 
			h.STARS,
			a.CURRENT_COST,
			a.PLACES,
			a.FREE_PLACES,
			a.APARTMENTS_NUM
			FROM APARTMENTS[a] inner join HOTELS[h] on a.HOTEL_ID = h.ID 
							   inner join CITY[c] on c.ID = h.CITY_ID 
			WHERE h.CITY_ID = @city_id and a.IS_CLOSE = 0;
END;

DROP PROCEDURE GetHotelsForChosedCity
