DECLARE @xml XML;
SELECT @xml = CAST(C1 AS xml) FROM OPENROWSET(BULK 'C:\Users\Dmitry\Desktop\курсач\CITY.xml', SINGLE_BLOB) AS T1(C1);


INSERT INTO CITY(NAME)
	SELECT 
		C3.value('name[1]', 'NVARCHAR(100)') AS NAME
	FROM @xml.nodes('dataroot/city') AS T3(C3)

select * from CITY
