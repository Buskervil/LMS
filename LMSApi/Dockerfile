FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY [".", "LMSApi"]
RUN dotnet restore "LMSApi/LMSApi.csproj"
COPY . .
WORKDIR "/src/LMSApi"
RUN dotnet build "LMSApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LMSApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LMSApi.dll"]
