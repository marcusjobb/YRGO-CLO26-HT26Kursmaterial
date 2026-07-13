# Övning — Kör HyresBil som en systemd-tjänst

🟡

---

## Bakgrunden

Du har precis deployat HyresBil till din Azure VM med SCP. Appen finns på servern. Du SSHar in, kör `dotnet HyresBil.dll` — och det funkar.

Tills du stänger terminalen.

Processen dör direkt. Ingen felkod, ingen logg, ingenting. HyresBil är borta.

Det är såklart oacceptabelt i produktion. Lösningen heter **systemd** — Linuxs inbyggda tjänstehanterare. När du registrerar en app som en systemd-tjänst händer tre saker automatiskt:

1. Appen startar när VM:en bootas
2. Appen startar om sig själv om den kraschar
3. Loggarna hamnar på ett ställe som faktiskt går att hitta

I den här övningen tar du HyresBil från "kör tills jag stänger SSH" till "kör alltid, oavsett vad".

---

## Vad du behöver

- Azure VM med Ubuntu 24.04 (från föregående övning)
- HyresBil deployad till `/opt/hyresbil/` på VM:en
- ASP.NET Core Runtime 10 installerat på VM:en
- SSH-åtkomst till VM:en

---

## Varför inte bara `nohup dotnet &`?

Det är en rimlig fråga. Det här fungerar:

```bash
nohup dotnet /opt/hyresbil/HyresBil.dll &
```

Appen kör. Du kan logga ut. Vad är problemet?

| Situation | `nohup dotnet &` | systemd |
|-----------|-----------------|---------|
| Appen kraschar | Förblir nere tills du SSHar in och startar manuellt | Startar om sig inom 5 sekunder |
| VM:en bootas om (uppdatering, drift) | Appen startar inte | Appen startar automatiskt |
| Loggar | Hamnar i en fil ingen hittar | Integrerade i journald — sökbara med `journalctl` |
| Övervaka status | `ps aux \| grep dotnet` | `systemctl status hyresbil` |
| Stoppa/starta | Hitta PID med `ps`, kör `kill`, starta om manuellt | `systemctl stop hyresbil` / `systemctl start hyresbil` |

`nohup` är ett stödhjul. Systemd är hur Linux-servrar faktiskt hanteras i produktion — nginx, PostgreSQL, MongoDB, din app. Alla körs som systemd-tjänster. Det är dags att du gör det också.

---

## Steg 1 — Skapa service-filen

Systemd läser konfigurationsfiler från `/etc/systemd/system/`. Du skapar en fil per tjänst. Filen beskriver hur tjänsten ska köras, vem som kör den, och när den ska starta.

1. **SSH:a in** på din VM:

   ```bash
   ssh azureuser@<DIN_VM_IP>
   ```

2. **Skapa** service-filen med nano:

   ```bash
   sudo nano /etc/systemd/system/hyresbil.service
   ```

3. **Klistra in** följande innehåll exakt som det ser ut:

   > `/etc/systemd/system/hyresbil.service`

   ```ini
   [Unit]
   Description=HyresBil Minimal API
   After=network.target

   [Service]
   Type=simple
   User=www-data
   WorkingDirectory=/opt/hyresbil
   ExecStart=/usr/bin/dotnet /opt/hyresbil/HyresBil.dll
   Restart=always
   RestartSec=5
   Environment=ASPNETCORE_URLS=http://0.0.0.0:5000
   Environment=ASPNETCORE_ENVIRONMENT=Production

   [Install]
   WantedBy=multi-user.target
   ```

4. **Spara och stäng:** `Ctrl+O` → `Enter` → `Ctrl+X`

> ℹ **Vad gör de tre sektionerna?**
>
> **[Unit]** — Metadata och startordning
>
> `Description` visas i `systemctl status` och `journalctl`. `After=network.target` garanterar att nätverket är uppe innan appen startar. Utan det kan Kestrel misslyckas med att binda port innan nätverkskortet är redo.
>
> **[Service]** — Hur tjänsten faktiskt körs
>
> `Type=simple` innebär att systemd betraktar den process du startar som själva tjänsten — ingen fork, ingen daemon-logik. `User=www-data` kör appen som en icke-privilegierad användare, inte root. `Restart=always` säger: starta om oavsett orsak. `RestartSec=5` väntar 5 sekunder innan omstart — annars fastnar du i en restart-loop om appen kraschar direkt vid start. `ASPNETCORE_URLS=http://0.0.0.0:5000` säger åt Kestrel att lyssna på alla nätverksgränssnitt, inte bara localhost.
>
> **[Install]** — När tjänsten ska vara aktiv
>
> `WantedBy=multi-user.target` är standardvärdet för nätverkstjänster. Det betyder: "aktivera mig vid normal systemstart."
>
> ⚠ **Vanliga misstag**
>
> - Kommentarer med `#` inne i unit-filen kan orsaka tolkningsfel — skriv inga kommentarer i filen
> - `localhost` i `ASPNETCORE_URLS` gör appen oåtkomlig från internet — det ska vara `0.0.0.0`
> - Felstavat DLL-namn i `ExecStart` → tjänsten startar men kraschar direkt
>
> ✓ **Snabbkoll:** Läs tillbaka filen med `cat /etc/systemd/system/hyresbil.service` och kontrollera att innehållet stämmer

---

## Steg 2 — Ladda om, aktivera och starta

Systemd läser inte nya filer automatiskt. Du måste tala om för den att titta igen.

1. **Ladda om** systemd så den ser den nya filen:

   ```bash
   sudo systemctl daemon-reload
   ```

2. **Aktivera** tjänsten — registrera den för automatisk start vid boot:

   ```bash
   sudo systemctl enable hyresbil
   ```

3. **Starta** tjänsten nu utan att vänta på omstart:

   ```bash
   sudo systemctl start hyresbil
   ```

> ℹ **Skillnaden mellan `enable` och `start` — de gör inte samma sak**
>
> Det här är ett klassiskt misstag. `enable` skapar en symlink som gör att tjänsten startar vid boot. Den startar **inte** tjänsten nu. `start` startar tjänsten nu. Den garanterar **inte** att den startar vid nästa omstart. Du behöver båda. Vanligtvis kör du dem i ordningen: `daemon-reload` → `enable` → `start`.
>
> ⚠ **Vanliga misstag**
>
> - Glömma `daemon-reload` efter att du ändrat unit-filen → systemd ser inte ändringarna
> - Bara köra `enable` → appen startar vid nästa boot, men inte nu
>
> ✓ **Snabbkoll:** Kör `systemctl is-enabled hyresbil` — svaret ska vara `enabled`

---

## Steg 3 — Verifiera att appen kör

1. **Kontrollera status:**

   ```bash
   sudo systemctl status hyresbil
   ```

   Du ska se något i den här stilen:

   ```
   ● hyresbil.service - HyresBil Minimal API
        Loaded: loaded (/etc/systemd/system/hyresbil.service; enabled; vendor preset: enabled)
        Active: active (running) since Mon 2026-07-13 09:12:34 UTC; 23s ago
      Main PID: 1234 (dotnet)
         Tasks: 14 (limit: 1149)
        Memory: 58.4M
           CPU: 1.234s
        CGroup: /system.slice/hyresbil.service
                └─1234 /usr/bin/dotnet /opt/hyresbil/HyresBil.dll
   ```

   Det viktigaste: `Active: active (running)` i grönt.

2. **Läs loggarna** med journalctl:

   ```bash
   sudo journalctl -u hyresbil -n 50
   ```

   `-u hyresbil` filtrerar på just den här tjänsten. `-n 50` visar de senaste 50 raderna. Du ser exakt vad Kestrel loggar — startup-meddelanden, fel, requests.

3. **Följ loggarna live** (valfritt — bra för felsökning):

   ```bash
   sudo journalctl -u hyresbil -f
   ```

   Avbryt med `Ctrl+C`.

4. **Testa i webbläsaren:** Öppna `http://<DIN_VM_IP>:5000/` — HyresBil ska svara.

> ⚠ **Vanliga misstag**
>
> - `Active: failed` — Kolla loggarna direkt: `journalctl -u hyresbil -n 30`. Vanligaste orsaken: fel sökväg i `ExecStart`.
> - Appen svarar inte fast tjänsten är aktiv — Azure NSG blockerar troligtvis port 5000. Öppna den med `az vm open-port --resource-group <RG> --name <VM> --port 5000`
>
> ✓ **Snabbkoll:** Status visar `active (running)`, webbläsaren laddar appen

---

## Steg 4 — Testa automatisk omstart

Det här är det roligaste steget. Simulera en krasch och se vad som händer.

1. **Hitta processens PID:**

   ```bash
   sudo systemctl status hyresbil | grep "Main PID"
   ```

   Notera siffran — det är appens process-ID just nu.

2. **Döda processen hårt** med SIGKILL (den signal en krasch motsvarar):

   ```bash
   sudo kill -9 <PID>
   ```

3. **Vänta 5 sekunder** — det är `RestartSec=5` i unit-filen. Kör sedan:

   ```bash
   sudo systemctl status hyresbil
   ```

   Tjänsten är `active (running)` igen. I utdatan ser du att den har startats om.

4. **Bekräfta i loggarna** att omstarten registrerades:

   ```bash
   sudo journalctl -u hyresbil -n 20
   ```

   Leta efter rader med `Started` och `Process exited` nära varandra i tid.

> ℹ **Varför `-9`?**
>
> `kill -9` skickar SIGKILL — processen kan inte fånga den, ignorera den eller städa upp. Det är den mest brutala varianten av "appkrasch". Prova samma sak med `nohup dotnet &` — appen hade legat nere tills du loggat in och startat om den manuellt. Med systemd är du tillbaka på 5 sekunder utan att någon ens vet om det.
>
> ✓ **Snabbkoll:** Tjänsten är `active (running)` inom ~10 sekunder efter att du körde `kill -9`

---

## Steg 5 — Testa omstart av hela VM:en

Det här är det sista beviset. Starta om VM:en och se om HyresBil kommer tillbaka av sig själv.

1. **Starta om VM:en:**

   ```bash
   sudo reboot
   ```

2. **Vänta** ungefär en minut medan VM:en bootar.

3. **SSH:a in igen** och kontrollera:

   ```bash
   sudo systemctl status hyresbil
   ```

4. **Öppna webbläsaren** — HyresBil ska svara utan att du gjort något som helst.

> ✓ **Snabbkoll:** `Active: active (running)` direkt efter inloggning, utan att du startat något manuellt

---

## Vanliga problem

> **`Active: failed` direkt vid start**
>
> Kolla loggarna: `sudo journalctl -u hyresbil -n 30`. Vanligaste orsaken är ett felstavat DLL-namn i `ExecStart`. Kör `ls /opt/hyresbil/` och kontrollera att `HyresBil.dll` faktiskt finns och stavas exakt som i unit-filen.
>
> **`Failed to enable unit: Unit file hyresbil.service does not exist`**
>
> Stavfel i filnamnet när du kör `systemctl enable`. Kör `ls /etc/systemd/system/hyresbil*` för att se vad som finns.
>
> **`daemon-reload` ger felmeddelanden**
>
> Syntax-fel i unit-filen. Kör `systemd-analyze verify /etc/systemd/system/hyresbil.service` — det visar exakt vilken rad som är fel.
>
> **Appen kör men svarar inte i webbläsaren**
>
> Azure NSG blockerar port 5000. Öppna den: `az vm open-port --resource-group <DIN_RG> --name <DIN_VM> --port 5000`
>
> **Tjänsten startar inte efter omstart trots `enable`**
>
> Verifiera: `systemctl is-enabled hyresbil`. Om svaret inte är `enabled` körde du förmodligen `enable` innan `daemon-reload`. Kör om i rätt ordning.

---

## Sammanfattning

HyresBil körs nu som en riktig systemtjänst på din Azure VM:

- ✓ Startar automatiskt när VM:en bootar
- ✓ Startar om sig själv inom 5 sekunder om den kraschar
- ✓ Hanteras med standardverktyg — `systemctl` och `journalctl`
- ✓ Körs som icke-privilegierad användare, inte root

Det viktigaste att ta med sig: systemd är inte överkurs eller "Linux-nörderi". Det är precis hur nginx, PostgreSQL, MongoDB och alla andra servertjänster körs i produktion. Du har nu samma grundstruktur som ops-team använder dagligen.

Nästa steg i kursen är att automatisera hela deploy-kedjan med GitHub Actions — då slipper du SCP:a och SSH:a manuellt varje gång du pushar ny kod.

---

## Gå djupare (valfritt)

- Lägg till `StandardOutput=journal` och `StandardError=journal` explicit i `[Service]` och jämför logg-utdatan
- Testa `systemctl restart hyresbil` och reflektera över hur det skiljer sig från `stop` + `start` i ett produktionsscenario
- Kör `systemctl list-units --type=service --state=running` för att se alla aktiva tjänster på VM:en
- Läs om `Type=notify` — ett smartare alternativ när appen har inbyggt stöd för systemd-integration via .NETbiblioteket `Microsoft.Extensions.Hosting.Systemd`

---

> ⏱️ **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen en AI, sen mig. I den ordningen.

---

*Av Marcus Ackre Medina · Nion Education · marcus.medina@nionit.com*
