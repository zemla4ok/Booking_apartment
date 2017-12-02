create table t (x date, y date);

insert into t(x, y)
	Values(DATEFROMPARTS(1997, 11, 10), DATEFROMPARTS(1997, 11, 15));



declare @x1 date;
set @x1 = DATEFROMPARTS(1997, 11, 10);
declare @y1 date;
set @y1 = DATEFROMPARTS(1997, 11, 15);

declare @res int;
set @res = DAY(@x1) - DAY(@y1);
print @res;