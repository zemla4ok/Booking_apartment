CREATE PROCEDURE GetAllApartments
AS BEGIN
	SELECT  c.NAME, 
			h.NAME, 
			h.STARS,
			a.CURRENT_COST,
			a.PLACES,
			a.FREE_PLACES,
			a.APARTMENTS_NUM
			FROM APARTMENTS[a] inner join HOTELS[h] on a.HOTEL_ID = h.ID 
							   inner join CITY[c] on c.ID = h.CITY_ID WHERE a.IS_CLOSE = 0; 
END;

drop procedure GetAllApartments