#!/bin/bash
# Publicerar en modul till studerande-repot.
# Publicerar: notes/, exercises/, examples/, tentafragor/, termer/ (kursnivå), README.md
# Publicerar INTE: _teacher/
#
# Användning:
#   bash _scripts/publish_week.sh kurs-01-grundlaggande-oop 02_syntax_och_variabler
#
# Kör från yrgo-kursmaterial/

set -e

KURS="${1:?Ange kursmapp, ex: kurs-01-grundlaggande-oop}"
MODUL="${2:?Ange modulmapp, ex: 02_syntax_och_variabler}"
STUDENT_REPO="/home/nionit/git/Skolor/YRGO/clo26/studerande"
SRC_KURS="$(pwd)/${KURS}"
SRC_MODUL="${SRC_KURS}/${MODUL}"
DST_MODUL="${STUDENT_REPO}/${KURS}/${MODUL}"

if [ ! -d "$SRC_MODUL" ]; then
  echo "Fel: Hittade inte $SRC_MODUL"
  exit 1
fi

echo "Publicerar ${KURS}/${MODUL}:"

# README för modulen
if [ -f "${SRC_MODUL}/README.md" ]; then
  mkdir -p "$DST_MODUL"
  cp "${SRC_MODUL}/README.md" "$DST_MODUL/"
  echo "  ✅ ${MODUL}/README.md"
fi

# lectures/
if [ -d "${SRC_MODUL}/lectures" ]; then
  mkdir -p "${DST_MODUL}/lectures"
  cp -r "${SRC_MODUL}/lectures/." "${DST_MODUL}/lectures/"
  echo "  🎤 ${MODUL}/lectures/ ($(ls "${SRC_MODUL}/lectures" | wc -l) filer)"
fi

# notes/
if [ -d "${SRC_MODUL}/notes" ]; then
  mkdir -p "${DST_MODUL}/notes"
  cp -r "${SRC_MODUL}/notes/." "${DST_MODUL}/notes/"
  echo "  📄 ${MODUL}/notes/ ($(ls "${SRC_MODUL}/notes" | wc -l) filer)"
fi

# exercises/
if [ -d "${SRC_MODUL}/exercises" ]; then
  mkdir -p "${DST_MODUL}/exercises"
  cp -r "${SRC_MODUL}/exercises/." "${DST_MODUL}/exercises/"
  echo "  📝 ${MODUL}/exercises/ ($(ls "${SRC_MODUL}/exercises" | wc -l) filer)"
fi

# examples/ (refererade från notes och övningar)
if [ -d "${SRC_MODUL}/examples" ]; then
  mkdir -p "${DST_MODUL}/examples"
  cp -r "${SRC_MODUL}/examples/." "${DST_MODUL}/examples/"
  echo "  💻 ${MODUL}/examples/ ($(ls "${SRC_MODUL}/examples" | wc -l) filer)"
fi

# tentafrågor/ (kursnivå — alla modulers frågor samlas i en central mapp)
if [ -d "${SRC_MODUL}/tentafragor" ]; then
  mkdir -p "${STUDENT_REPO}/${KURS}/tentafrågor"
  cp -r "${SRC_MODUL}/tentafragor/." "${STUDENT_REPO}/${KURS}/tentafrågor/"
  echo "  📋 tentafrågor/ ($(ls "${SRC_MODUL}/tentafragor" | wc -l) filer kopierade från ${MODUL}/tentafragor/)"
fi

# termer/ (på kursnivå, inte modullnivå)
if [ -d "${SRC_KURS}/termer" ]; then
  mkdir -p "${STUDENT_REPO}/${KURS}/termer"
  cp -r "${SRC_KURS}/termer/." "${STUDENT_REPO}/${KURS}/termer/"
  echo "  📚 termer/ ($(ls "${SRC_KURS}/termer" | wc -l) filer)"
fi

cd "$STUDENT_REPO"
git add -A
git commit -m "publish: ${KURS}/${MODUL}" || echo "(Inga ändringar att committa)"
git push origin main

echo ""
echo "Klart! ${KURS}/${MODUL} är publicerad."
