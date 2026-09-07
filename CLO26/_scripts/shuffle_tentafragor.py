#!/usr/bin/env python3
"""
shuffle_tentafragor.py — blandar om svarsbokstäver A/B/C jämnt i tentafråga-filer.
D (skämtsvar) lämnas alltid sist och ändras inte.
Garanterar max ceil(n/3) förekomster av varje bokstav bland rätta svar.

Användning:
  python3 _scripts/shuffle_tentafragor.py <fil> [<fil2> ...]
"""

import re
import sys
import random
from pathlib import Path
from math import ceil


def balanced_letters(n):
    """Returnerar n bokstäver (A/B/C) med jämn fördelning, slumpmässig ordning."""
    pool = sorted('ABC' * ceil(n / 3))[:n]
    random.shuffle(pool)
    return pool


def shuffle_file(path):
    text = Path(path).read_text('utf-8')

    parts = re.split(r'\n---\n', text)

    question_indices = []
    old_corrects = []

    for i, part in enumerate(parts):
        if '## Fråga' in part:
            m = re.search(r'\*\*Rätt svar: ([A-C])\*\*', part)
            if m:
                question_indices.append(i)
                old_corrects.append(m.group(1))

    n = len(question_indices)
    if n == 0:
        print(f"  Inga frågor hittades i {path.name}")
        return

    new_corrects = balanced_letters(n)

    for part_idx, old_correct, new_correct in zip(question_indices, old_corrects, new_corrects):
        part = parts[part_idx]

        # Extrahera option-texter för A, B, C
        q_opts = {}
        a_opts = {}
        for letter in 'ABC':
            m = re.search(rf'^{letter}\) (.+?)$', part, re.MULTILINE)
            if m:
                q_opts[letter] = m.group(1)
            m = re.search(rf'^\*\*{letter}\)\*\* (.+?)$', part, re.MULTILINE)
            if m:
                a_opts[letter] = m.group(1)

        # Bygg mapping: {old_letter -> new_letter}
        # Rätt svar: old_correct -> new_correct
        remaining_old = [l for l in 'ABC' if l != old_correct]
        remaining_new = [l for l in 'ABC' if l != new_correct]
        random.shuffle(remaining_new)
        mapping = {old_correct: new_correct}
        for old, new in zip(remaining_old, remaining_new):
            mapping[old] = new

        new_part = part

        # Steg 1: gamla bokstäver -> platshållare
        for old_letter in 'ABC':
            new_letter = mapping[old_letter]
            if old_letter == new_letter:
                continue
            if old_letter in q_opts:
                new_part = new_part.replace(
                    f'{old_letter}) {q_opts[old_letter]}',
                    f'__QQ{old_letter}__', 1
                )
            if old_letter in a_opts:
                new_part = new_part.replace(
                    f'**{old_letter})** {a_opts[old_letter]}',
                    f'__AA{old_letter}__', 1
                )

        # Steg 2: platshållare -> nya bokstäver
        for old_letter in 'ABC':
            new_letter = mapping[old_letter]
            ph_q = f'__QQ{old_letter}__'
            ph_a = f'__AA{old_letter}__'
            if ph_q in new_part and old_letter in q_opts:
                new_part = new_part.replace(ph_q, f'{new_letter}) {q_opts[old_letter]}', 1)
            if ph_a in new_part and old_letter in a_opts:
                new_part = new_part.replace(ph_a, f'**{new_letter})** {a_opts[old_letter]}', 1)

        # Uppdatera rätt svar-raden
        new_part = re.sub(
            r'\*\*Rätt svar: [A-C]\*\*',
            f'**Rätt svar: {new_correct}**',
            new_part
        )

        parts[part_idx] = new_part

    result = '\n---\n'.join(parts)
    Path(path).write_text(result, 'utf-8')

    dist = {l: new_corrects.count(l) for l in 'ABC'}
    print(f"  ✅ {path.name}: {n} frågor")
    print(f"     Rätta svar: {', '.join(new_corrects)}")
    print(f"     Distribution: A={dist['A']} B={dist['B']} C={dist['C']}")


if __name__ == '__main__':
    if len(sys.argv) < 2:
        print("Användning: python3 _scripts/shuffle_tentafragor.py <fil> [<fil2> ...]")
        sys.exit(1)

    for filepath in sys.argv[1:]:
        p = Path(filepath)
        if not p.exists():
            print(f"  ❌ Filen hittades inte: {filepath}")
            continue
        print(f"\n{p.name}:")
        shuffle_file(p)
