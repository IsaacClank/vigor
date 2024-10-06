#!/bin/bash

set -e

TOKEN="${1:-}"
HOST="localhost"
PORT=4000

ICON_CHECKMARK="✔"

if [[ -z $TOKEN ]]; then
  echo "No token provided. Exitting."
  exit 1
fi

ansi --down --inverse 'GET /api'
if http --quiet --check-status localhost:4000/api/health Authorization:"Bearer ${TOKEN}"; then
  ansi --forward=4 "OK response: $(ansi --green $ICON_CHECKMARK)"
fi

ansi --down --inverse 'GET /api/health'
if http --quiet --check-status localhost:4000/api/health Authorization:"Bearer ${TOKEN}"; then
  ansi --forward=4 "OK response: $(ansi --green $ICON_CHECKMARK)"
fi
