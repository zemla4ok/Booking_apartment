CREATE PROCEDURE GetSumForEviction
	@book_id int,
	@rc int output
AS BEGIN
	declare @places int;
	declare @evic_date date;
	declare @arriv_date date;
	declare @reserv_date date;
	declare @cost int;
	declare @is_early bit;
	declare @koeff decimal = 0;

	SELECT @evic_date = EVICTION_DATE,
		   @arriv_date = ARRIVIG_DATE,
		   @reserv_date = RESERVATION_DATE,
		   @cost = DEFAULT_COST,
		   @is_early = IS_EARLY,
		   @places = RESERVED_PLACES
			FROM APARTMENT_LIST WHERE ID = @book_id;

	IF @is_early = 1
	BEGIN
		set @koeff = 0.5;
	END

	set @rc = @places * ((DAY(@evic_date) - DAY(@arriv_date)) * @cost
					  + (DAY(@arriv_date) - DAY(@reserv_date)) * @cost * 0.5) +
					  @koeff * @cost
END;
