CREATE PROCEDURE InsertUser
	@name nvarchar(20),
	@surname nvarchar(20),
	@passport_num nvarchar(20)
	AS BEGIN
		INSERT INTO USERS (NAME, SURNAME, PASSPORT_NUM)
			VALUES(@name, @surname, @passport_num);
	END;

exec InsertUser
	@name = 'dima',
	@surname = 'kot',
	@passport_num = 'sadsad';

SELECT * FROM USERS;