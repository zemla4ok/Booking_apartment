
SELECT * FROM CITY

SELECT * FROM HOTELS

SELECT COUNT(*) FROM APARTMENTS

SELECT CURRENT_COST FROM APARTMENTS 

CREATE INDEX ind_cost ON APARTMENTS(CURRENT_COST);