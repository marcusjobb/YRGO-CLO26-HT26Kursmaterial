---
marp: true
theme: nion-dark
paginate: true
---

# Installera DB Browser for SQLite
## Verktyget vi använder för att se och redigera databaser visuellt

---

# Vad är DB Browser for SQLite?

Ett gratis GUI-verktyg för SQLite-databaser.

- Öppna och bläddra i `.db`-filer
- Köra SQL-frågor visuellt
- Se tabeller, index och data
- Fungerar på Windows, Mac och Linux

**Nedladdning:** [sqlitebrowser.org](https://sqlitebrowser.org)

---

# Windows

1. Gå till **sqlitebrowser.org** → klicka **Download**
2. Välj **Windows** → ladda ner `.msi`-filen (64-bit)
3. Kör installationen och följ guiden
4. Starta **DB Browser for SQLite** från startmenyn

✅ Klart

---

# macOS

**Alternativ 1 — Homebrew (rekommenderas om du har det):**

```bash
brew install --cask db-browser-for-sqlite
```

**Alternativ 2 — Direkt nedladdning:**

1. Gå till **sqlitebrowser.org** → klicka **Download**
2. Välj **macOS** → ladda ner `.dmg`-filen
3. Öppna DMG-filen och dra appen till `Applications`
4. Starta via Launchpad eller Spotlight

✅ Klart

---

# Linux (Ubuntu / Debian)

```bash
sudo apt update
sudo apt install sqlitebrowser
```

**Fedora / RHEL:**

```bash
sudo dnf install sqlitebrowser
```

**Arch:**

```bash
sudo pacman -S sqlitebrowser
```

Starta med: `sqlitebrowser` i terminalen, eller hitta den i app-menyn.

✅ Klart

---

# Verifiera installationen

1. Starta DB Browser for SQLite
2. Klicka **New Database** — ge den ett namn, t.ex. `test.db`
3. Klicka **Create Table** och lägg till ett par kolumner
4. Gå till fliken **Execute SQL** och skriv:

```sql
SELECT * FROM sqlite_master;
```

Om du ser ett resultat fungerar allt.

---

# Nästa steg

Nu har du två sätt att arbeta med SQLite:

| Verktyg | Användning |
|---------|-----------|
| **sqliteonline.com** | Snabb lek i webbläsaren, ingen fil |
| **DB Browser for SQLite** | Riktiga `.db`-filer, visuellt gränssnitt |

I C# pratar vi mot samma `.db`-fil med kod — men DB Browser låter dig se vad som händer inuti.
