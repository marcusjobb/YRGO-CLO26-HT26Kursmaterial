# Övning — MinVM: Skapa din första Azure-VM med SSH

🟢 Grundnivå

---

## Bakgrunden

Varje server du sätter upp i produktion — oavsett om det är en webbserver, en databas eller ett API — behöver kunna nås säkert. Det gör du med SSH och nyckelpar, inte lösenord.

**Lösenord på produktionsservrar är fel.** De kan gissas, läckas, och återanvändas på ett sätt som nycklar aldrig kan. Det här är industristandard — inte en preferens.

I den här övningen skapar du din första virtuella maskin i Azure, öppnar porten för SSH, och loggar in med ett nyckelpar du genererar lokalt. Scenariot heter **MinVM** och det är din att äga — tills du städar upp den i slutet.

---

## Vad gäller för den här övningen

- [ ] Som utbildare vill jag att du kan skapa ett SSH-nyckelpar lokalt med `ssh-keygen`
- [ ] Som utbildare vill jag att du förstår skillnaden mellan privat och publik nyckel — vad som stannar hos dig och vad som skickas till Azure
- [ ] Som utbildare vill jag att du kan provisionera en Ubuntu-VM i Azure-portalen med rätt storlek, region och autentiseringsmetod
- [ ] Som utbildare vill jag att du öppnar port 22 via NSG och förstår varför det krävs
- [ ] Som utbildare vill jag att du kopplar upp dig med `ssh` och verifierar att du är inne på rätt maskin
- [ ] Som utbildare vill jag att du städar upp resurser med `az group delete` när du är klar

*Dessa punkter är vad vi tittar på — inte om skärmen ser exakt likadan ut.*

---

## Uppgift

### Steg 1 — Skapa ett SSH-nyckelpar

Öppna din terminal (Git Bash på Windows, Terminal på Mac/Linux) och kör:

```bash
ssh-keygen -t rsa -b 4096 -C "minvm-azure" -f ~/.ssh/minvm_key
```

Tryck Enter två gånger för att skippa lösenfras (OK för labbet — i produktion sätter du alltid en).

Du får nu två filer:

```
~/.ssh/minvm_key        ← privat nyckel (stannar på din dator, visa den för ingen)
~/.ssh/minvm_key.pub    ← publik nyckel (den här skickar du till Azure)
```

Visa din publika nyckel och kopiera den — du behöver den om ett ögonblick:

```bash
cat ~/.ssh/minvm_key.pub
```

---

### Steg 2 — Säkra behörigheterna på privata nyckeln (Mac/Linux)

SSH vägrar använda en privat nyckel som andra användare kan läsa. Kör:

```bash
chmod 600 ~/.ssh/minvm_key
```

Windows/Git Bash hanterar det automatiskt — du kan hoppa över det här steget.

---

### Steg 3 — Skapa VM i Azure-portalen

Gå till [portal.azure.com](https://portal.azure.com) och logga in.

1. Sök på **Virtual Machines** och klicka **Create → Azure virtual machine**
2. Fyll i **Basics**:
   - **Subscription**: din aktiva prenumeration
   - **Resource group**: Skapa ny → `minvm-rg`
   - **Virtual machine name**: `minvm`
   - **Region**: `(Europe) Sweden Central`
   - **Availability options**: No infrastructure redundancy required
   - **Image**: `Ubuntu Server 22.04 LTS`
   - **Size**: `Standard_B1s` *(1 vCPU, 1 GiB RAM — tillräckligt för labbet)*
   - **Authentication type**: `SSH public key`
   - **Username**: `azureuser`
   - **SSH public key source**: `Use existing public key`
   - **SSH public key**: klistra in innehållet från `minvm_key.pub`
3. Under **Inbound port rules**:
   - **Public inbound ports**: `Allow selected ports`
   - **Select inbound ports**: `SSH (22)`
4. Klicka **Review + create → Create**

Vänta tills deploymenten är klar (ca 1–2 minuter).

---

### Steg 4 — Hämta IP-adressen

Gå till din VM i portalen och kopiera **Public IP address** från Overview-sidan.

---

### Steg 5 — Koppla upp med SSH

```bash
ssh -i ~/.ssh/minvm_key azureuser@DIN_IP_ADRESS
```

Byt ut `DIN_IP_ADRESS` mot den du kopierade. Första gången frågar SSH om du litar på servern — skriv `yes`.

### Förväntad output

```
The authenticity of host '20.x.x.x (20.x.x.x)' can't be established.
ED25519 key fingerprint is SHA256:xxxxxxxxxxxxxxxxxxxxxxxxxxxx.
Are you sure you want to continue connecting (yes/no/[fingerprint])? yes
Warning: Permanently added '20.x.x.x' (ED25519) to the list of known hosts.

Welcome to Ubuntu 22.04.x LTS (GNU/Linux 6.x.x-azure x86_64)

azureuser@minvm:~$
```

Du är inne. Det där prompten är din server.

---

### Steg 6 — Verifiera att du är på rätt maskin

Kör de här kommandona inne på VM:en:

```bash
uname -a
whoami
```

### Förväntad output

```
Linux minvm 6.x.x-azure #1 SMP ... x86_64 x86_64 x86_64 GNU/Linux
azureuser
```

`uname -a` visar OS och kernel. `whoami` bekräftar att du är inloggad som rätt användare. Ser du `azureuser` och ett Linux-kernel-svar — perfekt.

---

### Steg 7 — Städa upp

Stäng SSH-sessionen:

```bash
exit
```

Ta bort hela resource groupen med az CLI (snabbare än portalen):

```bash
az group delete --name minvm-rg --yes --no-wait
```

`--no-wait` betyder att kommandot returnerar direkt utan att vänta på att allt är borttaget. Borttagningen körs i bakgrunden i Azure.

---

## Tips

> SSH-nycklar fungerar som ett lås (publik nyckel på servern) och en nyckel (privat nyckel hos dig). Azure håller låset — du håller nyckeln. Det är omöjligt att ta sig in utan rätt nyckel, oavsett hur länge man försöker.

> Om SSH säger `Permission denied (publickey)`: kontrollera att du använder rätt sökväg till privata nyckeln med `-i` och att du kopierade hela den publika nyckeln till portalen (inklusive `ssh-rsa` i början och `minvm-azure` i slutet).

> `Connection timed out`? Kontrollera att NSG:n har port 22 öppen — gå till VM → Networking i portalen och verifiera att inbound-regeln finns.

> ⏱️ **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---

*Facit finns hos läraren.*
