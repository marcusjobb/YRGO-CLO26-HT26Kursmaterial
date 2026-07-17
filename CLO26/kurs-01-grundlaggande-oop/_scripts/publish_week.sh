#!/bin/bash
# Publicerar en veckas material till student-repot.
# Användning: bash _scripts/publish_week.sh 03
# Se _scripts/README.md för fullständig dokumentation.

set -e

WEEK=$(printf "%02d" "${1:?Ange veckonummer, ex: bash publish_week.sh 03}")
PUBLISH_FACIT=false
for arg in "$@"; do
  [[ "$arg" == "--facit" ]] && PUBLISH_FACIT=true
done

PLAN="_teacher/week_${WEEK}.md"
STUDENT_REMOTE="studerande"
WORK_BRANCH="publish/week_${WEEK}"

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

  # Blockera lärarfiler — publiceras aldrig direkt
  if [[ "$file" == *_teacher.md ]]; then
    echo "  🔒 Blockerad (lärarfil): $file"
    continue
  fi

  if [ -e "../$(git rev-parse --show-toplevel)/$file" ] || git show "main:$file" &>/dev/null; then
    mkdir -p "$(dirname "$file")"
    git checkout main -- "$file"
    echo "  ✅ $file"
  else
    echo "  ⚠️  Hittades inte: $file"
  fi
done <<< "$FILES"

# Publicera facit-filer om --facit är satt
if [ "$PUBLISH_FACIT" = true ]; then
  echo ""
  echo "Publicerar facit (--facit):"
  while IFS= read -r file; do
    file=$(echo "$file" | xargs)
    [ -z "$file" ] && continue
    [[ "$file" == *_teacher.md ]] && continue
    teacher_file="${file%.md}_teacher.md"
    facit_file="${file%.md}_facit.md"
    if git show "main:$teacher_file" &>/dev/null; then
      mkdir -p "$(dirname "$facit_file")"
      git checkout main -- "$teacher_file"
      mv "$teacher_file" "$facit_file"
      echo "  ✅ $facit_file"
    fi
  done <<< "$FILES"
fi

git add -A
git commit -m "Publicerar vecka ${WEEK}${PUBLISH_FACIT:+ (med facit)}" || echo "(Inga ändringar att committa)"
git push "$STUDENT_REMOTE" "${WORK_BRANCH}:main"

# Återgå till main
git checkout main
git branch -D "$WORK_BRANCH"

echo ""
echo "Klart! Vecka $WEEK är publicerad."

# Generera dayplanners för kommande vecka
DAYPLAN_DIR="_teacher/dayplanners"
mkdir -p "$DAYPLAN_DIR"

if command -v dayplanner &>/dev/null; then
  echo ""
  echo "Genererar dayplanners för vecka $WEEK..."
  KURS=$(basename "$(pwd)")
  for dag in 1 2 3; do
    outfile="${DAYPLAN_DIR}/vecka_${WEEK}_dag${dag}.md"
    dayplanner "$WEEK" "$dag" "$KURS" > "$outfile" 2>/dev/null && echo "  📅 $outfile" || echo "  ⚠️  Dag $dag saknar veckoplan"
  done
  echo ""
  echo "Dayplanners klara — öppna $_teacher/dayplanners/ måndag morgon."
else
  echo "  (dayplanner-scriptet hittades inte i PATH — kör 'dayplanner $WEEK 1' manuellt)"
fi
