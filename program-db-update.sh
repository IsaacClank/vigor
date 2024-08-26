#!/bin/bash

set -euo pipefail

WORKDIR=$(cd $(dirname $0); pwd)
cd "${WORKDIR}/Vigor.Core.Program.Db/" &>/dev/null

env DB_CONNECTION="${PROGRAM_DB}" dotnet ef database update

cd - &>/dev/null
