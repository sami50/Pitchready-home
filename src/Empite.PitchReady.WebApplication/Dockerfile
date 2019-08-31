FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Empite.PitchReady.WebApplication.csproj", ""]
RUN dotnet restore "./Empite.PitchReady.WebApplication.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Empite.PitchReady.WebApplication.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Empite.PitchReady.WebApplication.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Empite.PitchReady.WebApplication.dll"]