# Installera DB Browser for SQLite

## Vad är DB Browser for SQLite?

DB Browser for SQLite är ett gratis, öppen källkod-verktyg med ett grafiskt gränssnitt för SQLite-databaser. Det låter dig:

- Öppna och bläddra i `.db`-filer visuellt
- Köra SQL-frågor och se resultat direkt
- Se tabellstruktur, index och rådata
- Redigera innehåll utan att skriva SQL

Det fungerar på Windows, Mac och Linux. Ladda ner på **sqlitebrowser.org**.

---

## Windows

1. Gå till **sqlitebrowser.org** och klicka **Download**
2. Välj **Windows** och ladda ner `.msi`-filen (64-bit)
3. Kör installationsfilen och följ guiden — standardinställningarna fungerar
4. Starta **DB Browser for SQLite** från startmenyn

---

## macOS

**Alternativ 1 — Homebrew** (rekommenderas om du har det installerat):

```bash
brew install --cask db-browser-for-sqlite
```

**Alternativ 2 — Direkt nedladdning:**

1. Gå till **sqlitebrowser.org** och klicka **Download**
2. Välj **macOS** och ladda ner `.dmg`-filen
3. Öppna DMG-filen och dra appen till mappen **Applications**
4. Starta via Launchpad eller Spotlight (Cmd+Mellanslag → skriv "DB Browser")

---

## Linux

**Ubuntu / Debian:**

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

Starta med kommandot `sqlitebrowser` i terminalen, eller hitta programmet i app-menyn.

---

## Verifiera att det fungerar

1. Starta DB Browser for SQLite
2. Klicka **New Database** och ge den ett namn, till exempel `test.db`
3. Klicka **Create Table** och lägg till ett par kolumner
4. Gå till fliken **Execute SQL** och kör:

```sql
SELECT * FROM sqlite_master;
```

Ser du ett resultat fungerar allting.

---

## Två sätt att jobba med SQLite

Nu har du båda verktygen:

| Verktyg | Användning |
|---------|-----------|
| **sqliteonline.com** | Snabb lek i webbläsaren, ingen fil på disk |
| **DB Browser for SQLite** | Riktiga `.db`-filer, visuellt gränssnitt, sparas lokalt |

I C# pratar applikationen mot samma `.db`-fil med kod. DB Browser låter dig öppna filen och se exakt vad som finns i databasen — perfekt för att felsöka och förstå vad din kod faktiskt gör.
