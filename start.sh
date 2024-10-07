#!/bin/sh
dotnet run --project /build/_build.csproj
dotnet /app/Streetcode.WebApi.dll
wait
