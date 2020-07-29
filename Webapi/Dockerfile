#FROM microsoft/aspnetcore:latest
#WORKDIR /app
#COPY ["Webapi.csproj", ""]
#ENV ASPNETCORE_URLS http://*:8000
#EXPOSE 8000
#ENTRYPOINT ["dotnet", "Webapi.dll"]


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8000
#EXPOSE 443
ENV ASPNETCORE_URLS http://*:8000
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Webapi.csproj", ""]
RUN dotnet restore "./Webapi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Webapi.dll"]