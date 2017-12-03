CREATE PROCEDURE BookApartments
	@city nvarchar(30),
	@hotel nvarchar(50),
	@apart_num int,
	@free_pl int,

	--user params
	@user_name nvarchar(20),
	@user_surname nvarchar(20),
	@passport_num nvarchar(20),

	--apart list params
	@cost int,
	@arr_date date,
	@evic_date date,
	@is_early bit,
	@is_doseeage bit,
	@reserv_places int,

	@rc int output
AS BEGIN
	--BEGIN TRY
		set @rc = 1;
		declare @city_id int;
		set @city_id = (SELECT ID FROM CITY WHERE NAME like @city);

		declare @hotel_id int;
		set @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id);

		declare @apart_id int;
		set @apart_id = (SELECT ID FROM APARTMENTS WHERE APARTMENTS_NUM = @apart_num and HOTEL_ID = @hotel_id);

		BEGIN TRANSACTION
			IF @is_doseeage = 0
			BEGIN
				set @reserv_places = @free_pl;
			END

			IF @reserv_places = @free_pl
			BEGIN
				UPDATE APARTMENTS SET IS_CLOSE = 1 WHERE ID = @apart_id;
			END

			UPDATE APARTMENTS SET FREE_PLACES = @free_pl - @reserv_places WHERE ID =@apart_id;

			IF NOT EXISTS (SELECT ID FROM USERS WHERE PASSPORT_NUM = @passport_num)
			BEGIN
				INSERT INTO USERS(NAME, SURNAME, PASSPORT_NUM)
					VALUES (@user_name, @user_surname, @passport_num);
			END		
			
			declare @user_id int;
			set @user_id = (SELECT ID FROM USERS WHERE PASSPORT_NUM = @passport_num);


			INSERT INTO APARTMENT_LIST(APARTMENTS_ID, USERS_ID, DEFAULT_COST, ARRIVIG_DATE, EVICTION_DATE, IS_EARLY, IS_DOSEAGE, RESERVED_PLACES, RESERVATION_DATE)
				VALUES(@apart_id, @user_id, @cost, @arr_date, @evic_date, @is_early, @is_doseeage, @reserv_places, GETDATE());
		COMMIT		
	--END TRY
	--BEGIN CATCH
	--	ROLLBACK;
	--	set @rc = 0;
	--END CATCH
END;

drop proc BookApartments

select * from users