FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY StartupLanches.sln ./
COPY StartupLanches/StartupLanches.csproj StartupLanches/
COPY StartupLanches.BLL/StartupLanches.BLL.csproj StartupLanches.BLL/
COPY StartupLanches.Model/StartupLanches.Model.csproj StartupLanches.Model/
COPY StartupLanches.DAL/StartupLanches.DAL.csproj StartupLanches.DAL/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/StartupLanches
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StartupLanches.dll"]
