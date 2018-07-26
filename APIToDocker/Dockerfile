FROM microsoft/aspnetcore-build:2.0
WORKDIR /app/src

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

WORKDIR /app/src/out

ENTRYPOINT ["dotnet", "APIToDocker.dll"]

EXPOSE 3100
RUN chmod -R a+wrXs /app

ENV ASPNETCORE_URLS=http://+:3100

#ENV IAMATESTENV=TRUE

ENV DB_HOST=mssql,1433
ENV DB_USER=sa
ENV DB_PASSWORD=P@ssw0rd