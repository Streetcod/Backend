FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

RUN apt-get update     && apt-get install -y --no-install-recommends     curl     gpg     && rm -rf /var/lib/apt/lists/*
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG Configuration=Release

COPY ./Streetcode/*.sln ./
COPY ./Streetcode/Streetcode.WebApi/*.csproj ./Streetcode.WebApi/
COPY ./Streetcode/Streetcode.BLL/*.csproj ./Streetcode.BLL/
COPY ./Streetcode/Streetcode.DAL/*.csproj ./Streetcode.DAL/
COPY ./Streetcode/Streetcode.XUnitTest/*.csproj ./Streetcode.XUnitTest/
COPY ./Streetcode/Streetcode.XIntegrationTest/*.csproj ./Streetcode.XIntegrationTest/
COPY ./Streetcode/DbUpdate/*.csproj ./DbUpdate/
COPY ./houses.zip ./
RUN dotnet restore

COPY ./Streetcode ./
RUN dotnet build -c Release -o /app/build


FROM build AS publish
RUN dotnet publish  -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish ./
RUN dotnet dev-certs https
LABEL atom=Streetcode

CMD ["dotnet", "Streetcode.WebApi.dll"]
