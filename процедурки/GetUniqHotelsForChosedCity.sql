CREATE PROCEDURE GetUniqHotelsForChosedCity
	@city nvarchar(30)
AS BEGIN
	declare @city_id int;
	SET @city_id = (SELECT ID FROM CITY WHERE NAME = @city);
	SELECT distinct h.NAME 
			FROM APARTMENTS[a] inner join HOTELS[h] on a.HOTEL_ID = h.ID 
			WHERE h.CITY_ID = 2;
END;

drop procedure GetUniqHotelsForChosedCity