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

RUN chmod -R a+wrXs /app
