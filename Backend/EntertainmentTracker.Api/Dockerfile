FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY EntertainmentTracker.slnx .
COPY EntertainmentTracker.Api/EntertainmentTracker.Api.csproj EntertainmentTracker.Api/
COPY EntertainmentTracker.Application/EntertainmentTracker.Application.csproj EntertainmentTracker.Application/
COPY EntertainmentTracker.Domain/EntertainmentTracker.Domain.csproj EntertainmentTracker.Domain/
COPY EntertainmentTracker.Infrastructure/EntertainmentTracker.Infrastructure.csproj EntertainmentTracker.Infrastructure/

COPY EntertainmentTracker.Api/ EntertainmentTracker.Api/
COPY EntertainmentTracker.Application/ EntertainmentTracker.Application/
COPY EntertainmentTracker.Domain/ EntertainmentTracker.Domain/
COPY EntertainmentTracker.Infrastructure/ EntertainmentTracker.Infrastructure/

RUN dotnet publish EntertainmentTracker.Api/EntertainmentTracker.Api.csproj \
    --configuration Release \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "EntertainmentTracker.Api.dll"]