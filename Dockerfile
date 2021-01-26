# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /

# copy csproj and restore as distinct layers
COPY / .
RUN dotnet restore -r linux-x64

# copy everything else and build app
COPY . ./
WORKDIR /Luby.TimeManager.API
RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained false
#RUN dotnet publish -c Release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./Luby.TimeManager.API"]