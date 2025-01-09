FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
    
#    RUN apt-get update && \
#        apt-get install -y curl apt-transport-https gnupg && \
#        curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
#        curl https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
#        apt-get update && \
#        ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
#        echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["CRUD/CRUD.csproj", "CRUD/"]
RUN dotnet restore "CRUD/CRUD.csproj"

COPY . .
WORKDIR "/src/CRUD"

RUN dotnet build "CRUD.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CRUD.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUD.dll"]