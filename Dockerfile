FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ./*.sln .
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
COPY test/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p test/${file%.*}/ && mv $file test/${file%.*}/; done
RUN dotnet restore
COPY . .
RUN dotnet build

FROM build as publish
RUN dotnet publish "./src/Dotnet.Sonar.Sample.Api/Dotnet.Sonar.Sample.Api.csproj" --no-restore -c Release -o /app

FROM base as runtime
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dotnet.Sonar.Sample.Api.dll"]

FROM build AS unit-tests
CMD dotnet test \
    --no-build \
    -v quiet \
    --nologo \
    --filter "Category=UnitTest" \
    --logger trx \
    --results-directory ./TestResults \
    --settings coverlet.runsettings

FROM build AS integration-tests
CMD dotnet test \
    --no-build \
    -v quiet \
    --nologo \
    --filter "Category=IntegrationTest" \
    --results-directory ./TestResults \
    --logger trx \
    --settings coverlet.runsettings
