FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV AzureAD__TenantId ""
ENV AzureAD__ClientId ""
ENV AzureAD__ClientSecret ""
ENV Secrets__KeyVault ""

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN curl -fsSL https://deb.nodesource.com/setup_16.x | bash && \
    apt-get install -y nodejs && \
    npm install -g sass

WORKDIR /src
COPY ["src/BWHazel.Api.Web/BWHazel.Api.Web.csproj", "BWHazel.Api.Web/"]
COPY ["src/BWHazel.Api.Core/BWHazel.Api.Core.csproj", "BWHazel.Api.Core/"]
RUN dotnet restore "BWHazel.Api.Web/BWHazel.Api.Web.csproj"
COPY . .
WORKDIR "/src/BWHazel.Api.Web"

RUN dotnet build "BWHazel.Api.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BWHazel.Api.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BWHazel.Api.Web.dll"]

FROM base AS deploy
WORKDIR /app
COPY dist .
ENTRYPOINT ["dotnet", "BWHazel.Api.Web.dll"]