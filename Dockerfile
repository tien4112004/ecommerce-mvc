FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["EcommerceMVC/EcommerceMVC.csproj", "EcommerceMVC/"]
COPY ["EcommerceMVC.Tests/EcommerceMVC.Tests.csproj", "EcommerceMVC.Tests/"]
RUN dotnet restore "EcommerceMVC/EcommerceMVC.csproj"

COPY . .
# COPY "EcommerceMVC/.env" .env

WORKDIR "/src/EcommerceMVC"
RUN dotnet build "EcommerceMVC.csproj" -c Release -o /app/build

RUN dotnet publish "EcommerceMVC.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT [ "dotnet", "EcommerceMVC.dll" ]