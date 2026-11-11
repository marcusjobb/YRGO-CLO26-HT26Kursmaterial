---
marp: true
theme: nion-dark
paginate: true
---

# Installera DB Browser for SQLite
## Verktyget vi använder för att se och redigera databaser visuellt

<!-- DB Browser for SQLite är det visuella komplement till kommandoraden. Det låter studerande se vad som faktiskt finns i deras .db-fil utan att skriva SQL — perfekt för felsökning och förståelse. -->

---

# Vad är DB Browser for SQLite?

Ett gratis GUI-verktyg för SQLite-databaser.

- Öppna och bläddra i `.db`-filer
- Köra SQL-frågor visuellt
- Se tabeller, index och data
- Fungerar på Windows, Mac och Linux

**Nedladdning:** [sqlitebrowser.org](https://sqlitebrowser.org)

<!-- Visa webbplatsen på skärmen medan du pratar om det. Poängtera att det är gratis och öppen källkod — inget konto behövs. -->

---

# Windows

1. Gå till **sqlitebrowser.org** → klicka **Download**
2. Välj **Windows** → ladda ner `.msi`-filen (64-bit)
3. Kör installationen och följ guiden
4. Starta **DB Browser for SQLite** från startmenyn

✅ Klart

<!-- Ge Windows-användare 5 minuter. Vanligaste problemet: studerande laddar ner fel version (32-bit). Påminn om att välja 64-bit. Gå runt och hjälp de som fastnar. -->

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

<!-- Om studerande säger att macOS blockerar appen: högerklicka → Öppna. Gatekeeper-varningen kräver detta första gången för appar utanför App Store. -->

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

<!-- Linux-användare klarar sig oftast själva, men kolla att de faktiskt hittar programmet i menyn eller kör det från terminalen. -->

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

<!-- Verifiera gemensamt i helklass. Fråga vem som har problem. De som är klara hjälper grannen — peer learning. -->

---

# Nästa steg

Nu har du två sätt att arbeta med SQLite:

| Verktyg | Användning |
|---------|-----------|
| **sqliteonline.com** | Snabb lek i webbläsaren, ingen fil |
| **DB Browser for SQLite** | Riktiga `.db`-filer, visuellt gränssnitt |

I C# pratar vi mot samma `.db`-fil med kod — men DB Browser låter dig se vad som händer inuti.

<!-- Betona kopplingen: koden och DB Browser tittar på samma fil. Det är ett kraftfullt debuggingverktyg — du kan öppna .db-filen och se exakt vad INSERT / UPDATE / DELETE gjort. -->
