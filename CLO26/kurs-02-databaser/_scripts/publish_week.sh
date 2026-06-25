#!/bin/bash
# Publicerar en veckas material till student-repot.
# Användning: bash _scripts/publish_week.sh 03
# Se _scripts/README.md för fullständig dokumentation.

set -e

WEEK=$(printf "%02d" "${1:?Ange veckonummer, ex: bash publish_week.sh 03}")
PLAN="_teacher/vecka_${WEEK}.md"
STUDENT_REMOTE="studerande"
WORK_BRANCH="publish/vecka_${WEEK}"

if [ ! -f "$PLAN" ]; then
  echo "Fel: Hittade inte $PLAN"
  exit 1
fi

# Hämta filer från publish:-sektionen i veckoplansfilen
FILES=$(awk '/^publish:/,/^$/' "$PLAN" | grep '^\s*-' | sed 's/^\s*-\s*//')

if [ -z "$FILES" ]; then
  echo "Fel: Inga filer att publicera i $PLAN"
  exit 1
fi

echo "Publicerar vecka $WEEK:"
echo "$FILES"
echo ""

# Verifiera att studerande-remote finns
if ! git remote get-url "$STUDENT_REMOTE" &>/dev/null; then
  echo "Fel: Remote '$STUDENT_REMOTE' saknas."
  echo "Lägg till den med: git remote add studerande git@github:nionit/kurs-csharp-student.git"
  exit 1
fi

# Skapa en temporär branch baserad på studerande-remotens main
git fetch "$STUDENT_REMOTE"
git checkout -B "$WORK_BRANCH" "$STUDENT_REMOTE/main"

# Kopiera filer från main
while IFS= read -r file; do
  file=$(echo "$file" | xargs)
  [ -z "$file" ] && continue
  if [ -e "../$(git rev-parse --show-toplevel)/$file" ] || git show "main:$file" &>/dev/null; then
    mkdir -p "$(dirname "$file")"
    git checkout main -- "$file"
    echo "  ✅ $file"
  else
    echo "  ⚠️  Hittades inte: $file"
  fi
done <<< "$FILES"

git add -A
git commit -m "Publicerar vecka ${WEEK}" || echo "(Inga ändringar att committa)"
git push "$STUDENT_REMOTE" "${WORK_BRANCH}:main"

# Återgå till main
git checkout main
git branch -D "$WORK_BRANCH"

echo ""
echo "Klart! Vecka $WEEK är publicerad."
