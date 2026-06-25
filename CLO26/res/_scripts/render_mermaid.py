#!/usr/bin/env python3
"""Renderar mermaid-kodblock i markdown-filer till PNG.

Går igenom en mapp rekursivt och gör två saker:

1. Hittar ```mermaid-block i markdown-filer, renderar varje block till en
   PNG i en `diagrams/`-mapp bredvid markdown-filen, sparar källkoden i en
   `.mmd`-syskonfil och ersätter blocket med en bildreferens + en
   enradskommentar som pekar på `.mmd`-filen.

   OBS: källkoden läggs INTE i en HTML-kommentar i markdown-filen, eftersom
   mermaids pilsyntax (`-->`, `-->>`) avslutar HTML-kommentarer i förtid och
   Marps mermaid-preprocessor plockar upp ```mermaid-block även inuti
   kommentarer — båda gör att diagrammet läcker ut som synlig text.

2. Letar igenom alla `diagrams/*.mmd`-filer under mappen och renderar om
   bilden om `.mmd`-filen är nyare än `.png`-filen. Vill man ändra ett
   diagram: redigera `.mmd`-filen och kör scriptet igen.

Med --html / --pdf exporteras varje markdown-fil dessutom till en HTML-
respektive PDF-fil bredvid originalet, via `marp --theme-set <tema>`.

Användning:
    python3 render_mermaid.py [mapp] [--config mermaid-dark.json] [--scale 2]
                               [--theme nion_dark.css] [--html] [--pdf]
"""

import argparse
import re
import subprocess
from pathlib import Path

MERMAID_BLOCK = re.compile(r"```mermaid\n(.*?\n)```", re.DOTALL)
MARP_FRONTMATTER = re.compile(r"^---\s*\n.*?^marp:\s*true", re.DOTALL | re.MULTILINE)

SCRIPT_DIR = Path(__file__).resolve().parent
DEFAULT_CONFIG = SCRIPT_DIR.parent / "mermaid-dark.json"
DEFAULT_THEME = SCRIPT_DIR.parent / "nion_dark.css"


def render(mmd_path, png_path, config, scale):
    cmd = [
        "npx", "-y", "@mermaid-js/mermaid-cli",
        "-i", str(mmd_path),
        "-o", str(png_path),
        "-b", "transparent",
        "-s", str(scale),
    ]
    if config.exists():
        cmd += ["-c", str(config)]
    subprocess.run(cmd, check=True, capture_output=True, text=True)


def convert_new_blocks(md_path, config, scale):
    text = md_path.read_text(encoding="utf-8")
    matches = list(MERMAID_BLOCK.finditer(text))
    if not matches:
        return 0

    diagrams_dir = md_path.parent / "diagrams"
    diagrams_dir.mkdir(parents=True, exist_ok=True)

    new_text = text
    offset = 0
    for i, m in enumerate(matches, start=1):
        source = m.group(1)
        name = f"{md_path.stem}_{i}"
        mmd_path = diagrams_dir / f"{name}.mmd"
        png_path = diagrams_dir / f"{name}.png"

        mmd_path.write_text(source, encoding="utf-8")
        print(f"  -> diagrams/{name}.png")
        render(mmd_path, png_path, config, scale)

        replacement = (
            f"![Diagram](diagrams/{name}.png)\n\n"
            f"<!-- mermaid: diagrams/{name}.mmd -->"
        )

        start, end = m.start() + offset, m.end() + offset
        new_text = new_text[:start] + replacement + new_text[end:]
        offset += len(replacement) - (end - start)

    md_path.write_text(new_text, encoding="utf-8")
    return len(matches)


def export(md_path, theme, html, pdf):
    base_cmd = ["marp", str(md_path), "--html", "--theme-set", str(theme)]

    if html:
        out = md_path.with_suffix(".html")
        subprocess.run(base_cmd + ["-o", str(out)], check=True, capture_output=True, text=True)
        print(f"  -> {out.name}")

    if pdf:
        out = md_path.with_suffix(".pdf")
        subprocess.run(base_cmd + ["--pdf", "-o", str(out)], check=True, capture_output=True, text=True)
        print(f"  -> {out.name}")


def update_existing_diagrams(root, config, scale):
    updated = 0
    for mmd_path in sorted(root.rglob("diagrams/*.mmd")):
        png_path = mmd_path.with_suffix(".png")
        if png_path.exists() and png_path.stat().st_mtime >= mmd_path.stat().st_mtime:
            continue
        print(f"{mmd_path.relative_to(root)} har ändrats -> uppdaterar {png_path.name}")
        render(mmd_path, png_path, config, scale)
        updated += 1
    return updated


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("root", nargs="?", default=".", help="Mapp att söka i (rekursivt)")
    parser.add_argument("--config", default=str(DEFAULT_CONFIG), help="Mermaid-konfig (JSON)")
    parser.add_argument("--scale", default=2, type=int, help="Upplösningsskala för PNG (default 2)")
    parser.add_argument("--theme", default=str(DEFAULT_THEME), help="Marp-tema (CSS) för --html/--pdf")
    parser.add_argument("--html", action="store_true", help="Exportera varje .md till .html")
    parser.add_argument("--pdf", action="store_true", help="Exportera varje .md till .pdf")
    args = parser.parse_args()

    root = Path(args.root).resolve()
    config = Path(args.config).resolve()
    theme = Path(args.theme).resolve()

    for md_path in sorted(root.rglob("*.md")):
        if "diagrams" in md_path.parts:
            continue

        text = md_path.read_text(encoding="utf-8")
        did_something = False

        if MERMAID_BLOCK.search(text):
            print(md_path.relative_to(root))
            count = convert_new_blocks(md_path, config, args.scale)
            print(f"  {count} nytt diagram renderat")
            did_something = True

        if (args.html or args.pdf) and MARP_FRONTMATTER.search(text):
            if not did_something:
                print(md_path.relative_to(root))
            export(md_path, theme, args.html, args.pdf)

    update_existing_diagrams(root, config, args.scale)


if __name__ == "__main__":
    main()
