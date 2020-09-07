FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu
EXPOSE 1433
ENV SA_PASSWORD @Ab1234%56Fv@
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express
COPY Pokedex.Database/dbpokemon.sql Pokedex.Database/
RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 90 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '@Ab1234%56Fv@' -i Pokedex.Database/dbpokemon.sql \
    && pkill sqlservr 

