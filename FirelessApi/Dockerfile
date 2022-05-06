FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FirelessApi/FirelessApi.csproj", "FirelessApi/"]
RUN dotnet restore "FirelessApi/FirelessApi.csproj"
COPY . .
WORKDIR "/src/FirelessApi"
RUN dotnet build "FirelessApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FirelessApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FirelessApi.dll"]
