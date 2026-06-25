#!/usr/bin/env python3
"""
Inventerar /home/marcus/git/Old_courses/ och skapar en sökbar JSON-databas.

Körning:
  python3 inventory_old_courses.py
  python3 inventory_old_courses.py --output /annan/sökväg.json

Söka i resultatet med jq:
  jq '.modules[] | select(.topics[] | contains("design patterns"))' old_courses_inventory.json
  jq '.modules[] | select(.language == "csharp") | .path' old_courses_inventory.json
  jq '.modules[] | select(.course_mapping[] | contains("kurs-03"))' old_courses_inventory.json
  jq '.modules[] | select(.has_exercises == true and .language == "csharp") | .path' old_courses_inventory.json
  jq '.modules[] | select((.course_mapping | index("kurs-01") != null) and .language == "csharp") | .path' old_courses_inventory.json
"""

import os
import json
import argparse
from pathlib import Path
from datetime import date

BASE = Path("/home/marcus/git/Old_courses")
OUTPUT_DEFAULT = Path("/home/nionit/git/YRGO/CLO26/_teacher/old_courses_inventory.json")

# ── Topic detection: path-fragment → topic tags ──────────────────────────────
TOPIC_HINTS = {
    "variabler": ["variabler", "datatyper"],
    "variable": ["variabler", "datatyper"],
    "if": ["if", "villkorssatser"],
    "loop": ["loopar", "iteration"],
    "loopar": ["loopar", "iteration"],
    "iterationer": ["loopar", "iteration"],
    "metoder": ["metoder"],
    "methods": ["metoder"],
    "switch": ["switch"],
    "arrays": ["arrays", "datastrukturer"],
    "array": ["arrays", "datastrukturer"],
    "list": ["list", "datastrukturer"],
    "datastrukturer": ["datastrukturer", "arrays", "list", "dictionary"],
    "datastructures": ["datastrukturer", "arrays", "list", "dictionary"],
    "dictionary": ["dictionary", "datastrukturer"],
    "oop": ["oop", "klasser", "objekt"],
    "klasser": ["klasser", "oop"],
    "classes": ["klasser", "oop"],
    "arv": ["arv", "inheritance", "oop"],
    "inheritance": ["arv", "inheritance", "oop"],
    "polymorfism": ["polymorfism", "oop"],
    "polymorphism": ["polymorfism", "oop"],
    "abstrakt": ["abstrakt klass", "oop"],
    "abstract": ["abstrakt klass", "oop"],
    "interface": ["interface", "oop"],
    "inkapsling": ["inkapsling", "oop"],
    "encapsulation": ["inkapsling", "oop"],
    "designpattern": ["design patterns"],
    "design_pattern": ["design patterns"],
    "patterns": ["design patterns"],
    "factory": ["design patterns", "factory"],
    "singleton": ["design patterns", "singleton"],
    "observer": ["design patterns", "observer"],
    "strategy": ["design patterns", "strategy"],
    "solid": ["solid", "clean code"],
    "cleancode": ["clean code"],
    "clean_code": ["clean code"],
    "kodkvalite": ["clean code", "kodkvalitet"],
    "refactoring": ["refactoring", "clean code"],
    "sql": ["sql", "databaser"],
    "databas": ["databaser", "sql"],
    "database": ["databaser", "sql"],
    "entityframework": ["entity framework", "orm", "databaser"],
    "ef": ["entity framework", "orm"],
    "mongodb": ["mongodb", "nosql", "databaser"],
    "tdd": ["tdd", "testning"],
    "test": ["testning"],
    "testing": ["testning"],
    "xunit": ["xunit", "testning"],
    "junit": ["junit", "testning"],
    "mock": ["mocking", "testning"],
    "git": ["git"],
    "github": ["git", "github"],
    "cicd": ["ci/cd", "github actions"],
    "ci_cd": ["ci/cd"],
    "cloud": ["cloud", "azure"],
    "azure": ["azure", "cloud"],
    "api": ["api", "rest"],
    "rest": ["api", "rest"],
    "aspnet": ["asp.net", "webb"],
    "asp.net": ["asp.net", "webb"],
    "mvc": ["mvc", "asp.net", "webb"],
    "filhantering": ["filhantering"],
    "pseudokod": ["pseudokod", "problemlösning"],
    "algoritm": ["algoritmer"],
    "algorithm": ["algoritmer"],
    "scrum": ["scrum", "agile"],
    "agile": ["agile"],
    "kanban": ["kanban", "agile"],
    "uml": ["uml"],
    "enum": ["enum", "datastrukturer"],
    "asynkron": ["async/await"],
    "async": ["async/await"],
    "linq": ["linq"],
}

# ── Course mapping: topic tags → kurs ────────────────────────────────────────
COURSE_MAP = {
    "variabler":         ["kurs-01"],
    "datatyper":         ["kurs-01"],
    "if":                ["kurs-01"],
    "villkorssatser":    ["kurs-01"],
    "loopar":            ["kurs-01"],
    "iteration":         ["kurs-01"],
    "metoder":           ["kurs-01"],
    "switch":            ["kurs-01"],
    "arrays":            ["kurs-01"],
    "list":              ["kurs-01"],
    "datastrukturer":    ["kurs-01"],
    "dictionary":        ["kurs-01"],
    "enum":              ["kurs-01"],
    "git":               ["kurs-01"],
    "github":            ["kurs-01"],
    "pseudokod":         ["kurs-01"],
    "oop":               ["kurs-01", "kurs-03"],
    "klasser":           ["kurs-01", "kurs-03"],
    "objekt":            ["kurs-01"],
    "inkapsling":        ["kurs-01"],
    "arv":               ["kurs-03"],
    "inheritance":       ["kurs-03"],
    "polymorfism":       ["kurs-03"],
    "abstrakt klass":    ["kurs-03"],
    "interface":         ["kurs-03"],
    "design patterns":   ["kurs-03"],
    "factory":           ["kurs-03"],
    "singleton":         ["kurs-03"],
    "observer":          ["kurs-03"],
    "strategy":          ["kurs-03"],
    "solid":             ["kurs-03", "kurs-04"],
    "clean code":        ["kurs-03", "kurs-04"],
    "kodkvalitet":       ["kurs-03", "kurs-04"],
    "refactoring":       ["kurs-03", "kurs-04"],
    "sql":               ["kurs-02"],
    "databaser":         ["kurs-02", "kurs-03"],
    "entity framework":  ["kurs-03"],
    "orm":               ["kurs-03"],
    "mongodb":           ["kurs-02"],
    "nosql":             ["kurs-02"],
    "uml":               ["kurs-01", "kurs-03"],
    "tdd":               ["kurs-04"],
    "testning":          ["kurs-04"],
    "xunit":             ["kurs-04"],
    "mocking":           ["kurs-04"],
    "scrum":             ["kurs-04"],
    "agile":             ["kurs-04"],
    "kanban":            ["kurs-04"],
    "ci/cd":             ["kurs-04"],
    "github actions":    ["kurs-04"],
    "api":               ["kurs-03"],
    "rest":              ["kurs-03"],
    "asp.net":           ["kurs-03"],
    "mvc":               ["kurs-03"],
    "cloud":             ["cloud"],
    "azure":             ["cloud"],
    "algoritmer":        ["kurs-03"],
    "linq":              ["kurs-03"],
    "async/await":       ["kurs-03"],
    "filhantering":      ["kurs-03"],
}

def detect_language(path: Path) -> str:
    parts = [p.lower() for p in path.parts]
    if any("java" in p and "javascript" not in p for p in parts):
        return "java"
    if any(p in ("csharp", "c-sharp", "c#", "cs") for p in parts):
        return "csharp"
    # count files
    cs = sum(1 for _ in path.rglob("*.cs")) if path.is_dir() else 0
    java = sum(1 for _ in path.rglob("*.java")) if path.is_dir() else 0
    if cs > java:
        return "csharp"
    if java > cs:
        return "java"
    return "mixed"

def detect_topics(path: Path) -> list:
    topics = set()
    parts = [p.lower().replace("-", "_").replace(" ", "_") for p in path.parts]
    for part in parts:
        for hint, tags in TOPIC_HINTS.items():
            if hint in part:
                topics.update(tags)
    return sorted(topics)

def detect_course_mapping(topics: list) -> list:
    courses = set()
    for topic in topics:
        if topic in COURSE_MAP:
            courses.update(COURSE_MAP[topic])
    return sorted(courses) if courses else ["okänt"]

def count_files(path: Path) -> dict:
    counts = {}
    if not path.is_dir():
        return counts
    for f in path.rglob("*"):
        if f.is_file():
            ext = f.suffix.lower().lstrip(".")
            if ext:
                counts[ext] = counts.get(ext, 0) + 1
    return counts

def detect_structure(path: Path) -> dict:
    if not path.is_dir():
        return {}
    subdirs = {d.name.lower() for d in path.iterdir() if d.is_dir()}
    return {
        "has_lectures":    any(s in subdirs for s in ["lectures", "lecture", "föreläsning", "slides"]),
        "has_exercises":   any(s in subdirs for s in ["exercises", "exercise", "övningar", "ovningar", "övning"]),
        "has_assignments": any(s in subdirs for s in ["assignments", "assignment", "inlämningar", "uppgifter"]),
        "has_solutions":   any(s in subdirs for s in ["solutions", "solution", "lösningar", "losningar", "facit"]),
        "has_livecode":    any(s in subdirs for s in ["livecode", "livekod", "live", "live_code"]),
        "has_exam":        any(s in subdirs for s in ["exam", "tenta", "tentamen", "test"]),
    }

def classify_source(rel_parts: list) -> str:
    if not rel_parts:
        return "root"
    top = rel_parts[0].lower()
    if top in ("2022", "2023", "2024", "2025", "2026"):
        return f"year_{top}"
    if top == "books":
        return "books"
    if "codic" in top:
        return "codic"
    if "cloud" in top or "coud" in top:
        return "cloud_bcd"
    return top

def should_index(path: Path, rel: Path) -> bool:
    parts = rel.parts
    # skip root level
    if len(parts) < 2:
        return False
    # skip config/build/asset dirs
    skip = {"_site", "_sass", "_includes", "_layouts", "_data", "_posts",
            ".github", "node_modules", "__pycache__", "assets", "css", "fonts",
            "images", "_book", "pdf", "pdfconfigs", "var", "_tooltips",
            "JekyllYaml", "JekyllYaml_old", "JekyllYaml_old3", "learncoding"}
    if any(p.startswith((".", "_")) or p in skip for p in parts):
        return False
    # only index at depth 2–4
    if len(parts) < 2 or len(parts) > 5:
        return False
    return True

def build_inventory() -> dict:
    modules = []
    seen = set()

    for dirpath in sorted(BASE.rglob("*/")):
        rel = dirpath.relative_to(BASE)
        if not should_index(dirpath, rel):
            continue
        key = str(rel)
        if key in seen:
            continue
        seen.add(key)

        rel_parts = list(rel.parts)
        topics = detect_topics(rel)
        file_counts = count_files(dirpath)
        total_files = sum(file_counts.values())

        # skip near-empty dirs (less than 2 files total, likely just index)
        if total_files < 2:
            continue

        structure = detect_structure(dirpath)
        language = detect_language(dirpath)
        course_mapping = detect_course_mapping(topics)

        modules.append({
            "path": str(rel),
            "full_path": str(dirpath),
            "name": dirpath.name,
            "source": classify_source(rel_parts),
            "depth": len(rel_parts),
            "language": language,
            "topics": topics,
            "course_mapping": course_mapping,
            "file_counts": file_counts,
            "total_files": total_files,
            **structure,
        })

    return {
        "generated": str(date.today()),
        "base_path": str(BASE),
        "total_modules": len(modules),
        "modules": modules,
    }

def main():
    parser = argparse.ArgumentParser(description="Inventera old_courses → JSON")
    parser.add_argument("--output", default=str(OUTPUT_DEFAULT), help="Sökväg till output-JSON")
    args = parser.parse_args()

    print(f"Skannar {BASE} ...")
    inventory = build_inventory()

    out = Path(args.output)
    out.parent.mkdir(parents=True, exist_ok=True)
    with open(out, "w", encoding="utf-8") as f:
        json.dump(inventory, f, ensure_ascii=False, indent=2)

    print(f"✓ {inventory['total_modules']} moduler indexerade → {out}")
    print()
    print("Exempel på sökningar:")
    print(f'  jq \'.modules[] | select(.topics[] | contains("design patterns")) | .path\' {out}')
    print(f'  jq \'.modules[] | select(.language == "csharp" and (.course_mapping[] | contains("kurs-03"))) | .path\' {out}')
    print(f'  jq \'.modules[] | select(.has_exercises == true) | {{path, topics}}\' {out}')

if __name__ == "__main__":
    main()
