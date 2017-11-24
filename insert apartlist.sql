CREATE PROCEDURE InsertApartList
	@passport_num nvarchar(20),
	@city_name nvarchar(30),
	@hotel_name nvarchar(50),
	@apart_num int,
	@arriving_date date,
	@eviction_date date,
	@is_early bit,
	@is_doseage bit,
	@reserved_places int
	AS BEGIN
		declare @user_id int;
		declare @city_id int;
		declare @hotel_id int;
		declare @apart_id int;
		declare @defaul_cost int;
		set @user_id = (SELECT ID FROM USERS WHERE PASSPORT_NUM like @passport_num);
		set @city_id = (SELECT ID FROM CITY WHERE NAME like @city_name);
		set @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel_name);
		set @apart_id = (SELECT ID FROM APARTMENTS WHERE APARTMENTS_NUM = @apart_num);
		set @defaul_cost = (SELECT CURRENT_COST FROM APARTMENTS WHERE ID = @apart_id);

		INSERT INTO APARTMENT_LIST(APARTMENTS_ID, USERS_ID, DEFAULT_COST, ARRIVIG_DATE, EVICTION_DATE, IS_EARLY,
										IS_DOSEAGE, RESERVED_PLACES, RESERVATION_DATE)
					VALUES(@apart_id, @user_id, @defaul_cost, @arriving_date, @eviction_date, @is_early,
										@is_doseage, @reserved_places,CAST(GETDATE() as date))
	END;

