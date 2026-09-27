#!/bin/bash
# Installera git-hooks i studerande-repot.
# Kör en gång efter ny klon: bash _scripts/install-hooks.sh
set -e

HOOKS_DIR="$(git rev-parse --show-toplevel)/.git/hooks"
SCRIPTS_DIR="$(git rev-parse --show-toplevel)/_scripts/hooks"

cp "$SCRIPTS_DIR/pre-commit" "$HOOKS_DIR/pre-commit"
chmod +x "$HOOKS_DIR/pre-commit"

echo "Hooks installerade:"
echo "  pre-commit — blockerar _teacher/ och solutions/ vid commit"
