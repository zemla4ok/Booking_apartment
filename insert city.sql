--insert city
CREATE PROCEDURE InsrtCity
	@name NVARCHAR(30)
	as begin
		INSERT INTO CITY (NAME)
			VALUES (@name);
	end;

exec InsrtCity @name='Washington';