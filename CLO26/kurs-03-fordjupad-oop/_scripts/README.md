# Scripts — dokumentation

Alla scripts körs av läraren, aldrig av studerande.

---

## publish_week.sh

**Vad:** Publicerar en veckas material till studerande-repot på GitHub.

**Kör så här:**
```bash
bash _scripts/publish_week.sh 03
```

**Kräver:**
- `git` installerat och konfigurerat med SSH (`github`-hosten i ~/.ssh/config)
- Studerande-repot ska vara inställt som remote `studerande` i detta repo
- Veckoplansfil `_teacher/week_NN.md` med en `publish:`-sektion

**Vad händer:**
1. Läser `_teacher/week_NN.md` och hämtar filerna under `publish:`
2. Kopierar dessa till ett temporärt område
3. Pushar till `studerande`-remoten

**Lägg till studerande-remote (görs en gång):**
```bash
git remote add studerande git@github:nionit/kurs-csharp-studerande.git
```
