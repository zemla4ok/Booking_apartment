CREATE PROCEDURE SettleClient
	@city_name nvarchar(30),
	@hotel_name nvarchar(50),
	@apart_num int,
	@user_name nvarchar(20),
	@user_surname nvarchar(20),
	@passport_num nvarchar(30),
	@reserv_places int,
	@is_doseage bit,
	@rc bit output
AS BEGIN

	declare @city_id int;
	set @city_id = (SELECT ID FROM CITY WHERE NAME like @city_name);

	declare @hotel_id int;
	set @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel_name and CITY_ID = @city_id);

	declare @apart_id int;
	set @apart_id = (SELECT ID FROM APARTMENTS WHERE APARTMENTS_NUM = @apart_num and HOTEL_ID = @hotel_id);

	declare @free_pl int;
	declare @cost int;
	BEGIN TRANSACTION
	SELECT @free_pl=FREE_PLACES, @cost=CURRENT_COST FROM APARTMENTS WHERE ID = @apart_id;

	if @free_pl < @reserv_places
	BEGIN
		set @rc = 0;
		rollback;
		return;
	END

	IF @is_doseage = 0
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


	INSERT INTO APARTMENT_LIST(APARTMENTS_ID, USERS_ID, DEFAULT_COST, ARRIVIG_DATE, EVICTION_DATE, IS_EARLY,
										IS_DOSEAGE, RESERVED_PLACES, RESERVATION_DATE, IS_EVICTED)
					VALUES(@apart_id, @user_id, @cost, CAST(GETDATE() as date), CAST(GETDATE() as date), 0,
										@is_doseage, @reserv_places,CAST(GETDATE() as date), 0);
	COMMIT
	set @rc = 1;
END;

drop proc SettleClient