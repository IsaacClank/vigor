#!/bin/bash

set -e

# Additional initialization should be placed below
psql -v ON_ERROR_STOP=1 --dbname "$POSTGRES_DB" --username "$POSTGRES_USER" <<- EOSQL
EOSQL

