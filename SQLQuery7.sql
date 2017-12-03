CREATE PROCEDURE GetSumForOrder
	@city nvarchar(30),
	@hotel nvarchar(50),
	@is_doseage bit,
	@places int,
	@apart_num int,
	@cost int,
	@is_early bit,
	@evic_date date,
	@arr_date date,
	@rc int output
AS BEGIN
	declare @city_id int;
	set @city_id = (SELECT ID FROM CITY WHERE NAME like @city);

	declare @hotel_id int;
	set @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id);

	IF @is_doseage = 0
	BEGIN
		set @places = (SELECT FREE_PLACES FROM APARTMENTS 
							WHERE HOTEL_ID = @hotel_id and APARTMENTS_NUM = @apart_num);
	END

	declare @koeff decimal;
	set @koeff = 0;
	IF @is_early = 1
	BEGIN
		set @koeff = 0.5;
	END

	set @rc = ((DAY(@evic_date) - DAY(@arr_date)) * @cost + 
				(DAY(@arr_date) - DAY(GETDATE())) * 0.5 * @cost) * @places +
				@koeff * @cost;					
END;

drop proc GetSumForOrder

SELECT * FROM Users
SELECT * FROM APARTMENT_LIST
