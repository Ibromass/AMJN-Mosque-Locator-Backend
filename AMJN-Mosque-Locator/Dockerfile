FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["AMJN-Mosque-Locator.csproj", "./"]
RUN dotnet restore "AMJN-Mosque-Locator.csproj"

COPY . .
RUN dotnet publish "AMJN-Mosque-Locator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

EXPOSE 10000

COPY --from=build /app/publish .
ENTRYPOINT ["sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT:-10000} exec dotnet AMJN-Mosque-Locator.dll"]
