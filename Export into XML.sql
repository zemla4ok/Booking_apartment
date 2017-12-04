exec master.dbo.sp_configure 'show advanced options', 1;
RECONFIGURE;
exec master.dbo.sp_configure 'xp_cmdshell', 1;
RECONFIGURE;


ALTER PROCEDURE ExportToXML
	@path nvarchar(256),
	@rc bit output
AS BEGIN
	set @rc= 0;
	BEGIN TRANSACTION
		declare @sql nvarchar(500) =
			'bcp "SELECT [ID], [APARTMENTS_ID], [DEFAULT_COST], [ARRIVIG_DATE], [EVICTION_DATE], [IS_EARLY], [IS_DOSEAGE], [RESERVED_PLACES], [RESERVATION_DATE] '+
			'FROM APARTMENT_LIST WHERE RESERVATION_DATE = CAST(GETDATE() as date) '+
			'FOR XML PATH(''List''), ROOT(''Root'')" queryout "'+@path+'" -d BookingApartment -w -T';
		EXEC xp_cmdshell @sql;
		set @rc = 1;
	COMMIT;
END;

exec ExportToXML 'D:\aplist.xml'