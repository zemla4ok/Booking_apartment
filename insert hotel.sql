CREATE PROCEDURE Registration
	@name NVARCHAR(50),
	@stars INT,
	@city NVARCHAR(30),
	@password NVARCHAR(32),
	@rc int output
	AS BEGIN
		DECLARE @city_id int;
		SET @rc = 0;
		IF NOT EXISTS(SELECT ID FROM CITY WHERE NAME = @city)
		BEGIN
			exec InsrtCity @name=@city;
		END
		SET @city_id = (SELECT ID FROM CITY WHERE NAME = @city);

		IF NOT EXISTS (SELECT ID FROM HOTELS WHERE CITY_ID = @city_id and NAME = @name)
		BEGIN
			INSERT INTO HOTELS (NAME, STARS, CITY_ID, HOTEL_PASSWORD)
				VALUES (@name, @stars, @city_id, @password);
			SET @rc = 1;
		END		
	END;	

exec Registration 
	@name = 'slm2',
	@stars = 4,
	@city = 'slm',
	@password = '1111',
	@rc = 10;

select * from HOTELS;

drop procedure InsertHotel
