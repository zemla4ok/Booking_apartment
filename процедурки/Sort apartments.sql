CREATE PROCEDURE SortApartments
	@city nvarchar(30),
	@hotel nvarchar(50),
	@curr_cost bit,
	@places bit,
	@free_places bit,
	@apart_num bit,
	@is_close bit
AS BEGIN
	declare @city_id int;
	set @city_id = (SELECT ID FROM CITY WHERE NAME like @city);
	declare @hotel_id int;
	set @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id);
	IF @curr_cost = 1
	BEGIN
		SELECT CURRENT_COST, PLACES, FREE_PlACES, APARTMENTS_NUM, IS_CLOSE	
			FROM APARTMENTS WHERE HOTEL_ID = @hotel_id ORDER BY CURRENT_COST;
	END
	IF @places = 1
	BEGIN
		SELECT CURRENT_COST, PLACES, FREE_PlACES, APARTMENTS_NUM, IS_CLOSE	
			FROM APARTMENTS WHERE HOTEL_ID = @hotel_id ORDER BY PLACES;
	END
	IF @free_places = 1
	BEGIN
		SELECT CURRENT_COST, PLACES, FREE_PlACES, APARTMENTS_NUM, IS_CLOSE	
			FROM APARTMENTS WHERE HOTEL_ID = @hotel_id ORDER BY FREE_PLACES;
	END
	IF @apart_num = 1
	BEGIN
		SELECT CURRENT_COST, PLACES, FREE_PlACES, APARTMENTS_NUM, IS_CLOSE	
			FROM APARTMENTS WHERE HOTEL_ID = @hotel_id ORDER BY APARTMENTS_NUM;
	END
	IF @is_close = 1
	BEGIN
		SELECT CURRENT_COST, PLACES, FREE_PlACES, APARTMENTS_NUM, IS_CLOSE	
			FROM APARTMENTS WHERE HOTEL_ID = @hotel_id ORDER BY IS_CLOSE;
	END
END;

drop procedure SortApartments