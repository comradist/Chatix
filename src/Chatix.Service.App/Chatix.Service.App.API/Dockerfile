FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /src

COPY . ./
WORKDIR src/Chatix.Service.App/Chatix.Service.App.API;

RUN dotnet restore

RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Chatix.Service.App.API.dll" ]

