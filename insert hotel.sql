CREATE PROCEDURE InsertHotel
	@name NVARCHAR(50),
	@stars INT,
	@city NVARCHAR(30),
	@password NVARCHAR(32)
	AS BEGIN
		DECLARE @city_id int;
		IF NOT EXISTS(SELECT ID FROM CITY WHERE NAME = @city)
		BEGIN
			exec InsrtCity @name=@city;
		END
		SET @city_id = (SELECT ID FROM CITY WHERE NAME = @city);
		INSERT INTO HOTELS (NAME, STARS, CITY_ID, HOTEL_PASSWORD)
			VALUES (@name, @stars, @city_id, @password)
	END;	

exec InsertHotel 
	@name = 'zvezda',
	@stars = 5,
	@city = 'qwe',
	@password = '1111';

select * from CITY;
select * from HOTELS;
