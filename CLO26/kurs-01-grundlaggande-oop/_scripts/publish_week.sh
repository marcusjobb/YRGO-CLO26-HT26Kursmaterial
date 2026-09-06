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

# Prefix från git-root till kursmappen (ex: CLO26/kurs-01-grundlaggande-oop/)
GIT_PREFIX=$(git rev-parse --show-prefix)
# Kursens mappnamn i student-repot (ex: kurs-01-grundlaggande-oop)
COURSE_DIR=$(basename "$(pwd)")

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
  echo "Lägg till den med: git remote add studerande git@github-jobb:marcusjobb/CLO26-Coursematerial.git"
  exit 1
fi

# Skapa ett temporärt worktree för student-branchen
# (undviker att byta branch i den aktiva working tree)
git fetch "$STUDENT_REMOTE"
WORKTREE=$(mktemp -d)
git worktree add "$WORKTREE" -B "$WORK_BRANCH" "$STUDENT_REMOTE/main"

PUBLISHED_FILES=()

copy_file() {
  local src_key="$1"   # sökväg i teacher-repot (med prefix)
  local dest_rel="$2"  # relativ sökväg i student-repot under COURSE_DIR/

  local dest="$WORKTREE/$COURSE_DIR/$dest_rel"
  mkdir -p "$(dirname "$dest")"
  git show "main:${src_key}" > "$dest"
  PUBLISHED_FILES+=("$COURSE_DIR/$dest_rel")
}

# Kopiera filer från main-branchen i lärarrepot
while IFS= read -r file; do
  file=$(echo "$file" | xargs)
  [ -z "$file" ] && continue

  if [[ "$file" == *_teacher.md ]]; then
    echo "  🔒 Blockerad (lärarfil): $file"
    continue
  fi

  if git show "main:${GIT_PREFIX}${file}" &>/dev/null; then
    copy_file "${GIT_PREFIX}${file}" "$file"
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
    if git show "main:${GIT_PREFIX}${teacher_file}" &>/dev/null; then
      copy_file "${GIT_PREFIX}${teacher_file}" "$facit_file"
      echo "  ✅ $facit_file"
    fi
  done <<< "$FILES"
fi

# Committa och pusha från worktree
(
  cd "$WORKTREE"
  if [ ${#PUBLISHED_FILES[@]} -gt 0 ]; then
    git add "${PUBLISHED_FILES[@]}"
  fi
  FACIT_SUFFIX=$( [ "$PUBLISH_FACIT" = true ] && echo " (med facit)" || echo "" )
  git commit -m "Publicerar vecka ${WEEK}${FACIT_SUFFIX}" || echo "(Inga ändringar att committa)"
  git push "$STUDENT_REMOTE" "${WORK_BRANCH}:main"
)

# Städa upp worktree och branch
git worktree remove "$WORKTREE" --force
git branch -D "$WORK_BRANCH" 2>/dev/null || true

echo ""
echo "Klart! Vecka $WEEK är publicerad till $COURSE_DIR/ i student-repot."

# Generera dayplanners för kommande vecka
DAYPLAN_DIR="_teacher/dayplanners"
mkdir -p "$DAYPLAN_DIR"

if command -v dayplanner &>/dev/null; then
  echo ""
  echo "Genererar dayplanners för vecka $WEEK..."
  for dag in 1 2 3; do
    outfile="${DAYPLAN_DIR}/vecka_${WEEK}_dag${dag}.md"
    dayplanner "$WEEK" "$dag" "$COURSE_DIR" > "$outfile" 2>/dev/null && echo "  📅 $outfile" || echo "  ⚠️  Dag $dag saknar veckoplan"
  done
  echo ""
  echo "Dayplanners klara — öppna _teacher/dayplanners/ måndag morgon."
else
  echo "  (dayplanner hittades inte i PATH — kör 'dayplanner $WEEK 1' manuellt)"
fi
