# See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Lab2/Lab2.csproj", "Lab2/"]
RUN dotnet restore "Lab2/Lab2.csproj"
COPY . .
WORKDIR "/src/Lab2"
RUN dotnet build "Lab2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Lab2.dll"]
