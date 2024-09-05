FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet build "./build/_build.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./build/_build.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM publish AS final
WORKDIR /src/Streetcode
CMD ["dotnet", "./Streetcode.WebApi/bin/Release/net6.0/Streetcode.WebApi.dll"]
