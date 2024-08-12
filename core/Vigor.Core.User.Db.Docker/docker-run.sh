#!/bin/bash

set -euo pipefail

PORT="${1:-5430}"

WORKDIR=$(cd $(dirname $0); pwd)
cd $WORKDIR &>/dev/null

docker build --tag vigor/core/user/db:latest .
docker run \
  --name vigor-core-user-db \
  --publish "${PORT}:5432" \
  --detach --rm \
  --env-file .env \
  vigor/core/user/db:latest

cd - &>/dev/null