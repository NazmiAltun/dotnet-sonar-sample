#!/bin/bash
docker buildx build \
  --target unit-tests \
  --tag  dotnet-sonar-sample-unit:latest \
  .

docker buildx build \
  --target integration-tests  \
  --tag  dotnet-sonar-sample-integration:latest \
  .

docker run \
  --rm \
  -it \
  -v /${PWD}/TestResults:/app/TestResults \
  dotnet-sonar-sample-unit:latest

docker run \
  --rm \
  -it \
  -v /${PWD}/TestResults:/app/TestResults \
  dotnet-sonar-sample-integration:latest
