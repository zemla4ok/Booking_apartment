CREATE PROCEDURE addApartmentsFromXML
@path nvarchar(256),
@city nvarchar(30),
@hotel nvarchar(50)
AS BEGIN
	declare @city_id int;
	SET @city_id = (SELECT ID FROM CITY WHERE NAME like @city);

	declare @hotel_id int;
	SET @hotel_id = (SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id);

	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	BEGIN TRAN
		declare @result table(x xml);
		declare @sql nvarchar(300)=
			'SELECT 
				CAST(REPLACE(CAST(x AS VARCHAR(MAX)), ''encoding="utf-16"'', ''encoding="utf-8"'') AS XML)
				FROM OPENROWSET(BULK '''+@path+''', SINGLE_BLOB) AS T(x)';
		
		INSERT INTO @result EXEC(@sql);
		declare @xml XML = (SELECT TOP 1 x from @result);

		INSERT INTO APARTMENTS (CURRENT_COST, PLACES, FREE_PLACES, HOTEL_ID, APARTMENTS_NUM, CLOSE_DATE)
			SELECT 
				C3.value('curr_cost[1]', 'INT') As CURRENT_COST,
				C3.value('places[1]', 'INT') AS PLACES,
				C3.value('free_places[1]', 'INT') AS FREE_PLACES,
				@hotel_id,
				C3.value('apartment_num[1]', 'INT') AS APARTMENTS_NUM,
				C3.value('close_date[1]', 'DATE') AS CLOSE_DATE
			FROM @xml.nodes('dataroot') AS T3(C3);
	COMMIT;
END;

DROP PROCEDURE addApartmentsFromXML;

exec addApartmentsFromXML
	@path = 'C:\Users\Dmitry\Desktop\Booking_apartment\apartments.xml',
	@city = 'slm',
	@hotel = 'slm1';

select * from APARTMENTS
delete APARTMENTS;