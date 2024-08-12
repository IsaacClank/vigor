#!/bin/bash

set -euo pipefail

PORT="${1:-6379}"

WORKDIR=$(cd $(dirname $0); pwd)
cd $WORKDIR &>/dev/null

docker build --tag vigor/core/queue:latest .
docker run \
  --name vigor-core-queue \
  --publish "${PORT}:6379" \
  --detach --rm \
  --env-file .env \
  vigor/core/queue:latest

cd - &>/dev/null