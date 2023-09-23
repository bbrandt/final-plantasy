# Adjust DOTNET_OS_VERSION as desired
ARG DOTNET_OS_VERSION=""
ARG DOTNET_SDK_VERSION=7.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_SDK_VERSION}${DOTNET_OS_VERSION} AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
RUN npm install -g npm@10.1.0

WORKDIR /src

# copy everything
COPY . ./
# restore as distinct layers
RUN dotnet restore Web/Web.csproj
# build and publish a release
RUN dotnet publish Web/Web.csproj -c Release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_SDK_VERSION}

RUN apt-get update -y && apt-get install -y ca-certificates fuse3 sqlite3
COPY --from=flyio/litefs:0.5 /usr/local/bin/litefs /usr/local/bin/litefs

ENV ASPNETCORE_URLS http://+:8081
ENV ASPNETCORE_ENVIRONMENT Production
EXPOSE 8081

WORKDIR /app
COPY --from=build /app .
RUN cp ./litefs.yml /etc/litefs.yml
ENTRYPOINT litefs mount
