FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY *.csproj ./
#
RUN dotnet restore 

COPY . ./
RUN dotnet build "/src/VirtualHerbarium.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/VirtualHerbarium.Backend.csproj" -c Release -o /app/publish

WORKDIR /app/publish

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "VirtualHerbarium.Backend.dll"]