CREATE PROCEDURE Authorisation
	@city nvarchar(30),
	@hotel nvarchar(50),
	@password nvarchar(32),
	@rc int output
	AS BEGIN
		declare @city_id int;
		declare @hotel_id int;
		set @rc = 0;
		set @city_id = (SELECT ID FROM CITY WHERE NAME like @city);
		IF EXISTS(SELECT ID FROM HOTELS WHERE NAME like @hotel and CITY_ID = @city_id and HOTEL_PASSWORD like @password)
		BEGIN
			set @rc = 1;
		END
	END;

	drop procedure Authorisation;
