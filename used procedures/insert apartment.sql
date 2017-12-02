CREATE PROCEDURE AddApartment
	@curr_cost INT,
	@places INT,
	@free_places INT,
	@city NVARCHAR(30),
	@hotel NVARCHAR(50),
	@apartment_num INT,
	@is_close bit,
	@rc bit output
	AS BEGIN
		SET @rc = 0;
		DECLARE @city_id INT;
		DECLARE @hotel_id INT;
		IF @free_places <= @places
		BEGIN
			IF EXISTS(SELECT ID FROM CITY WHERE NAME like @city)
			BEGIN
				SET @city_id = (SELECT ID FROM CITY WHERE NAME like @city);
				IF EXISTS(SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id)
				BEGIN
					SET @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id);
					IF NOT EXISTS(SELECT ID FROM APARTMENTS WHERE HOTEL_ID = @hotel_id and APARTMENTS_NUM = @apartment_num)
					BEGIN
						INSERT INTO APARTMENTS(CURRENT_COST, PLACES, FREE_PLACES, HOTEL_ID, APARTMENTS_NUM, IS_CLOSE)
							VALUES(@curr_cost, @places, @free_places, @hotel_id, @apartment_num, @is_close);
							SET @rc = 1;
					END
				END
			END
		END
	END;

drop procedure AddApartment;