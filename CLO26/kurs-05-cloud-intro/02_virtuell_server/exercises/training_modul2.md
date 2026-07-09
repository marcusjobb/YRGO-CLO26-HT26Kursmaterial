# Träningsuppgifter: Cloud Intro — Modul 2

> **Modul:** 02 — Virtuell server (CLI, SSH, serverkonfiguration)

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är SSH?

a. Secure Shell — ett krypterat protokoll för att logga in på och administrera fjärrservrar<br>b. Super Simple HTTP<br>c. En filöverföringsmetod<br>d. En databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Secure Shell — ett krypterat protokoll för att logga in på och administrera fjärrservrar

  **Förklaringar:**

  - ✅ **a) Secure Shell** - **RÄTT**: SSH ger dig en terminal på en server var som helst i världen. All trafik är krypterad. Standardport 22. `ssh anvandare@server.ip`
  - ❌ **b) Super Simple HTTP** - FEL: Ingenting med HTTP att göra
  - ❌ **c) Filöverföring** - FEL: SSH kan överföra filer (SCP, SFTP), men huvudsyftet är terminalåtkomst
  - ❌ **d) Databas** - FEL: SSH är ett nätverksprotokoll, inte en databas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är ett publikt/private nyckelpar inom SSH?

a. Ett lås och en nyckel för att öppna en dörr<br>b. Publik nyckel (delas med servern) + Privat nyckel (behålls hemlig) — används för lösenordslös inloggning<br>c. Samma som användarnamn och lösenord<br>d. En sessionsnyckel

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Publik nyckel + Privat nyckel — används för lösenordslös inloggning

  **Förklaringar:**

  - ❌ **a) Fysiskt lås** - FEL: Analogi, inte tekniskt korrekt
  - ✅ **b) Publik på servern, privat hos dig** - **RÄTT**: `ssh-keygen -t rsa -b 4096` skapar paret. Publika nyckeln lägger du i `~/.ssh/authorized_keys` på servern. Privata nyckeln stannar på din dator. Vid inloggning bevisar du att du äger den privata nyckeln — utan att skicka lösenord
  - ❌ **c) Användarnamn/lösenord** - FEL: Nyckelpar är SÄKRARE än lösenord — ingen risk för lösenordsfiske
  - ❌ **d) Sessionsnyckel** - FEL: Sessionsnycklar skapas efter autentisering, inte före
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är en "bastion host"?

a. En server som är direkt tillgänglig från internet och används som inkörsport till interna servrar<br>b. En server som bara finns på papper<br>c. En lastbalanserare<br>d. En databas-server

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En server som är direkt tillgänglig från internet och används som inkörsport till interna servrar

  **Förklaringar:**

  - ✅ **a) Inkörsport till interna servrar** - **RÄTT**: En bastion host (hopplåda) är den ENDA servern som är exponerad mot internet. Du SSHar till bastionen, och därifrån SSHar du vidare till interna servrar. Minskar attackytan
  - ❌ **b) Papper** - FEL: Bastion host är en verklig server
  - ❌ **c) Lastbalanserare** - FEL: Lastbalanserare distribuerar trafik, bastion host ger åtkomst
  - ❌ **d) Databas-server** - FEL: Bastion host är för administration, inte datalagring
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är CLI (Command Line Interface)?

a. Ett grafiskt användargränssnitt<br>b. Ett textbaserat gränssnitt där du skriver kommandon för att interagera med systemet — grunden för molnadministration<br>c. Ett programmeringsspråk<br>d. En typ av databas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett textbaserat gränssnitt där du skriver kommandon för att interagera med systemet

  **Förklaringar:**

  - ❌ **a) Grafiskt** - FEL: GUI (Graphical User Interface) är motsatsen
  - ✅ **b) Textbaserat, kommandon** - **RÄTT**: CLI är standard för molnadministration. `az vm create`, `kubectl get pods`, `docker ps`. Går att scripta och automatisera — omöjligt med GUI
  - ❌ **c) Programmeringsspråk** - FEL: CLI är ett gränssnitt, inte ett språk (men du använder kommandon i ett skal som Bash)
  - ❌ **d) Databas** - FEL: CLI är administrationsverktyg, inte databas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad gör kommandot `chmod 600 ~/.ssh/id_rsa`?

a. Ändrar ägare av filen<br>b. Sätter rättigheterna så att BARA ägaren kan läsa och skriva — den privata nyckeln måste vara skyddad<br>c. Skapar en ny SSH-nyckel<br>d. Kopierar nyckeln till servern

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Sätter rättigheterna så att BARA ägaren kan läsa och skriva

  **Förklaringar:**

  - ❌ **a) Ändrar ägare** - FEL: Det gör `chown`, inte `chmod`
  - ✅ **b) Skyddar den privata nyckeln** - **RÄTT**: `chmod 600` = read+write för ägaren, inget för andra. SSH vägrar använda en privat nyckel som har för öppna rättigheter — säkerhetsåtgärd
  - ❌ **c) Skapar nyckel** - FEL: Det gör `ssh-keygen`
  - ❌ **d) Kopierar** - FEL: Det gör `ssh-copy-id`
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är en "provisionering" i molnet?

a. Att planera resursanvändning<br>b. Processen att skapa och konfigurera molnresurser — som att starta en VM, skapa en databas eller konfigurera nätverk<br>c. Att ta bort resurser<br>d. Att fakturera för resurser

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Processen att skapa och konfigurera molnresurser

  **Förklaringar:**

  - ❌ **a) Planera** - FEL: Planering före provisionering, inte själva skapandet
  - ✅ **b) Skapa och konfigurera** - **RÄTT**: `az vm create --name MyVM...` — det är provisionering. Med IaC (Terraform/Bicep) görs detta automatiskt och repeterbart
  - ❌ **c) Ta bort** - FEL: Det är deprovisionering
  - ❌ **d) Fakturera** - FEL: Fakturering sker EFTER provisionering
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
