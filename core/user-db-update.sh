#!/bin/bash

set -euo pipefail

WORKDIR=$(cd $(dirname $0); pwd)
cd "${WORKDIR}/Vigor.Core.User.Db/" &>/dev/null

env DB_CONNECTION="${USER_DB}" dotnet ef database update

cd - &>/dev/null