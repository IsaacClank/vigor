#!/bin/bash

set -euo pipefail

WORKDIR=$(cd $(dirname $0); pwd)

cd "${WORKDIR}/Vigor.Core.Platform.Db/" &>/dev/null
env DB_CONNECTION="${PLATFORM_DB}" dotnet ef database update

cd - &>/dev/null
