FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["JsonToPdf.net/JsonToPdf.net.csproj", "JsonToPdf.net/"]
RUN dotnet restore "JsonToPdf.net/JsonToPdf.net.csproj"
COPY . .
WORKDIR "/src/JsonToPdf.net"
RUN dotnet build "JsonToPdf.net.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JsonToPdf.net.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JsonToPdf.net.dll"]