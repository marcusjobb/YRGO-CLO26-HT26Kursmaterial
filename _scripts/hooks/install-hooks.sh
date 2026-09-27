#!/bin/bash
# Installera git-hooks i studerande-repot.
# Kör en gång efter ny klon: bash _scripts/hooks/install-hooks.sh
set -e

REPO_ROOT="$(git rev-parse --show-toplevel)"
HOOKS_DIR="$REPO_ROOT/.git/hooks"
SCRIPTS_DIR="$REPO_ROOT/_scripts/hooks"

cp "$SCRIPTS_DIR/pre-commit" "$HOOKS_DIR/pre-commit"
chmod +x "$HOOKS_DIR/pre-commit"

echo "Hook installerad: pre-commit"
echo ""
echo "OBS: Direkta commits är blockerade i det här repot."
echo "Publicera via: bash CLO26/_scripts/publish_week.sh <kurs> <modul>"
