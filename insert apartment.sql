CREATE PROCEDURE InsertApartment
	@curr_cost INT,
	@places INT,
	@free_places INT,
	@hotel NVARCHAR(50),
	@apartment_num INT,
	@close_date date
	AS BEGIN
		DECLARE @hotel_id int;
		SET @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel)
		INSERT INTO APARTMENTS(CURRENT_COST, PLACES, FREE_PLACES, HOTEL_ID, APARTMENTS_NUM, CLOSE_DATE)
			VALUES(@curr_cost, @places, @free_places, @hotel_id, @apartment_num, @close_date);
	END;

exec InsertApartment
	@curr_cost = 100,
	@places = 5,
	@free_places = 2,
	@hotel = 'zvezda',
	@apartment_num = 123,
	@close_date = '10-11-1997'

select * from APARTMENTS;

drop procedure InsertApartment;