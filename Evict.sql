CREATE PROCEDURE EvictClient
	@book_id int,
	@rc bit output
AS BEGIN
	set @rc = 1
	BEGIN TRY
		declare @is_ev bit;
		SELECT @is_ev = IS_EVICTED FROM APARTMENT_LIST WHERE ID = @book_id;
		
		IF @is_ev = 1
		BEGIN
			set @rc = 0;
			return;
		END
			
		declare @apart_id int;
		declare @places int;
	
		SELECT  @apart_id = APARTMENTS_ID,
				@places = RESERVED_PLACES
				FROM APARTMENT_LIST WHERE ID = @book_id;
	
		declare @apart_places int;
		SELECT @apart_places = FREE_PLACES FROM APARTMENTS WHERE ID = @apart_id;
	
		UPDATE APARTMENTS SET FREE_PLACES = @apart_places + @places WHERE ID = @apart_id;
		UPDATE APARTMENTS SET IS_CLOSE = 0 WHERE ID = @apart_id;
		UPDATE APARTMENT_LIST SET IS_EVICTED = 1 WHERE ID = @apart_id;
	END TRY
	BEGIN CATCH
		set @rc = 0;
	END CATCH
END;

drop proc EvictClient