docker exec -it my_postgres bash
pg_dump -U evangelos.chavlis -d weather_db -t 'geography_natural."ClimateZones"' > /tmp/climatezones.sql
exit
docker cp my_postgres:/tmp/climatezones.sql C:\Users\user\repos\climatezones.sql


docker cp C:\Users\user\repos\climatezones.sql my_postgres:/tmp/climatezones.sql
docker exec -it my_postgres bash
psql -U evangelos.chavlis -d weather_db -f /tmp/climatezones.sql
psql -U evangelos.chavlis -d weather_db
SELECT * FROM geography_natural."ClimateZones";