ALTER PROCEDURE UpdateApartment
	@city nvarchar(30),
	@hotel nvarchar(50),
	@curr_cost int,
	@places int,
	@free_places int,
	@apart_num int,
	@is_close bit,
	@rc bit output
AS BEGIN
	SET @rc = 1;
	BEGIN TRY
		declare @city_id int;
		SET @city_id = (SELECT ID FROM CITY WHERE NAME like @city);
		declare @hotel_id int;
		SET @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id);
		IF @curr_cost != -1
		BEGIN
			UPDATE APARTMENTS 
				SET CURRENT_COST = @curr_cost 
				WHERE HOTEL_ID = @hotel_id and APARTMENTS_NUM = @apart_num;
		END
		IF @places != -1
		BEGIN
			UPDATE APARTMENTS 
				SET PLACES = @places
				WHERE HOTEL_ID = @hotel_id and APARTMENTS_NUM = @apart_num;
		END
		IF @free_places != -1
		BEGIN
			declare @current_places int;
			SELECT @current_places=PLACES FROM APARTMENTS WHERE HOTEL_ID = @hotel_id and APARTMENTS_NUM = @apart_num;
			IF @current_places<@free_places
			BEGIN
				set @rc=0;
				return;
			END
			UPDATE APARTMENTS 
				SET FREE_PLACES = @free_places
				WHERE HOTEL_ID = @hotel_id and APARTMENTS_NUM = @apart_num;
		END
		UPDATE APARTMENTS 
			SET IS_CLOSE = @is_close
			WHERE HOTEL_ID = @hotel_id and APARTMENTS_NUM = @apart_num;
	END TRY
	BEGIN CATCH
		SET @rc = 0;
	END CATCH
END;

drop procedure UpdateApartment

exec UpdateApartment
	@city ='q',
	@hotel ='q',
	@curr_cost = 1,
	@places = 1,
	@free_places =1,
	@apart_num =1,
	@close_date ='10.11.1997',
	@rc =1;

SELECT * FROM APARTMENTS;
	