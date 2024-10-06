FROM mcr.microsoft.com/devcontainers/dotnet:8.0-bookworm AS build

WORKDIR /local/src/vigor
COPY *.sln .
COPY Vigor.Common.Auth.Keycloak/*.csproj ./Vigor.Common.Auth.Keycloak/
COPY Vigor.Common.Db.PostgreSql/*.csproj ./Vigor.Common.Db.PostgreSql/
COPY Vigor.Common.Db.Repository/*.csproj ./Vigor.Common.Db.Repository/
COPY Vigor.Common.Exception/*.csproj ./Vigor.Common.Exception/
COPY Vigor.Common.Extensions.AspNetCore/*.csproj ./Vigor.Common.Extensions.AspNetCore/
COPY Vigor.Common.Extensions.Logging/*.csproj ./Vigor.Common.Extensions.Logging/
COPY Vigor.Common.Extensions.System/*.csproj ./Vigor.Common.Extensions.System/
COPY Vigor.Common.JsonApi/*.csproj ./Vigor.Common.JsonApi/
COPY Vigor.Common.Queue.Redis/*.csproj ./Vigor.Common.Queue.Redis/
COPY Vigor.Common.Util/*.csproj ./Vigor.Common.Util/
COPY Vigor.Common.Util.Test/*.csproj ./Vigor.Common.Util.Test/
COPY Vigor.Core.Platform.Api.Http/*.csproj ./Vigor.Core.Platform.Api.Http/
COPY Vigor.Core.Platform.Api.Queue/*.csproj ./Vigor.Core.Platform.Api.Queue/
COPY Vigor.Core.Platform.Common.Auth.Keycloak/*.csproj ./Vigor.Core.Platform.Common.Auth.Keycloak/
COPY Vigor.Core.Platform.Db/*.csproj ./Vigor.Core.Platform.Db/
COPY Vigor.Core.Platform.Db.Broadcast/*.csproj ./Vigor.Core.Platform.Db.Broadcast/
COPY Vigor.Core.Platform.Db.Entities/*.csproj ./Vigor.Core.Platform.Db.Entities/
COPY Vigor.Core.Platform.Domain.Facility/*.csproj ./Vigor.Core.Platform.Domain.Facility/
COPY Vigor.Core.Platform.Domain.Program/*.csproj ./Vigor.Core.Platform.Domain.Program/
RUN dotnet restore Vigor.Core.sln

COPY . .

WORKDIR /local/src/vigor/Vigor.Core.Platform.Api.Http
RUN dotnet publish -o /app

# ---------------------------------------------------------------------

FROM mcr.microsoft.com/devcontainers/dotnet:8.0-bookworm

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT [ "dotnet" ]
CMD [ "Vigor.Core.Platform.Api.Http.dll" ]