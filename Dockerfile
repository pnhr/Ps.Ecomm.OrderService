FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ps.Ecomm.OrderService/Ps.Ecomm.OrderService.csproj", "Ps.Ecomm.OrderService/"]
RUN dotnet restore "Ps.Ecomm.OrderService/Ps.Ecomm.OrderService.csproj"
COPY . .
WORKDIR "/src/Ps.Ecomm.OrderService"
RUN dotnet build "Ps.Ecomm.OrderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ps.Ecomm.OrderService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ps.Ecomm.OrderService.dll"]
