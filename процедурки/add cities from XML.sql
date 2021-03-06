﻿CREATE PROCEDURE addCityesFromXML
	@path nvarchar(256),
	@rc int output
AS BEGIN
	SET @rc = 1;
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	BEGIN TRY
		BEGIN TRAN
			declare @results table (x xml)		
			declare @sql nvarchar(300)=
				'SELECT 
					CAST(REPLACE(CAST(x AS VARCHAR(MAX)), ''encoding="utf-16"'', ''encoding="utf-8"'') AS XML)
					FROM OPENROWSET(BULK '''+@path+''', SINGLE_BLOB) AS T(x)'; 

			INSERT INTO @results EXEC (@sql);
			declare @xml XML = (SELECT  TOP 1 x from  @results);

			INSERT INTO CITY(NAME)
				SELECT 
					C3.value('name[1]', 'NVARCHAR(30)') AS NAME
				FROM @xml.nodes('dataroot/city') AS T3(C3);
		COMMIT;
	END TRY
	BEGIN CATCH
		SET @rc = 0;
		ROLLBACK;
	END CATCH;
END;

drop procedure addCityesFromXML;
exec addCityesFromXML
	@path = 'C:\Users\Dmitry\Desktop\Booking_apartment\CITY.xml',
	@rc=0;

SELECT * FROM CITY;
delete city